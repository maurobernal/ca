using ca.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

Console.WriteLine("Enviroment:" + builder.Environment.EnvironmentName);

// Add services to the container.
builder.Services.AddKeyVaultIfConfigured(builder.Configuration);

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddWebServices();

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
if(bool.Parse(builder?.Configuration["Settings:ApplyMigrations"]?? "false"))
    await app.ApplyMigrations();

if (bool.Parse(builder?.Configuration["Settings:ApplySeeds"] ?? "false"))
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
