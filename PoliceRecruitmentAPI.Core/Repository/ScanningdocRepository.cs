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
    public class ScanningdocRepository
    {
        private readonly DatabaseContext _dbContext;

        public ScanningdocRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IActionResult> Scanningdoc(ScanningdocDto model)
        {
            using (var connection = _dbContext.CreateConnection())
            {
                var parameter = SetScanning(model);
                try
                {
                    var sqlConnection = (Microsoft.Data.SqlClient.SqlConnection)connection;
                    await sqlConnection.OpenAsync();
                    var queryResult = await connection.QueryMultipleAsync("Proc_ScanDoc", parameter, commandType: CommandType.StoredProcedure);
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
        public DynamicParameters SetScanning(ScanningdocDto user)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@OperationType", user.BaseModel.OperationType, DbType.String);
            parameters.Add("@UserId", user.UserId, DbType.String);
            parameters.Add("@CandidateID", user.CandidateID, DbType.Int64);
            parameters.Add("@thumbstring", user.Thumbstring, DbType.String);
            parameters.Add("@thumbstring1", user.Thumbstring1, DbType.String);
            parameters.Add("@thumbstring2", user.Thumbstring2, DbType.String);
            parameters.Add("@thumbstring3", user.Thumbstring3, DbType.String);
            parameters.Add("@thumbstring4", user.Thumbstring4, DbType.String);
            parameters.Add("@thumbstring5", user.Thumbstring5, DbType.String);
            parameters.Add("@thumbstring6", user.Thumbstring6, DbType.String);
            parameters.Add("@thumbstring7", user.Thumbstring7, DbType.String);
            parameters.Add("@thumbstring8", user.Thumbstring8, DbType.String);
            parameters.Add("@thumbstring9", user.Thumbstring9, DbType.String);
            parameters.Add("@ChestNo", user.ChestNo, DbType.String);
            parameters.Add("@Date", user.Date, DbType.DateTime);
            parameters.Add("@imagestring", user.Imagestring, DbType.String);
            parameters.Add("@RecruitId", user.RecruitId, DbType.String); 
            parameters.Add("@CategoryName", user.CategoryName, DbType.String);
            parameters.Add("@IVImage", user.IVImage, DbType.String);
            parameters.Add("@Secretkeys", user.Secretkeys, DbType.String);
            parameters.Add("@Signature", user.Signature, DbType.String);
            parameters.Add("@OutcomeId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@OutcomeDetail", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);
            return parameters;

        }

    }
}
