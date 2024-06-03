using Assignment_3;
using Assignment_3.DataAccess;
using Assignment_3.Middlewares;
using Assignment_3.Services;
using Assignment_3.Services.IServices;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MovieRentalDbContext>
    (options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString("Default"));
    });
builder.Services.AddScoped<GlobalExceptionHandlingMiddleware>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IRentalService, RentalService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();
