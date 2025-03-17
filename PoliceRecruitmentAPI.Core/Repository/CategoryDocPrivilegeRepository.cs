using Dapper;
using Microsoft.AspNetCore.Mvc;
using PoliceRecruitmentAPI.Core.ModelDtos;
using PoliceRecruitmentAPI.DataAccess.Context;
using System.Data;

namespace PoliceRecruitmentAPI.Core.Repository
{
    public class CategoryDocPrivilegeRepository
    {
        private readonly DatabaseContext _dbContext;
        public CategoryDocPrivilegeRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IActionResult> Category(CategoryPrirvilegeDto model)
        {

            using (var connection = _dbContext.CreateConnection())
            {
                var parameter = SetParameter(model);
                try
                {
                    var sqlConnection = (Microsoft.Data.SqlClient.SqlConnection)connection;
                    await sqlConnection.OpenAsync();
                    var queryResult = await connection.QueryMultipleAsync("proc_CategoryDocMaster", parameter, commandType: CommandType.StoredProcedure);
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
        public async Task<IActionResult> Get(CategoryPrirvilegeDto model)
        {
            using (var connection = _dbContext.CreateConnection())

            {
                var parameter = SetParameter(model);
                try
                {
                    var sqlConnection = (Microsoft.Data.SqlClient.SqlConnection)connection;
                    await sqlConnection.OpenAsync();
                    var queryResult = await connection.QueryMultipleAsync("proc_CategoryDocMaster", parameter, commandType: CommandType.StoredProcedure);
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

        public DynamicParameters SetParameter(CategoryPrirvilegeDto user)
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
                parameters.Add("@CategoryPrivilege", user.DataTable.AsTableValuedParameter("[dbo].[Tbl_CategoryPrivilege]"));
            }
            parameters.Add("@OutcomeId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@OutcomeDetail", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);
            return parameters;
        }
    }
}
