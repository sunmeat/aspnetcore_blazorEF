using Blazored.Toast; // dotnet add package Blazored.Toast
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
// dotnet add package Microsoft.Extensions.Logging.Console
// dotnet add package Microsoft.Extensions.Http

namespace BlazorApp1.Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            // конфігурація HttpClient для Blazor WebAssembly
            builder.Services.AddScoped(sp =>
                new HttpClient
                {
                    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) // зазвичай це буде щось на кшталт "https://localhost:7032/"
                });

            builder.Services.AddBlazoredToast();

            await builder.Build().RunAsync();
        }
    }
}

// всі основні зміни у файлі ContactInfo.razor