using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PoliceRecruitmentAPI.Core.ModelDtos;
using PoliceRecruitmentAPI.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace PoliceRecruitmentAPI.Core.Repository
{
    public class CandidateDailyReportRepository
    {
        private readonly DatabaseContext _dbContext;

        public CandidateDailyReportRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Candidate(CandidateDailyReportDto model)
        {
            using (var connection = _dbContext.CreateConnection())
            {

                var parameter = SetCandidate(model);
                try
                {
                    var sqlConnection = (Microsoft.Data.SqlClient.SqlConnection)connection;
                    await sqlConnection.OpenAsync();
                    var queryResult = await connection.QueryMultipleAsync("Proc_CandidateDailyReport", parameter, commandType: CommandType.StoredProcedure);
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

        public async Task<IActionResult> Get(CandidateDailyReportDto model)
        {
            using (var connection = _dbContext.CreateConnection())
            {

                var parameter = SetCandidate(model);
                try
                {
                    var sqlConnection = (Microsoft.Data.SqlClient.SqlConnection)connection;
                    await sqlConnection.OpenAsync();


                    var queryResult = await connection.QueryMultipleAsync("Proc_CandidateDailyReport", parameter, commandType: CommandType.StoredProcedure);
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
        public DynamicParameters SetCandidate(CandidateDailyReportDto user)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@OperationType", user.BaseModel.OperationType, DbType.String);
            parameters.Add("@UserId", user.UserId, DbType.String);
            parameters.Add("@RecruitId", user.RecruitId, DbType.String);
            parameters.Add("@documentdata", user.documentdata, DbType.String);
            parameters.Add("@heichestdata", user.heichestdata, DbType.String);
            parameters.Add("@SelectedDate", user.SelectedDate, DbType.Date);
            parameters.Add("@Eventid", user.Eventid, DbType.String);
            parameters.Add("@AllData", user.All, DbType.String);
            parameters.Add("@OutcomeId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@OutcomeDetail", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);
            return parameters;

        }

    }
}
