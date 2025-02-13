using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PoliceRecruitmentAPI.Core.ModelDtos;
using PoliceRecruitmentAPI.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace PoliceRecruitmentAPI.Core.Repository
{
    public class DeviceConfigurationRepository
    {
        private readonly DatabaseContext _dbContext;

        public DeviceConfigurationRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> DeviceConf(DeviceConfigurationDto model)
        {
            using (var connection = _dbContext.CreateConnection())
            {
                var parameter = SetDeviceConf(model);
                try
                {
                    var sqlConnection = (Microsoft.Data.SqlClient.SqlConnection)connection;
                    await sqlConnection.OpenAsync();
                    var queryResult = await connection.QueryMultipleAsync("proc_deviceconfiguration", parameter, commandType: CommandType.StoredProcedure);
                    var Model = queryResult.Read<Object>();
                    var outcome = queryResult.ReadSingleOrDefault<Outcome>();
                    var outcomeId = outcome?.OutcomeId ?? 0;
                    var outcomeDetail = outcome?.OutcomeDetail ?? string.Empty;
                    var result = new Result
                    {
                        Outcome = outcome,
                        Data = Model,
                        UserId = model.UserId,
                        IpAddress=model.ipaddress,
                        SessionId=model.sessionid

                    };

                    if (outcomeId == 1)
                    {
                        return new ObjectResult(result)
                        {
                            StatusCode = 200
                        };
                    }
                    else if (outcomeId == 2)
                    {
                        return new ObjectResult(result)
                        {
                            StatusCode = 409
                        };
                    }
                    else if (outcomeId == 3)
                    {
                        return new ObjectResult(result)
                        {
                            StatusCode = 402
                        };
                    }
                    else
                    {
                        return new ObjectResult(result)
                        {
                            StatusCode = 400
                        };
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        public async Task<IActionResult> Get(DeviceConfigurationDto model)
        {
            using (var connection = _dbContext.CreateConnection())
            {
                var parameter = SetDeviceConf(model);
                try
                {
                    var sqlConnection = (Microsoft.Data.SqlClient.SqlConnection)connection;
                    await sqlConnection.OpenAsync();
                    var queryResult = await connection.QueryMultipleAsync("proc_deviceconfiguration", parameter, commandType: CommandType.StoredProcedure);
                    var Model = queryResult.Read<Object>().ToList();
                    var outcome = queryResult.ReadSingleOrDefault<Outcome>();
                    var outcomeId = outcome?.OutcomeId ?? 0;
                    var outcomeDetail = outcome?.OutcomeDetail ?? string.Empty;
                    var result = new Result
                    {
                        Outcome = outcome,
                        Data = Model,
                        UserId = model.UserId,
                        IpAddress=model.ipaddress,
                        SessionId=model.sessionid
                    };

                    if (outcomeId == 1)
                    {
                        return new ObjectResult(result) { StatusCode = 200 };
                    }
                    else
                    {
                        return new ObjectResult(result) { StatusCode = 400 };
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        public DynamicParameters SetDeviceConf(DeviceConfigurationDto user)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@OperationType", user.BaseModel.OperationType, DbType.String);
            parameters.Add("@Id", user.Id, DbType.Guid);
            parameters.Add("@UserId", user.UserId, DbType.String);
            parameters.Add("@RecruitId", user.RecruitId, DbType.String);
            parameters.Add("@EventId", user.EventId, DbType.String);
            parameters.Add("@DeviceId", user.DeviceId, DbType.String);
            parameters.Add("@categoryId", user.categoryId, DbType.String);
            parameters.Add("@Location", user.Location, DbType.String);
            parameters.Add("@IsActive", user.IsActive, DbType.String);
            parameters.Add("@OutcomeId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@OutcomeDetail", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);
            return parameters;

        }
    }
}
