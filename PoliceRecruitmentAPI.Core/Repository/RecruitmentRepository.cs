using Dapper;
using Microsoft.AspNetCore.Mvc;
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
	public class RecruitmentRepository
	{
		private readonly DatabaseContext _dbContext;

		public RecruitmentRepository(DatabaseContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<IActionResult> Recruit(RecruitmentDto model)
		{
			using (var connection = _dbContext.CreateConnection())
			{

				var parameter = SetRecruitment(model);
				try
				{
					var sqlConnection = (Microsoft.Data.SqlClient.SqlConnection)connection;
					await sqlConnection.OpenAsync();
					var queryResult = await connection.QueryMultipleAsync("proc_Recruitment", parameter, commandType: CommandType.StoredProcedure);
					var Model = queryResult.Read<Object>();
					var outcome = queryResult.ReadSingleOrDefault<Outcome>();
					var outcomeId = outcome?.OutcomeId ?? 0;
					var outcomeDetail = outcome?.OutcomeDetail ?? string.Empty;
                    var UserNamee = outcome?.UserNamee ?? string.Empty;
                    var DecryptedPass = outcome?.DecryptedPass ?? string.Empty;
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
                            StatusCode = 408
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
		public async Task<IActionResult> Get(RecruitmentDto model)
		{
			using (var connection = _dbContext.CreateConnection())
			{

				var parameter = SetRecruitment(model);
				try
				{
					var sqlConnection = (Microsoft.Data.SqlClient.SqlConnection)connection;
					await sqlConnection.OpenAsync();


					var queryResult = await connection.QueryMultipleAsync("proc_Recruitment", parameter, commandType: CommandType.StoredProcedure);
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

		public DynamicParameters SetRecruitment(RecruitmentDto user)
		{
			DynamicParameters parameters = new DynamicParameters();
			parameters.Add("@OperationType", user.BaseModel.OperationType, DbType.String);
            parameters.Add("@UserId", user.UserId, DbType.String);
			parameters.Add("@Id", user.Id, DbType.Guid);
			parameters.Add("@post", user.post, DbType.String);
			parameters.Add("@place", user.place, DbType.String);
            parameters.Add("@noofseats", user.noofseats, DbType.String);
            parameters.Add("@year", user.year, DbType.String);
			parameters.Add("@RecruitId", user.RecruitId, DbType.String);
			parameters.Add("@isActive", user.isActive, DbType.String);
            parameters.Add("@startDate", user.startDate, DbType.DateTime);
            parameters.Add("@noOfCandidate", user.noOfCandidate, DbType.String);
            parameters.Add("@OutcomeId", dbType: DbType.Int32, direction: ParameterDirection.Output);
			parameters.Add("@OutcomeDetail", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);
            parameters.Add("@UserNamee", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);
            parameters.Add("@DecryptedPass", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);
            return parameters;

		}
	}
}
