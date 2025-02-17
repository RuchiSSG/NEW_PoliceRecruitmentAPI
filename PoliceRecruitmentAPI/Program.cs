using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using PoliceRecruitmentAPI.Core.Repository;
using PoliceRecruitmentAPI.DataAccess.Context;
using PoliceRecruitmentAPI.DataAccess.Repository;
using PoliceRecruitmentAPI.Services.ApiServices;
using PoliceRecruitmentAPI.Services.Interfaces;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

// Ensure Logs directory exists
var logsPath = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
if (!Directory.Exists(logsPath))
{
    Directory.CreateDirectory(logsPath);
}

try
{
    // Configure Serilog
    builder.Host.UseSerilog((context, configuration) =>
    {
        configuration
            .ReadFrom.Configuration(context.Configuration)
            .Enrich.FromLogContext()
            .WriteTo.File(
                Path.Combine(logsPath, "log-.txt"),
                rollingInterval: RollingInterval.Day,
                 outputTemplate: "{Message:lj}{NewLine}{Exception}");
    });

    builder.Logging.ClearProviders();
    builder.Logging.AddSerilog();

    // Add services to the container.
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // Add session state service
    builder.Services.AddDistributedMemoryCache();
    builder.Services.AddSession(options => {
        options.IdleTimeout = TimeSpan.FromMinutes(60);
        options.Cookie.Name = "ephr";
        options.Cookie.IsEssential = true;
    });

    // CORS Configuration
    builder.Services.AddCors(o => o.AddPolicy("default", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    }));

    builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: "AllowOrigin",
            builder =>
            {
                builder.WithOrigins("*")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
    });

    // Database Context Configuration
    if (builder.Environment.IsDevelopment())
    {
        builder.Services.AddDbContext<DatabaseContext>(opts =>
            opts.UseSqlServer(builder.Configuration["ConnectionStrings:dev"]));
    }
    builder.Services.AddDbContext<DatabaseContext>(opts =>
        opts.UseSqlServer(builder.Configuration["ConnectionStrings:prod"]));

    // Service Registration
    builder.Services.AddScoped<IAuthService, AuthService>().AddScoped<AuthRepository>();
    builder.Services.AddScoped<ICandidateService, CandidateService>().AddScoped<CandidateRepository>();
    builder.Services.AddScoped<IDocumentService, DocumentService>().AddScoped<DocumentRepository>();
    builder.Services.AddScoped<IAdmissionCardService, AdmissionCardService>().AddScoped<AdmissionCardRepository>();
    builder.Services.AddScoped<IheiCheMeasurement, heiCheMeasurementService>().AddScoped<heiCheMeasurementRepositry>();
    builder.Services.AddScoped<IAppealService, AppealService>().AddScoped<AppealRepository>();
    builder.Services.AddScoped<IRunningService, RunningService>().AddScoped<RunningRepository>();
    builder.Services.AddScoped<IShotPutService, ShotPutService>().AddScoped<ShotPutRepository>();
    builder.Services.AddScoped<IScanningdocService, ScanningdocService>().AddScoped<ScanningdocRepository>();
    builder.Services.AddScoped<IUserMasterService, UserMasterService>().AddScoped<UserMasterRepository>();
    builder.Services.AddScoped<IRoleMasterService, RoleMasterService>().AddScoped<RoleMasterRepository>();
    builder.Services.AddScoped<IGetWebMenuService, GetWebMenuService>().AddScoped<GetWebMenuRepository>();
    builder.Services.AddScoped<IRecruitmentService, RecruitmentService>().AddScoped<RecruitmentRepository>();
    builder.Services.AddScoped<IRecruitmentEventService, RecruitmentEventService>().AddScoped<RecruitmentEventRepository>();
    builder.Services.AddScoped<IDutyMasterService, DutyMasterService>().AddScoped<DutyMasterRepository>();
    builder.Services.AddScoped<IParameterMasterService, ParameterMasterService>().AddScoped<ParameterMasterRepository>();
    builder.Services.AddScoped<IParameterValueMasterService, ParameterValueMasterService>().AddScoped<ParameterValueMasterRepository>();
    builder.Services.AddScoped<IRFIDChestNoMappingService, RFIDChestNoMappingService>().AddScoped<RFIDChestNoMappingRepository>();
    builder.Services.AddScoped<IDashboardService, DashboardService>().AddScoped<DashboardRepository>();
    builder.Services.AddScoped<IDeviceConfigurationService, DeviceConfigurationService>().AddScoped<DeviceConfigurationRepository>();
    builder.Services.AddScoped<ICategoryMasterService, CategoryMasterService>().AddScoped<CategoryMasterRepository>();
    builder.Services.AddScoped<ICategoryDocPrivilegeService, CategoryDocPrivilegeService>().AddScoped<CategoryDocPrivilegeRepository>();
    builder.Services.AddScoped<ICandidateDailyReportService, CandidateDailyReportServicecs>().AddScoped<CandidateDailyReportRepository>();
    builder.Services.AddScoped<ICastCutOffService, CastCutOffService>().AddScoped<CastCutOffServiceRepository>();
    builder.Services.AddScoped<IOmrMasterService, OmrMasterService>().AddScoped<OmrMasterRepository>();
    builder.Services.AddScoped<IEventAccessService, EventAccessService>().AddScoped<EventAccessRepository>();
    builder.Services.AddScoped<ICandidateScheduleMasterService, CandidateScheduleMasterService>().AddScoped<CandidateScheduleMasterRepository>();

    builder.Services.AddHttpClient();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        Log.Information("Development environment detected, Swagger enabled");
    }

    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    // Configure CORS
    app.UseCors(builder =>
    {
        builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
    app.UseCors("AllowOrigin");

    Log.Information("Application started successfully");
    Log.Information("=====================================================================================================================================");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
    Log.Information("=====================================================================================================================================");
}
finally
{
    Log.Information("Shutting down application");
    Log.Information("=====================================================================================================================================");
    Log.CloseAndFlush();
}