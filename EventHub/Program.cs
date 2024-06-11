using EventHub.Middlewares;
using EventHub.Services.Implementations;
using EventHub.Services.IServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<GlobalExceptionHandlingMiddleware>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IKeyVaultService, KeyVaultService>();
builder.Services.AddScoped<IEventProcessorService, EventProcessorService>();
builder.Services.AddScoped<IEventProducerService, EventProducerService>();

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
