using API.Extensions;
using API.Middleware;
using BAL.Helper;
using Domain.CommonEntity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using NLog;
using NLog.Web;
using Domain.EntityModel;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin",
         options =>
         options.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
});
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "SmsDigitalApi", Version = "v1" });
    c.AddSecurityDefinition("Bearer",
    new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        In = ParameterLocation.Header,
        Description = "Authorization header using the Bearer scheme."
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
                        Array.Empty<string>()
                    }
                });
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
ConfigurationManager Configuration = builder.Configuration;



builder.Services.Configure<ConnectionStrings>(Configuration.GetSection("ConnectionStrings"));
builder.Services.Configure<JwtSetting>(Configuration.GetSection("JwtSetting"));
builder.Services.Configure<EncryptionSetting>(Configuration.GetSection("EncryptionSetting"));
builder.Services.Configure<UserEntityModel>(Configuration.GetSection("UserEntityModel"));


builder.Services.AddAuthorization(options =>
{
    options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
   .RequireAuthenticatedUser().Build();
});

builder.Services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
var appSettingsSection = Configuration.GetSection("AppSettings");

var appSettings = appSettingsSection.Get<AppSettings>();

builder.Services.RegisterDependencies(Configuration);



var jwtSecretKey = Encoding.ASCII.GetBytes(appSettings.Secret);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
           .AddJwtBearer(x =>
           {
               x.Authority = "";
               x.RequireHttpsMetadata = false;
               x.SaveToken = true;
               x.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(jwtSecretKey),
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidIssuer = appSettings.Issuer,
                   ValidAudience = appSettings.Audience,
               };
           });
builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
builder.Host.UseNLog();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCorrelationId();
app.UseRequestResponseLogging();
var logger = app.Services.GetRequiredService<ILogger<Logger>>();
app.ConfigureExceptionHandler(logger);
app.MapControllers();

app.Run();
