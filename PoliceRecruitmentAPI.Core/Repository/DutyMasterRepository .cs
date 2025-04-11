using Dapper;
using Microsoft.AspNetCore.Mvc;
using PoliceRecruitmentAPI.Core.ModelDtos;
using PoliceRecruitmentAPI.DataAccess.Context;
using System.Data;

namespace PoliceRecruitmentAPI.Core.Repository
{
	public class DutyMasterRepository
	{
		private readonly DatabaseContext _dbContext;
		public DutyMasterRepository(DatabaseContext dbContext)
		{
			_dbContext = dbContext;
		}
		public async Task<IActionResult> Duty(DutyMasterDto model)
		{

			using (var connection = _dbContext.CreateConnection())
			{
				var parameter = SetParameter(model);
				try
				{
					var sqlConnection = (Microsoft.Data.SqlClient.SqlConnection)connection;
					await sqlConnection.OpenAsync();
					var queryResult = await connection.QueryMultipleAsync("proc_DutyMaster", parameter, commandType: CommandType.StoredProcedure, commandTimeout: 300);
					var Model = queryResult.Read<Object>();
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
		public async Task<IActionResult> Get(DutyMasterDto model)
		{
			using (var connection = _dbContext.CreateConnection())

			{
				var parameter = SetParameter(model);
				try
				{
					var sqlConnection = (Microsoft.Data.SqlClient.SqlConnection)connection;
					await sqlConnection.OpenAsync();
					var queryResult = await connection.QueryMultipleAsync("proc_DutyMaster", parameter, commandType: CommandType.StoredProcedure, commandTimeout: 300);
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

		public DynamicParameters SetParameter(DutyMasterDto user)
		{
			DynamicParameters parameters = new DynamicParameters();
			parameters.Add("@OperationType", user.BaseModel?.OperationType, DbType.String);
			parameters.Add("@userId", user.UserId, DbType.String);
			parameters.Add("@d_id", user.d_id, DbType.Guid);
			
			parameters.Add("@d_dutyname", user.d_dutyname, DbType.String);
			parameters.Add("@d_description", user.d_description, DbType.String);
			parameters.Add("@d_module", user.d_module, DbType.String);
			parameters.Add("@d_isactive", user.d_isactive, DbType.String);
			parameters.Add("@d_is_Admin", user.d_is_Admin, DbType.String);
			parameters.Add("@d_recruitid", user.d_recruitid, DbType.String);
			parameters.Add("@d_no_of_user", user.d_no_of_user, DbType.String);
			parameters.Add("@d_createddate", user.d_createddate, DbType.DateTime);
			parameters.Add("@d_updateddate", user.d_updateddate, DbType.DateTime);
			if (user.DataTable != null && user.DataTable.Rows.Count > 0)
			{
				parameters.Add("@DutyPrivilege", user.DataTable.AsTableValuedParameter("[dbo].[Tbl_DutyPrivilege]"));
			}
			parameters.Add("@OutcomeId", dbType: DbType.Int32, direction: ParameterDirection.Output);
			parameters.Add("@OutcomeDetail", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);
			return parameters;
		}
	}
}
