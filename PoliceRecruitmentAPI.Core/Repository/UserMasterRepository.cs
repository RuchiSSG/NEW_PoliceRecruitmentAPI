using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PoliceRecruitmentAPI.Core.ModelDtos;
using PoliceRecruitmentAPI.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceRecruitmentAPI.Core.Repository
{
    public class UserMasterRepository
    {
        private readonly DatabaseContext _dbContext;
        public UserMasterRepository(DatabaseContext dbContext)
        {
			_dbContext = dbContext;
        }

		
		public async Task<IActionResult> UserMaster(UserMasterDto model)
        {

            using (var connection = _dbContext.CreateConnection())

            {
				var parameter = SetUser(model);
				try
                {
                    var sqlConnection = (Microsoft.Data.SqlClient.SqlConnection)connection;
                    await sqlConnection.OpenAsync();
                    var queryResult = await connection.QueryMultipleAsync("proc_UserMaster", parameter, commandType: CommandType.StoredProcedure);
                    var Model = queryResult.Read<Object>().ToList();
                    var outcome = queryResult.ReadSingleOrDefault<Outcome>();
                    var outcomeId = outcome?.OutcomeId ?? 0;
                    var outcomeDetail = outcome?.OutcomeDetail ?? string.Empty;
                    var result = new Result
                    {
                        Outcome = outcome,
                        Data = Model,
                        UserId = model.UserId
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
                            StatusCode = 423
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

		public async Task<IActionResult> Shuffle(UserMasterDto model)
		{

			using (var connection = _dbContext.CreateConnection())

			{
				var parameter = SetUser(model);
				try
				{
					var sqlConnection = (Microsoft.Data.SqlClient.SqlConnection)connection;
					await sqlConnection.OpenAsync();
					var queryResult = await connection.QueryMultipleAsync("proc_UserMaster", parameter, commandType: CommandType.StoredProcedure);
					var Model = queryResult.Read<Object>().ToList();
					var outcome = queryResult.ReadSingleOrDefault<Outcome>();
					var outcomeId = outcome?.OutcomeId ?? 0;
					var outcomeDetail = outcome?.OutcomeDetail ?? string.Empty;
					var result = new Result
					{
						Outcome = outcome,
                        Data = Model,
                        UserId = model.UserId
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

		public async Task<IActionResult> Get(UserMasterDto model)
        {
            using (var connection = _dbContext.CreateConnection())
            {
				var parameter = SetUser(model);
				try
                {
                    var sqlConnection = (Microsoft.Data.SqlClient.SqlConnection)connection;
                    await sqlConnection.OpenAsync();
                    var queryResult = await connection.QueryMultipleAsync("proc_UserMaster", parameter, commandType: CommandType.StoredProcedure);
                    var Model = queryResult.ReadSingleOrDefault<Object>();
                    var outcome = queryResult.ReadSingleOrDefault<Outcome>();
                    var outcomeId = outcome?.OutcomeId ?? 0;
                    var outcomeDetail = outcome?.OutcomeDetail ?? string.Empty;
                    var result = new Result
                    {
                        Outcome = outcome,
						Data = Model,
						UserId = model.UserId
					};

                    if (outcomeId == 1)
                    {
                        return new ObjectResult(result)
                        {
                            StatusCode = 200
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

		public async Task<IActionResult> GetEmailId(EmailConfigureDto model)
		{
			using (var connection = _dbContext.CreateConnection())
			{
				//Guid? userIdValue = (Guid)user.UserId;
				try
				{
					var sqlConnection = (Microsoft.Data.SqlClient.SqlConnection)connection;
					await sqlConnection.OpenAsync();


					
					var queryResult = await connection.QueryMultipleAsync("ProcEmailConfigure", commandType: CommandType.StoredProcedure);
					var Model = queryResult.ReadSingleOrDefault<Object>();
					var outcome = queryResult.ReadSingleOrDefault<Outcome>();
					var outcomeId = outcome?.OutcomeId ?? 0;
					var outcomeDetail = outcome?.OutcomeDetail ?? string.Empty;
					//var IDMail = outcome?.IDMail ?? string.Empty;
					var result = new Result
					{
						Outcome = outcome,
						Data = Model,
						//UserId = model.UserId
					};

					if (outcomeId == 1)
					{
						return new ObjectResult(result)
						{
							StatusCode = 200
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
		public DynamicParameters SetUser(UserMasterDto user)
        {

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@OperationType", user.BaseModel.OperationType, DbType.String);
            parameters.Add("@um_id", user.um_id, DbType.Guid);
            parameters.Add("@UserId", user.UserId, DbType.String);
            parameters.Add("@um_user_name", user.um_user_name, DbType.String);
            parameters.Add("@um_password", user.um_password, DbType.String);
            parameters.Add("@um_staffname", user.um_staffname, DbType.String);
            parameters.Add("@um_duty", user.um_duty, DbType.String); 
            parameters.Add("@um_bukkel_no", user.um_bukkel_no, DbType.String); 
            parameters.Add("@um_post", user.um_post, DbType.String); 
            parameters.Add("@um_duty", user.um_duty, DbType.String); 
            parameters.Add("@um_phone_no", user.um_phone_no, DbType.String); 
            parameters.Add("@um_roleid", user.um_roleid, DbType.String); 
            parameters.Add("@um_recruitid", user.um_recruitid, DbType.String); 
            parameters.Add("@s_umids", user.s_umids, DbType.String); 
            parameters.Add("@um_rolename", user.um_rolename, DbType.String); 
            parameters.Add("@um_isactive", user.um_isactive, DbType.String);
            parameters.Add("@um_createddate", user.um_createddate, DbType.DateTime);
            parameters.Add("@um_updateddate", user.um_updateddate, DbType.DateTime);
			if (user.DataTable != null && user.DataTable.Rows.Count > 0)
			{
				parameters.Add("@UserMaster", user.DataTable.AsTableValuedParameter("[dbo].[tbl_UserMaster]"));
			}
			parameters.Add("@OutcomeId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@OutcomeDetail", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);
            return parameters;

        }
    }
}
