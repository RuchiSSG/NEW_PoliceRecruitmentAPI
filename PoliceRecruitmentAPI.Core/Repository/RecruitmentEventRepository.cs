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
	public class RecruitmentEventRepository
	{
		private readonly DatabaseContext _dbContext;

		public RecruitmentEventRepository(DatabaseContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<IActionResult> RecruitEvent(RecruitmentEventDto model)
		{
			using (var connection = _dbContext.CreateConnection())
			{

				var parameter = SetRecruitEvent(model);
				try
				{
					var sqlConnection = (Microsoft.Data.SqlClient.SqlConnection)connection;
					await sqlConnection.OpenAsync();
					var queryResult = await connection.QueryMultipleAsync("proc_RecruitmentEventParameter", parameter, commandType: CommandType.StoredProcedure);
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
		public async Task<IActionResult> Get(RecruitmentEventDto model)
		{
			using (var connection = _dbContext.CreateConnection())
			{

				var parameter = SetRecruitEvent(model);
				try
				{
					var sqlConnection = (Microsoft.Data.SqlClient.SqlConnection)connection;
					await sqlConnection.OpenAsync();


					var queryResult = await connection.QueryMultipleAsync("proc_RecruitmentEventParameter", parameter, commandType: CommandType.StoredProcedure);
					var Model = queryResult.ReadSingleOrDefault<Object>();
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

        public async Task<IActionResult> GetSign(AllEventSignDto model)
        {
            using (var connection = _dbContext.CreateConnection())
            {

                var parameter = SetRecruitEventSign(model);
                try
                {
                    var sqlConnection = (Microsoft.Data.SqlClient.SqlConnection)connection;
                    await sqlConnection.OpenAsync();


                    var queryResult = await connection.QueryMultipleAsync("proc_RecruitmentEventParameter", parameter, commandType: CommandType.StoredProcedure);
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
        public DynamicParameters SetRecruitEvent(RecruitmentEventDto user)
		{
			DynamicParameters parameters = new DynamicParameters();
			parameters.Add("@OperationType", user.BaseModel.OperationType, DbType.String);
			parameters.Add("@UserId", user.UserId, DbType.String);
			parameters.Add("@Id", user.Id, DbType.Guid);
            parameters.Add("@EventId", user.EventsId, DbType.Guid);
            parameters.Add("@eventName", user.eventName, DbType.String);
			parameters.Add("@eventUnit", user.eventUnit, DbType.String);
			parameters.Add("@recConfId", user.recConfId, DbType.String);
            parameters.Add("@groupid", user.groupid, DbType.String);
            parameters.Add("@categoryId", user.categoryId, DbType.String);
            parameters.Add("@isActive", user.isActive, DbType.String);
            //parameters.Add("@ChestNo", user.ChestNo, DbType.String);
            //parameters.Add("@CandidateID", user.CandidateID, DbType.String);
            //parameters.Add("@CandidateName", user.CandidateName, DbType.String);
            //parameters.Add("@eightmeter", user.eightmeter, DbType.String);
            //parameters.Add("@hundredmeter", user.hundredmeter, DbType.String);
            //parameters.Add("@sixteenhundredmeter", user.sixteenhundredmeter, DbType.String);
            //parameters.Add("@Shotput", user.Shotput, DbType.String);
            //parameters.Add("@Signature", user.Signature, DbType.String);
            //parameters.Add("@Date", user.Date, DbType.DateTime);
            if (user.DataTable != null && user.DataTable.Rows.Count > 0)
			{
				parameters.Add("@RecruitmentConfig", user.DataTable.AsTableValuedParameter("[dbo].[tbl_RecruitmentConfig]"));
			}
            
            parameters.Add("@OutcomeId", dbType: DbType.Int32, direction: ParameterDirection.Output);
			parameters.Add("@OutcomeDetail", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);
			return parameters;

		}

        public DynamicParameters SetRecruitEventSign(AllEventSignDto user)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@OperationType", user.BaseModel.OperationType, DbType.String);
            parameters.Add("@UserId", user.UserId, DbType.String);
             
            parameters.Add("@recConfId", user.recConfId, DbType.String);
             
             
            if (user.DataTablesign != null && user.DataTablesign.Rows.Count > 0)
            {
                parameters.Add("@allrecruitsign", user.DataTablesign.AsTableValuedParameter("[dbo].[tbl_AllRecruitmentEventSign]"));
            }
            parameters.Add("@OutcomeId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@OutcomeDetail", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);
            return parameters;

        }
    }
}
