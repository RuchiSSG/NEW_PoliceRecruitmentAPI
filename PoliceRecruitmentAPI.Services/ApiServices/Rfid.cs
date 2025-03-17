using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using PoliceRecruitmentAPI.Services.Interfaces;
using System.Net.NetworkInformation;
using PoliceRecruitmentAPI.Core;
using PoliceRecruitmentAPI.Core.ModelDtos;
using Microsoft.AspNetCore.Mvc;


namespace PoliceRecruitmentAPI.Services.ApiServices
{
    public class Rfid : IRfidRepository
    {
        private readonly TcpListener _tcpListener;
        private string _latestTag;
        private readonly object _lock = new();
        private bool _isRunning = false;
        private string[] _hexString;
        private int _hexdataCount;
        private const int MAX_DATA_COUNT = 1000;
        private string _accessToken;
        private string _refreshToken;
        private static string _cachedTag = null;
        private static DateTime _lastReadTime = DateTime.MinValue;
        private string _connectionStatus = "Connected";
        private bool _ethernetConnected = true;
        private string _userid;
        private string _recruitid;

        public Rfid(int port = 9090)
        {
            try
            {
                string ethernetIp = GetEthernetIpAddress();
                if (ethernetIp == null)
                {
                    _connectionStatus = "Ethernet cable is not connected";
                    _ethernetConnected = false;
                    Console.WriteLine(_connectionStatus);
                    // Still create a listener on loopback address to avoid null reference
                    _tcpListener = new TcpListener(IPAddress.Loopback, port);
                }
                else
                {
                    _tcpListener = new TcpListener(IPAddress.Parse(ethernetIp), port);
                    // Enable port reuse to prevent "Address already in use" error
                    _tcpListener.Server.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                }
            }
            catch (Exception ex)
            {
                _connectionStatus = $"Connection error: {ex.Message}";
                _ethernetConnected = false;
                Console.WriteLine(_connectionStatus);
                // Create a fallback listener to prevent null references
                _tcpListener = new TcpListener(IPAddress.Loopback, port);
            }
        }

        public void SetParameters(string accessToken, string refreshToken, string userid, string recruitid)
        {
            _accessToken = accessToken;
            _refreshToken = refreshToken;
            _userid = userid;
            _recruitid = recruitid;
        }

        public async Task StartListenerAsync()
        {
            if (!_isRunning)
            {
                try
                {
                    _tcpListener.Start();
                    _isRunning = true;
                    _ = Task.Run(async () => await ListenAsync());
                    if (_connectionStatus != "Connected")
                    {
                        Console.WriteLine("Warning: Started with status: " + _connectionStatus);
                    }
                }
                catch (Exception ex)
                {
                    _connectionStatus = $"Failed to start listener: {ex.Message}";
                    _ethernetConnected = false;
                    Console.WriteLine(_connectionStatus);
                }
            }
        }

        private string GetEthernetIpAddress()
        {
            try
            {
                // Get all network interfaces
                NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();

                // Find Ethernet interfaces that are up
                var ethernetInterface = interfaces.FirstOrDefault(ni =>
                    (ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet ||
                     ni.Description.ToLower().Contains("ethernet")) &&
                    ni.OperationalStatus == OperationalStatus.Up);

                // If no Ethernet interface is found or it's not up
                if (ethernetInterface == null)
                {
                    Console.WriteLine("No active Ethernet interface found");
                    return null;
                }

                Console.WriteLine($"Found Ethernet interface: {ethernetInterface.Name} - {ethernetInterface.Description}");

                // Get IP properties of the interface
                IPInterfaceProperties ipProps = ethernetInterface.GetIPProperties();

                // Get IPv4 addresses
                var ipv4Address = ipProps.UnicastAddresses
                    .FirstOrDefault(addr => addr.Address.AddressFamily == AddressFamily.InterNetwork)?.Address;

                if (ipv4Address == null)
                {
                    Console.WriteLine("No IPv4 address found on Ethernet interface");
                    return null;
                }

                Console.WriteLine($"Using Ethernet IP: {ipv4Address}");
                return ipv4Address.ToString();
            }
            catch (Exception ex)
            {
                _connectionStatus = $"Failed to get Ethernet IP: {ex.Message}";
                _ethernetConnected = false;
                Console.WriteLine(_connectionStatus);
                return null;
            }
        }

        public async Task StopListener()
        {
            if (_isRunning)
            {
                _tcpListener.Stop();
                _isRunning = false;
            }
        }

