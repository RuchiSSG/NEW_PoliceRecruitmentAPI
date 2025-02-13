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
    public class EventAccessRepository
    {
        private readonly DatabaseContext _dbContext;
        public EventAccessRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IActionResult> Event(EventAccessMasterDto model)
        {

            using (var connection = _dbContext.CreateConnection())
            {
                var parameter = SetParameter(model);
                try
                {
                    var sqlConnection = (Microsoft.Data.SqlClient.SqlConnection)connection;
                    await sqlConnection.OpenAsync();
                    var queryResult = await connection.QueryMultipleAsync("proc_EventAccessMaster", parameter, commandType: CommandType.StoredProcedure);
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
        public async Task<IActionResult> Get(EventAccessMasterDto model)
        {
            using (var connection = _dbContext.CreateConnection())

            {
                var parameter = SetParameter(model);
                try
                {
                    var sqlConnection = (Microsoft.Data.SqlClient.SqlConnection)connection;
                    await sqlConnection.OpenAsync();
                    var queryResult = await connection.QueryMultipleAsync("proc_EventAccessMaster", parameter, commandType: CommandType.StoredProcedure);
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

        public DynamicParameters SetParameter(EventAccessMasterDto user)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@OperationType", user.BaseModel?.OperationType, DbType.String);
            parameters.Add("@userId", user.UserId, DbType.String);
            parameters.Add("@Id", user.Id, DbType.Guid);
            parameters.Add("@CategoryId", user.CategoryId, DbType.String);
            parameters.Add("@CategoryName", user.CategoryName, DbType.String);
            parameters.Add("@Isactive", user.Isactive, DbType.String);
            parameters.Add("@Is_Admin", user.Is_Admin, DbType.String);
            parameters.Add("@Recruitid", user.RecruitId, DbType.String);
            parameters.Add("@Createddate", user.Createddate, DbType.DateTime);
            parameters.Add("@Updateddate", user.Updateddate, DbType.DateTime);
            if (user.DataTable != null && user.DataTable.Rows.Count > 0)
            {
                parameters.Add("@EventAccess", user.DataTable.AsTableValuedParameter("[dbo].[Tbl_EventAccess]"));
            }
            parameters.Add("@OutcomeId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@OutcomeDetail", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);
            return parameters;
        }
    }
}
