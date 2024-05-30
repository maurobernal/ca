using ca.Infrastructure;
using ca.Infrastructure.Data;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
Console.WriteLine("Enviroment:" + builder.Environment.EnvironmentName);

// Add services to the container.



builder.Services.AddHashiVaultServices(builder.Configuration);

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddWebServices();

//Logger with Serilog
var serverSeq = configuration.GetConnectionString("Seq") ?? string.Empty;
builder.Host.UseSerilog((context, configurator) =>
configurator
                // added new properties
                .Enrich.WithMachineName()
                .Enrich.WithThreadName()
                .Enrich.WithThreadId()
                .Enrich.FromLogContext()
                .Enrich.WithProperty("service", "ca-api")

                // destiny
                .WriteTo.File(@"/logs/events.txt", rollingInterval: RollingInterval.Day)
                .WriteTo.Console()
                .WriteTo.Seq(serverSeq)

);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    await app.ApplyMigrations();
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
if (bool.Parse(builder.Configuration["Settings:ApplyMigrations"] ?? "false"))
    await app.ApplyMigrations();

if (bool.Parse(builder.Configuration["Settings:ApplySeeds"] ?? "false"))
    await app.LoadSeeds();


app.UseHealthChecks("/health");
app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseSwaggerUi(settings =>
{
    settings.Path = "/api";
    settings.DocumentPath = "/api/specification.json";
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapRazorPages();

app.MapFallbackToFile("index.html");

app.UseExceptionHandler(options => { });

app.Map("/", () => Results.Redirect("/api"));

app.MapEndpoints();

app.Run();

public partial class Program { }
