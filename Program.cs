using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using FluentValidation.AspNetCore;
using KOG.ECommerce.Configurations;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Features.Categories.CreateCategory;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using KOG.ECommerce.Features.Common.Categories.DTOs;
using KOG.ECommerce.Features.Common.Companies.DTOs;
using KOG.ECommerce.Features.Common.RoleModules.GetModulesByRoleId.DTOs;
using KOG.ECommerce.Features.Companies;
using KOG.ECommerce.Common;
using Microsoft.Extensions.FileProviders;
using KOG.ECommerce.Features.Cities.CreateCity;
using KOG.ECommerce.Features.Cities.EditCity;
using KOG.ECommerce.Features.Cities.DeleteCity;
using KOG.ECommerce.Features.Companies.CompanyFilterIndex;
using Autofac.Core;
using FluentValidation;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using KOG.ECommerce.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Cross-origin
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
    builder.RegisterModule(new AutofacModule()));
//builder.Services.AddAutoMapper(typeof(DeactivateRoleRequestProfile));

builder.Services.AddAutoMapper(typeof(CompanyProfile));
builder.Services.AddAutoMapper(typeof(CategoryProfileDTO));
builder.Services.AddAutoMapper(typeof(CompanyFilterProfile));
builder.Services.AddAutoMapper(typeof(GetModulesByRoleIdProfile));
builder.Services.AddValidatorsFromAssemblyContaining<CompanyFilterRequestValidator>();
builder.Logging.ClearProviders();
Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(ConfigurationHelper.GetConfiguration())
    .Enrich.WithMachineName()
    .Enrich.WithThreadId()
    .WriteTo.Seq("http://localhost:5341/")
    .WriteTo.MSSqlServer(connectionString: ConfigurationHelper.GetConnectionString(),
                     restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information,
                   sinkOptions: new MSSqlServerSinkOptions { TableName = "Logs", AutoCreateSqlTable = true })
    .CreateLogger();
builder.Logging.AddSerilog();
Log.Information("starting App..");

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(Program)));

// Configure JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "KOG.ECommerce",
        ValidAudience = "KOG.ECommerce-Users",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Constants.SecretKey))
    };
});
// Swagger Configuration
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "KOG.ECommerce", Version = "v1" });

    // Configure Swagger to use JWT Authorization
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. Example: 'Bearer {token}'",
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});
FirebaseApp.Create(new AppOptions()
{
    Credential = GoogleCredential.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "kog-e-commerce-firebase-adminsdk-fbsvc-88c0f7200b.json")),
});

builder.Services.AddScoped<TransactionMiddleware>();
//builder.Services.AddScoped<BlacklistTokenMiddleware>();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

MapperHelper.Mapper = app.Services.GetService<IMapper>();
//app.UseMiddleware<BlacklistTokenMiddleware>();
app.UseMiddleware<GlobalErrorHandlerMiddleware>();
app.UseMiddleware<TransactionMiddleware>();

app.UseCors();

// Configure Authentication & Authorization
app.UseHttpsRedirection();
app.UseAuthentication(); // This is essential for JWT authentication
app.UseAuthorization();
app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "wwwroot", "Media")),
    RequestPath = "/Media",
    OnPrepareResponse = ctx =>
    {
        ctx.Context.Response.Headers.Append("Access-Control-Allow-Origin", "*");
    }
});
app.UseHttpsRedirection();

//app.UseAuthorization();

//  Run Seeder
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<Entities>();
    DbInitializer.Seed(dbContext);
}
app.MapControllers();
app.Run();