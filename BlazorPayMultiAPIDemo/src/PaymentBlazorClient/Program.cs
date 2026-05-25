using PaymentBlazorClient.Services;

var builder = WebApplication.CreateBuilder(args);

// Set up Blazor Server with Razor Pages support
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Register the payment gateway client and point it at the gateway API base URL from appsettings.json
builder.Services.AddHttpClient<PaymentGatewayClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["PaymentGatewayApiBaseUrl"]!);
});

var app = builder.Build();

// In production, use a friendly error page and enforce HTTPS security headers
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Wire up the Blazor hub and set up the fallback page for all routes
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.Run();
