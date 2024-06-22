using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SampleWebApp.Model;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Create a SecretClient using the managed identity for authentication
var keyVaultUrl = new Uri("https://kvyoutubedemowithdotnet.vault.azure.net/");
var secretClient = new SecretClient(keyVaultUrl, new DefaultAzureCredential());

// Retrieve the secret by name
KeyVaultSecret secret = secretClient.GetSecret("KeyVaultDemo-ConnectionStrings--DefaultConnection");

// Access the secret value
string defaultConnectionString = secret.Value;

// Other service configurations
builder.Services.AddSingleton<DAL>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
