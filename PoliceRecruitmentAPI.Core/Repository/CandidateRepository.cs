using Dapper;
using Microsoft.AspNetCore.Http;
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
	public class CandidateRepository
	{
		private readonly DatabaseContext _dbContext;

		public CandidateRepository(DatabaseContext dbContext)
		{
			_dbContext = dbContext;
		}

	

		public async Task<IActionResult> Candidate(CandidateDto model)
		{
			using (var connection = _dbContext.CreateConnection())
			{
				
				var parameter = SetCandidate(model);
				try
				{
					var sqlConnection = (Microsoft.Data.SqlClient.SqlConnection)connection;
					await sqlConnection.OpenAsync();
					var queryResult = await connection.QueryMultipleAsync("Proc_Candidate", parameter, commandType: CommandType.StoredProcedure);
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
                            StatusCode = 408
                        };
                    }
                    else if (outcomeId == 4)
                    {
                        return new ObjectResult(result)
                        {
                            StatusCode = 407
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

        public async Task<IActionResult> Candidate1(CandidateDto model)
        {
            using (var connection = _dbContext.CreateConnection())
            {

                var parameter = SetCandidate(model);
                try
                {
                    var sqlConnection = (Microsoft.Data.SqlClient.SqlConnection)connection;
                    await sqlConnection.OpenAsync();
                    var queryResult = await connection.QueryMultipleAsync("Proc_Candidate", parameter, commandType: CommandType.StoredProcedure);
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

        public async Task<IActionResult> Candidate2(CandidateDto model)
        {
            using (var connection = _dbContext.CreateConnection())
            {
                var parameter = SetCandidate(model);
                try
                {
                    var sqlConnection = (Microsoft.Data.SqlClient.SqlConnection)connection;
                    await sqlConnection.OpenAsync();

                    // Execute stored procedure and get multiple result sets
                    var queryResult = await connection.QueryMultipleAsync("Proc_CandidateTestReport", parameter, commandType: CommandType.StoredProcedure);

                    // Read the first result set (MeritList)
                    var meritList = queryResult.Read<Object>().ToList(); // This will contain the MeritList data

                    // Read the second result set (CastWisedata)
                    var castWiseData = queryResult.Read<Object>().ToList(); // This will contain the CastWisedata

                    // Read the Outcome (if it exists, you can adjust this based on your stored procedure)
                    var outcomes = queryResult.Read<Outcome>().ToList();
                    var outcome = outcomes.FirstOrDefault(); // Get the first outcome (or handle accordingly)

                    var outcomeId = outcome?.OutcomeId ?? 0;
                    var outcomeDetail = outcome?.OutcomeDetail ?? string.Empty;

                    // Create the result object
                    var result = new Result
                    {
                        Outcome = outcome,
                        Data = new object[] // Data is now an array
                {
                    new { meritList = meritList, castWisedata = castWiseData  }, // Add meritList as an item in the array
                    ////new { castWisedata = castWiseData } // Add castWisedata as an item in the array
                },
                        UserId = model.UserId
                    };

                    // Determine the appropriate status code based on outcomeId
                    if (outcomeId == 1)
                    {
                        return new ObjectResult(result) { StatusCode = 200 };
                    }
                    else if (outcomeId == 2)
                    {
                        return new ObjectResult(result) { StatusCode = 409 };
                    }
                    else if (outcomeId == 3)
                    {
                        return new ObjectResult(result) { StatusCode = 402 };
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


        public async Task<IActionResult> Get(CandidateDto model)
		{
			using (var connection = _dbContext.CreateConnection())
			{
				
				var parameter = SetCandidate(model);
				try
				{
					var sqlConnection = (Microsoft.Data.SqlClient.SqlConnection)connection;
					await sqlConnection.OpenAsync();
				

					var queryResult = await connection.QueryMultipleAsync("Proc_Candidate", parameter, commandType: CommandType.StoredProcedure);
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
		public DynamicParameters SetCandidate(CandidateDto user)
		{
			DynamicParameters parameters = new DynamicParameters();
			parameters.Add("@OperationType", user.BaseModel.OperationType, DbType.String);
			parameters.Add("@UserId", user.UserId, DbType.String);
			parameters.Add("@CandidateID", user.CandidateID, DbType.Int64);
			parameters.Add("@RecruitmentYear", user.RecruitmentYear, DbType.String);
			parameters.Add("@OfficeName", user.OfficeName, DbType.String);
			parameters.Add("@PostName", user.PostName, DbType.String);
			parameters.Add("@ApplicationNo", user.ApplicationNo, DbType.String);
			parameters.Add("@ExaminationFee", user.ExaminationFee, DbType.String);
			parameters.Add("@FullNameDevnagari", user.FullNameDevnagari, DbType.String);
			parameters.Add("@FullNameEnglish", user.FullNameEnglish, DbType.String);
			parameters.Add("@DocumentUploaded", user.DocumentUploaded, DbType.Boolean);
			parameters.Add("@MothersName", user.MothersName, DbType.String);
			parameters.Add("@Gender", user.Gender, DbType.String);
			parameters.Add("@MaritalStatus", user.MaritalStatus, DbType.String);
			parameters.Add("@PassCertificationNo", user.PassCertificationNo, DbType.String);
			parameters.Add("@DOB", user.DOB, DbType.DateTime);
			parameters.Add("@Age", user.Age, DbType.String);
			parameters.Add("@Address", user.Address, DbType.String);
			parameters.Add("@PinCode", user.PinCode, DbType.String);
			parameters.Add("@MobileNumber", user.MobileNumber, DbType.String);
			parameters.Add("@EmailId", user.EmailId, DbType.String);
			parameters.Add("@PermanentAddress", user.PermanentAddress, DbType.String);
			parameters.Add("@PermanentPinCode", user.PermanentPinCode, DbType.String);
			parameters.Add("@Nationality", user.Nationality, DbType.String);
			parameters.Add("@Religion", user.Religion, DbType.String);
			parameters.Add("@Cast", user.Cast, DbType.String);
			parameters.Add("@SubCast", user.SubCast, DbType.String);
			parameters.Add("@Cast", user.Cast, DbType.String);
			parameters.Add("@ChestNo", user.ChestNo, DbType.String);
			parameters.Add("@RecruitId", user.RecruitId, DbType.String);
			parameters.Add("@PartTime", user.PartTime, DbType.Boolean);
			parameters.Add("@ProjectSick", user.ProjectSick, DbType.Boolean);
			parameters.Add("@ExServiceman", user.ExServiceman, DbType.Boolean);
			parameters.Add("@EarthquakeAffected", user.EarthquakeAffected, DbType.Boolean);
			parameters.Add("@Orphan", user.Orphan, DbType.Boolean);
			parameters.Add("@Ancestral", user.Ancestral, DbType.Boolean);
			parameters.Add("@groupid", user.groupid, DbType.Int64);
            parameters.Add("@Categoryname", user.Category, DbType.String);
            parameters.Add("@EventId", user.EventId, DbType.String);
            parameters.Add("@Id", user.Id, DbType.String);
            parameters.Add("@CastId", user.CastId, DbType.String);
             if (user.DataTable != null && user.DataTable.Rows.Count > 0)
            {
                parameters.Add("@Candidate", user.DataTable.AsTableValuedParameter("[dbo].[Candidate]"));
            }
            else if (user.DataTable1 != null && user.DataTable1.Rows.Count > 0)
            {
                parameters.Add("@GrounTest", user.DataTable1.AsTableValuedParameter("[dbo].[TypeGrounTestResult]"));
            }
            else if (user.DataTable2 != null && user.DataTable2.Rows.Count > 0)
            {
                parameters.Add("@CandidateNew", user.DataTable2.AsTableValuedParameter("[dbo].[Candidate_New]"));
            }
            parameters.Add("@OutcomeId", dbType: DbType.Int32, direction: ParameterDirection.Output);
			parameters.Add("@OutcomeDetail", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);
			return parameters;

		}
	}
}
