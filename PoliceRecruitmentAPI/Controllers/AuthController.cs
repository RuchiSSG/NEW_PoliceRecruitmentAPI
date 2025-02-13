using Context;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PoliceRecruitmentAPI.Core.ModelDtos;
using PoliceRecruitmentAPI.DataAccess.Context;
using PoliceRecruitmentAPI.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Token;


namespace PoliceRecruitmentAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		public IConfiguration _configuration;
		private readonly ILogger<AuthController> _logger;
		public readonly IAuthService _authService;
		private readonly DatabaseContext _dbContext;
		public AuthController(ILogger<AuthController> logger, IConfiguration configuration, IAuthService authService, DatabaseContext dbContext)
		{
			_logger = logger;
			_configuration = configuration;
			_authService = authService;
			_dbContext = dbContext;

		}

        //[HttpPost(Name = "Login")]
        //public async Task<IActionResult> Login(LoginDto model)
        //{
        //    try
        //    {
        //        if (model.BaseModel == null)
        //        {
        //            model.BaseModel = new BaseModel();
        //        }
        //        model.BaseModel.OperationType = "ValidateLogin";
        //        jwtTokenCreate tk = new jwtTokenCreate(_configuration);
        //        dynamic userDetail = await _authService.VerifyUser(model);

        //        var outcomeidvalue = userDetail.Value.Outcome.OutcomeId;
        //        var loginoutcomes = userDetail.Value.Outcome;


        //        //  return userDetail;
        //        var outcomes = userDetail.Value.Outcome;
        //        var Model = userDetail.Value.Data;
        //        var username = Model.Username;
        //        Result result;




        //        if (Model.username != null)
        //        {

        //            //var loginoutcomes = Model.Value.Outcome;
        //            //var loginModel = loginuser.Value.Data;
        //            //string? RoleId = loginModel.urm_role_id;
        //            //Model.RoleId = RoleId;
        //            //string? Username = loginModel.um_user_name;
        //            //Model.Username = Username;
        //            //Model.com_company_name = loginModel.com_company_name;
        //            string staffid = Model.Staffid;
        //            string staffname = Model.Staffname;

        //            string UserId = Model.UserId.ToString();
        //            if (Model.RoleId != null)
        //            {
        //                string roleid = Model.RoleId.ToString();
        //            }
        //            else
        //            {
        //                string RoleId = Model.DutyId.ToString();

        //            }

        //            string dutyname = Model.DutyName;
        //            //Model.co_country_code = loginModel.co_country_code;
        //            //Model.co_timezone = loginModel.co_timezone;
        //            //Model.cm_currency_format = loginModel.cm_currency_format;
        //            //Model.cm_currencysymbol = loginModel.cm_currencysymbol;
        //            //Model.st_staff_name = loginModel.st_staff_name

        //            if (Model.UserId != null)
        //            {
        //                var authClaims = new List<Claim>
        //                    {
        //				     // new Claim(ClaimTypes.NameIdentifier,roleid.ToString()),
        //						 new Claim(ClaimTypes.NameIdentifier,UserId.ToString()),
        //                         new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        //                    };
        //                var token = GetToken(authClaims);
        //                string s = UserId;
        //                var Token = new JwtSecurityTokenHandler().WriteToken(token);
        //                var expiration = token.ValidTo.ToString();
        //                var OperationType = "InsertToken";
        //                TokenRepo TR = new TokenRepo(_dbContext);
        //                var InsterToken = await TR.InsertToken(Token, expiration, s, OperationType);
        //                if (loginoutcomes.OutcomeDetail == "login successfully")
        //                {
        //                    LoginDto loginDetails = new LoginDto();
        //                    if (loginDetails.BaseModel == null)
        //                    {
        //                        loginDetails.BaseModel = new BaseModel();
        //                    }


        //                    string encrypttext = tk.Encrypt(Token, "abcdefghijklmnop");//remove
        //                                                                               //jwtTokenCreate jkt = new jwtTokenCreate(_configuration);

        //                    //string jwtToken = jkt.GenerateJwtToken(roleid, rolename, staffid, staffname);
        //                    //string encrypttext = jwtToken;
        //                    //tk.Encrypt(jwtToken, "abcdefghijklmnop");
        //                    //var InsterToken1 = await TR.InsertToken(Token, expiration, s, OperationType);
        //                    Outcome outcome = new Outcome
        //                    {
        //                        OutcomeId = loginoutcomes.OutcomeId,
        //                        OutcomeDetail = loginoutcomes.OutcomeDetail,
        //                        Tokens = encrypttext,//remove
        //                                             //Tokens = Token,
        //                        Expiration = expiration
        //                    };
        //                    result = new Result
        //                    {
        //                        Data = Model,
        //                        Outcome = outcome,
        //                        //UserId= createduser.Value.Data.Id
        //                    };

        //                    //Response.Cookies.Append("jwtToken", jwtToken.ToString());
        //                    return Ok(new { result });


        //                }
        //                else
        //                {
        //                    Outcome outcome = new Outcome
        //                    {

        //                        OutcomeId = 0,
        //                        OutcomeDetail = "Please enter valid credentials",
        //                        Tokens = null,
        //                        Expiration = null
        //                    };
        //                    result = new Result
        //                    {
        //                        Data = null,
        //                        Outcome = outcome,
        //                        //UserId= createduser.Value.Data.Id
        //                    };

        //                    return Ok(new { result });
        //                }


        //            }
        //            else
        //            {
        //                Outcome outcome = new Outcome
        //                {

        //                    OutcomeId = 0,
        //                    OutcomeDetail = "Please enter valid credentials",
        //                    Tokens = null,
        //                    Expiration = null
        //                };
        //                result = new Result
        //                {
        //                    Data = null,
        //                    Outcome = outcome,
        //                    //UserId= createduser.Value.Data.Id
        //                };

        //                return Ok(new { result });
        //            }

        //        }


        //        else
        //        {
        //            Outcome outcome = new Outcome
        //            {

        //                OutcomeId = 2,
        //                OutcomeDetail = "Please enter valid credentials",
        //                Tokens = null,
        //                Expiration = null
        //            };
        //            result = new Result
        //            {
        //                Data = null,
        //                Outcome = outcome,
        //                //UserId= createduser.Value.Data.Id
        //            };

        //            return Ok(new { result });
        //        }





        //    }
        //    catch (Exception ex)
        //    {
        //        return new JsonResult(new { message = ex.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
        //    }
        //}

        // new login method
        [HttpPost(Name = "Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            _logger.LogInformation("Received login request: {@model}", model);

            if (model == null || string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password))
            {
                _logger.LogWarning("Invalid request payload: {model}", model);
                var result = new Result
                {
                    Data = null,
                    Outcome = new Outcome
                    {
                        OutcomeId = 0,
                        OutcomeDetail = "Invalid request payload",
                        Tokens = null,
                        Expiration = null
                    }
                };
                return BadRequest(result);
            }

            try
            {
                if (model.BaseModel == null)
                {
                    model.BaseModel = new BaseModel();
                }
                model.BaseModel.OperationType = "ValidateLogin";
                var SessionId = model.SessionId.ToString();
                var IpAddress = model.IpAddress.ToString();
                jwtTokenCreate tk = new jwtTokenCreate(_configuration);
                dynamic userDetail = await _authService.VerifyUser(model);

                if (userDetail.Value == null)
                {
                    _logger.LogWarning("Unexpected result from authentication service.");
                    var result = new Result
                    {
                        Data = null,
                        Outcome = new Outcome
                        {
                            OutcomeId = 0,
                            OutcomeDetail = "Unexpected result from authentication service",
                            Tokens = null,
                            Expiration = null
                        }
                    };
                    return BadRequest(result);
                }

                var outcome = userDetail.Value.Outcome;
                var Model = userDetail.Value.Data;
                var outcomeId = outcome.OutcomeId;
                var outcomeDetail = outcome.OutcomeDetail;
               

                if (Model != null)
                {
                    if (outcomeId == 1)
                    {
                        string staffid = Model.Staffid;
                        string staffname = Model.Staffname;
                        string roleid = Model.RoleId?.ToString();
                        string dutyid = Model.DutyId?.ToString();
                        string dutyname = Model.DutyName;
                        var UserId = Model.UserId.ToString();
                        var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, UserId),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                        var token = GetToken(authClaims);
                        var Token = new JwtSecurityTokenHandler().WriteToken(token);
                        var expiration = token.ValidTo.ToString();
                        var OperationType = "InsertToken";
                        TokenRepo TR = new TokenRepo(_dbContext);
                        var InsterToken = await TR.InsertToken(Token, expiration, UserId, OperationType, SessionId, IpAddress);

                        string encrypttext = tk.Encrypt(Token, "abcdefghijklmnop");

                        var result = new Result
                        {
                            Data = Model,
                            Outcome = new Outcome
                            {
                                OutcomeId = outcomeId,
                                OutcomeDetail = outcomeDetail,
                                Tokens = encrypttext,
                                Expiration = expiration,
                                UserId= Model.UserId.ToString(),
                                SessionId = model.SessionId,
                                IpAddress = model.IpAddress
                            }
                        };

                        return Ok(new { result });
                    }
                    else
                    {
                        var result = new Result
                        {
                            Data = null,
                            Outcome = new Outcome
                            {
                                OutcomeId = outcomeId,
                                OutcomeDetail = "Invalid username or password",
                                Tokens = null,
                                Expiration = null,
                                SessionId = model.SessionId,
                                IpAddress = model.IpAddress
                            }
                        };

                        return Ok(new { result });
                    }
                }
                else
                {
                    _logger.LogWarning("Invalid username or password.");
                    var result = new Result
                    {
                        Data = null,
                        Outcome = new Outcome
                        {
                            OutcomeId = 0,
                            OutcomeDetail = "Invalid username or password",
                            Tokens = null,
                            Expiration = null,
                            SessionId = model.SessionId,
                            IpAddress = model.IpAddress
                        }
                    };
                    return BadRequest(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during login.");
                var result = new Result
                {
                    Data = null,
                    Outcome = new Outcome
                    {
                        OutcomeId = 0,
                        OutcomeDetail = "An error occurred during login",
                        Tokens = null,
                        Expiration = null,
                        SessionId = model.SessionId,
                        IpAddress = model.IpAddress
                    }
                };
                return StatusCode(StatusCodes.Status500InternalServerError, result);
            }
        }






        private JwtSecurityToken GetToken(List<Claim> authClaims)
		{
			var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

			var token = new JwtSecurityToken(
				issuer: _configuration["JWT:ValidIssuer"],
				audience: _configuration["JWT:ValidAudience"],
				expires: DateTime.Now.AddHours(2),
				claims: authClaims,
				signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
				);

			return token;
		}
	}
}