        public async Task<IActionResult> GetLatestTagAsync(RfidOutcome rfidTagDto)
        {
            if (!_isRunning)
            {
                await StartListenerAsync();
            }

            // Check Ethernet connection status on every call
            CheckEthernetConnection();

            // Create the result and outcome objects
            var result1 = new RfidResult
            {
                ConnectionStatus = _connectionStatus,
                IsConnected = _ethernetConnected
            };

            var outcome = new Outcome
            {
                OutcomeId = 1,
                OutcomeDetail = "GetLatestTag",
            };

            var Model = new RfidOutcome
            {
                UserId = rfidTagDto.UserId,
                refershtoken = _refreshToken,
                Result = result1,
                TagId = null,
                RecruitId=rfidTagDto.RecruitId
            };

            var result = new Result
            {
                Outcome = outcome,
                Data = Model,
                UserId = Model.UserId
            };

            // If there's a connection issue, always return null tag with error message
            if (!_ethernetConnected)
            {
                result1.Tag = null;
                result1.Message = _connectionStatus;
                _cachedTag = null;

                return new ObjectResult(result)
                {
                    StatusCode = 400
                };
            }

            int retries = 10; // Max wait time: 10 seconds
            while (retries-- > 0)
            {
                await Task.Delay(1000); // Wait 1 second before checking again

                // Recheck Ethernet connection on each retry
                if (!CheckEthernetConnection())
                {
                    result1.Tag = null;
                    result1.ConnectionStatus = _connectionStatus;
                    result1.IsConnected = false;
                    result1.Message = _connectionStatus;

                    outcome.OutcomeId = 0; // Set to failure

                    return new ObjectResult(result)
                    {
                        StatusCode = 400
                    };
                }

                lock (_lock)
                {
                    if (!string.IsNullOrEmpty(_latestTag))
                    {
                        _cachedTag = _latestTag; // Fixed pointer syntax
                        result1.Tag = _latestTag;
                        Model.TagId = _latestTag; // Store in the top-level property as well
                        result1.Message = "Tag read successfully";
                        Console.WriteLine($"Returning new tag: {_latestTag}");

                        return new ObjectResult(result)
                        {
                            StatusCode = 200
                        };
                    }

                    if (!string.IsNullOrEmpty(_cachedTag))
                    {
                        result1.Tag = _cachedTag;
                        Model.TagId = _cachedTag; // Store in the top-level property as well
                        result1.Message = "Tag read successfully";
                        Console.WriteLine($"Returning cached tag: {_cachedTag}");

                        return new ObjectResult(result)
                        {
                            StatusCode = 200
                        };
                    }
                }
            }

            Console.WriteLine("No new tag detected!"); // Debugging
            result1.Message = "No tag read yet";
            result1.Tag = null;
            outcome.OutcomeId = 0; // Set to failure when no tag is found

            return new ObjectResult(result)
            {
                StatusCode = 200
            };
        }

        private bool CheckEthernetConnection()
        {
            try
            {
                // Get all network interfaces
                NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();

                // Find Ethernet interfaces that are up
                var ethernetInterface = interfaces.FirstOrDefault(ni =>
                    (ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet ||
                     ni.Description.ToLower().Contains("ethernet")) &&
                    ni.OperationalStatus == OperationalStatus.Up);

                // If no Ethernet interface is found or it's not up
                if (ethernetInterface == null)
                {
                    _connectionStatus = "Ethernet cable is not connected";
                    _ethernetConnected = false;
                    Console.WriteLine("Connection check: " + _connectionStatus);
                    return false;
                }

                // Get IP properties of the interface
                IPInterfaceProperties ipProps = ethernetInterface.GetIPProperties();

                // Get IPv4 addresses
                var ipv4Address = ipProps.UnicastAddresses
                    .FirstOrDefault(addr => addr.Address.AddressFamily == AddressFamily.InterNetwork)?.Address;

                if (ipv4Address == null)
                {
                    _connectionStatus = "No IPv4 address found on Ethernet interface";
                    _ethernetConnected = false;
                    Console.WriteLine("Connection check: " + _connectionStatus);
                    return false;
                }

                _connectionStatus = "Connected";
                _ethernetConnected = true;
                return true;
            }
            catch (Exception ex)
            {
                _connectionStatus = $"Connection error: {ex.Message}";
                _ethernetConnected = false;
                Console.WriteLine("Connection check: " + _connectionStatus);
                return false;
            }
        }

        private async Task ListenAsync()
        {
            try
            {
                while (_isRunning)
                {
                    // Check connection before accepting
                    if (!_ethernetConnected)
                    {
                        await Task.Delay(5000); // Wait before rechecking
                        CheckEthernetConnection();
                        continue;
                    }

                    var client = await _tcpListener.AcceptTcpClientAsync();
                    _ = Task.Run(() => ProcessClientAsync(client));
                }
            }
            catch (Exception ex)
            {
                _connectionStatus = $"Listener error: {ex.Message}";
                _ethernetConnected = false;
                Console.WriteLine(_connectionStatus);
                _isRunning = false;
            }
        }

        private async Task ProcessClientAsync(TcpClient client)
        {
            try
            {
                using (var stream = client.GetStream())
                {
                    var buffer = new byte[1024];
                    while (client.Connected && _isRunning)
                    {
                        // Check connection periodically
                        if (!_ethernetConnected)
                        {
                            break;
                        }

                        int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                        if (bytesRead > 0)
                        {
                            string tag = ParseRFIDTag(buffer, bytesRead);
                            lock (_lock)
                            {
                                _latestTag = tag;
                                _cachedTag = tag; // Also update the cached tag
                                Console.WriteLine($"New tag received: {tag}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing client: {ex.Message}");
            }
        }

        private string ParseRFIDTag(byte[] buffer, int bytesRead)
        {
            try
            {
                // Create a StringBuilder for the hex data
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < bytesRead; i++)
                {
                    sb.Append(buffer[i].ToString("X2")).Append(" ");
                }

                string hexstring = sb.ToString().Trim();

                // Process the hexstring in chunks of 128 characters (64 bytes)
                if (hexstring.Length >= 128)
                {
                    string currentSegment = hexstring.Substring(0, 128);

                    // Check if the segment starts with "AA" to be considered valid
                    if (currentSegment.StartsWith("AA"))
                    {
                        string[] hexBytes = currentSegment.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                        if (hexBytes.Length >= 12)
                        {
                            for (int i = 29; i <= hexBytes.Length - 12; i += 12)
                            {
                                string joinedString = string.Join("", hexBytes.Skip(i).Take(12));

                                if (joinedString.Length == 24)
                                {
                                    return joinedString; // Return the first valid tag found
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parsing RFID data: {ex.Message}");
            }

            return string.Empty;
        }

        // Public method to check connection status
        public string GetConnectionStatus()
        {
            CheckEthernetConnection(); // Refresh status before returning
            return _connectionStatus;
        }
    }
}