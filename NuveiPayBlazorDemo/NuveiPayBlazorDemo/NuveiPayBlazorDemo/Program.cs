using NuveiPayBlazorDemo.Components;
using NuveiPayBlazorDemo.Services;
using NuveiPayBlazorDemo.Services.Interface;
using NuveiPayBlazorDemo.Shared.Utility;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(); //Add controllers - manual step

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

//Nuvei Trans demo information
builder.Services.AddScoped<INuveiProcess, NuveiProcess>();
builder.Services.AddScoped<INuveiTransPOST, NuveiTransPOST>();
builder.Services.AddScoped<INuveiSQLMerchantData, NuveiSQLMerchantData>();

builder.Services.AddHttpClient("NuveiTransaction", client =>
{
    client.Timeout = TimeSpan.FromSeconds(30);
});


AppDefinitions.NuveiCCAPI = "https://ppp-test.nuvei.com/ppp/api/v1/";
AppDefinitions.NuveiTokenURL = "";
AppDefinitions.NuveiSessionURL = "https://ppp-test.nuvei.com/ppp/api/v1/getSessionToken.do";
AppDefinitions.NuveiTransactionDetailsURL = "https://ppp-test.nuvei.com/ppp/api/v1/getTransactionDetails.do";




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(NuveiPayBlazorDemo.Client._Imports).Assembly);

app.MapControllers(); //manual step to add controlelrs

app.Run();
