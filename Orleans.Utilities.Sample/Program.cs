using Orleans.Utilities;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOrleans(orleans =>
{
    orleans.UseLocalhostClustering();
    orleans.AddMemoryGrainStorageAsDefault();
});

builder.Services.AddGrainIdentifier();
builder.Services.AddGrainLocators();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();

public partial class Program2 {}