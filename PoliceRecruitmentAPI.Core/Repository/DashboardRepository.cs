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
	public class DashboardRepository
	{
		private readonly DatabaseContext _dbContext;

		public DashboardRepository(DatabaseContext dbContext)
		{
			_dbContext = dbContext;
		}
	
		public async Task<IActionResult> Get(DashboardDto model)
		{
			using (var connection = _dbContext.CreateConnection())
			{

				var parameter = SetCandidate(model);
				try
				{
					var sqlConnection = (Microsoft.Data.SqlClient.SqlConnection)connection;
					await sqlConnection.OpenAsync();
					var queryResult = await connection.QueryMultipleAsync("Proc_Dashboard", parameter, commandType: CommandType.StoredProcedure);
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
		public DynamicParameters SetCandidate(DashboardDto user)
		{
			DynamicParameters parameters = new DynamicParameters();
			parameters.Add("@OperationType", user.BaseModel.OperationType, DbType.String);
			parameters.Add("@UserId", user.UserId, DbType.String);
			parameters.Add("@RecruitId", user.RecruitId, DbType.String);
			parameters.Add("@AllCandidate", user.AllCandidate, DbType.Int64);
			parameters.Add("@ForGround", user.ForGround, DbType.Int64);
			parameters.Add("@Pass", user.Pass, DbType.Int64);
            parameters.Add("@DocumentPass", user.DocumentPass, DbType.Int64);
            parameters.Add("@DocumentFail", user.DocumentFail, DbType.Int64);
            parameters.Add("@Heichestpass", user.Heichestpass, DbType.Int64);
            parameters.Add("@HeichestFail", user.HeichestFail, DbType.Int64);
            parameters.Add("@OutcomeId", dbType: DbType.Int32, direction: ParameterDirection.Output);
			parameters.Add("@OutcomeDetail", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);
			return parameters;

		}
	}
}
