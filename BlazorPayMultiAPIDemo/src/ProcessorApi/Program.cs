using ProcessorApi.Repositories;
using ProcessorApi.Services;
using ProcessorApi.Services.IService;

var builder = WebApplication.CreateBuilder(args);

// Standard API setup — controllers handle routing, Swagger lets us test the endpoint in the browser
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Transaction repository is singleton so it keeps all records in memory for the life of the app
builder.Services.AddSingleton<ITransactionRepository, InMemoryTransactionRepository>();

// Processor service is scoped — a new instance per request
builder.Services.AddScoped<IProcessorService, ProcessorService>();

var app = builder.Build();

// Only show Swagger in development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
