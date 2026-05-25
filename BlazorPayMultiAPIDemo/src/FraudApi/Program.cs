using FraudApi.Services;
using FraudApi.Services.IService;

var builder = WebApplication.CreateBuilder(args);

// Standard API setup — controllers handle routing, Swagger lets us test endpoints in the browser
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register the fraud service so it can be injected wherever it's needed
builder.Services.AddScoped<IFraudService, FraudService>();

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
