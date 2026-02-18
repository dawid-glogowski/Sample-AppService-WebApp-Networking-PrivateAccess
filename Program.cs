using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();
builder.Services.AddSingleton<ConnectivityTester>();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapGet("/api/test", async (string target, HttpClient client, ConnectivityTester tester) =>
{
    return Results.Json(await tester.TestConnectivity(target, client));
});

app.Run();
