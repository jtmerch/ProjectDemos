using PaymentGatewayApi.Clients;
using PaymentGatewayApi.Repositories;
using PaymentGatewayApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IMerchantRepository, InMemoryMerchantRepository>();
builder.Services.AddScoped<IMerchantService, MerchantService>();
builder.Services.AddScoped<IFraudClient, FraudClient>();
builder.Services.AddScoped<IProcessorClient, ProcessorClient>();
builder.Services.AddScoped<IPaymentOrchestrationService, PaymentOrchestrationService>();
builder.Services.AddScoped<PaymentProcessorBase, SimulatedPaymentProcessor>();

builder.Services.AddHttpClient<IFraudClient, FraudClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["BackendApis:FraudApiBaseUrl"]!);
});

builder.Services.AddHttpClient<IProcessorClient, ProcessorClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["BackendApis:ProcessorApiBaseUrl"]!);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("BlazorClient", policy =>
    {
        policy.WithOrigins(builder.Configuration["Cors:BlazorClientOrigin"] ?? "https://localhost:7140")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("BlazorClient");
app.UseAuthorization();
app.MapControllers();
app.Run();

public partial class Program { }
