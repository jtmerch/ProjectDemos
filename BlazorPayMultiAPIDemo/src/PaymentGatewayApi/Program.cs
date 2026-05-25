using PaymentGatewayApi.Clients;
using PaymentGatewayApi.Repositories;
using PaymentGatewayApi.Repositories.IService;
using PaymentGatewayApi.Services;
using PaymentGatewayApi.Services.IService;

var builder = WebApplication.CreateBuilder(args);

// Standard API setup — controllers, Swagger for testing endpoints in the browser
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// In-memory cache used for idempotency — stores responses so duplicate requests don't reprocess
builder.Services.AddMemoryCache();

// Register all our services and repositories with the dependency injection container
builder.Services.AddScoped<IMerchantRepository, InMemoryMerchantRepository>();
builder.Services.AddScoped<IMerchantService, MerchantService>();
builder.Services.AddScoped<IFraudClient, FraudClient>();
builder.Services.AddScoped<IProcessorClient, ProcessorClient>();
builder.Services.AddScoped<IPaymentOrchestrationService, PaymentOrchestrationService>();
builder.Services.AddScoped<PaymentProcessorBase, SimulatedPaymentProcessor>();

// Set up the HTTP client for calling the Fraud API — base URL comes from appsettings.json
builder.Services.AddHttpClient<IFraudClient, FraudClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["BackendApis:FraudApiBaseUrl"]!);
});

// Set up the HTTP client for calling the Processor API — base URL comes from appsettings.json
builder.Services.AddHttpClient<IProcessorClient, ProcessorClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["BackendApis:ProcessorApiBaseUrl"]!);
});

// CORS policy so the Blazor client is allowed to call this API
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

// Only expose Swagger UI in development — not in production
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
