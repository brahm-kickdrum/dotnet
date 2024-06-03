using Assignment_2.Middleware;
using Assignment_2.Repository;
using Assignment_2.Repository.IRepository;
using Assignment_2.Service;
using Assignment_2.Service.IService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
 .AddJwtBearer(options =>
 {
     options.TokenValidationParameters = new TokenValidationParameters
     {
         ValidateIssuer = true,
         ValidateAudience = true,
         ValidateLifetime = true,
         ValidateIssuerSigningKey = true,
         ValidIssuer = builder.Configuration.GetSection("Jwt:Issuer").Get<string>(),
         ValidAudience = builder.Configuration.GetSection("Jwt:Audience").Get<string>(),
         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt:Key").Get<string>()))
     };
 });

builder.Services.AddControllers();
builder.Services.AddSingleton<IAuthService, AuthService>();
builder.Services.AddSingleton<IUserDataService, UserDataService>();
builder.Services.AddSingleton<IAuthRepository, AuthRepository>();
builder.Services.AddSingleton<IUserDataRepository, UserDataRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<GlobalExceptionHandlingMiddleware>();
builder.Services.AddLogging(); 
builder.Services.AddSingleton<ILogger<GlobalExceptionHandlingMiddleware>, Logger<GlobalExceptionHandlingMiddleware>>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();
