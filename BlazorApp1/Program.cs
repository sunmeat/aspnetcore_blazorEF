using BlazorApp1.Components;
using BlazorApp1.Data;
using Blazored.Toast;
using Microsoft.EntityFrameworkCore;

// <BlazoredToasts @rendermode="new InteractiveAutoRenderMode()" додано в Components / App.razor

// додано контролер для API в Controllers / ContactController.cs

// додано модель даних в Models / ContactInfoEntity.cs

// додано DbContext в Data / AppDbContext.cs та налаштування рядка підключення в appsettings.json

// для роботи з EF Core додано пакети:
// dotnet add package Microsoft.EntityFrameworkCore.SqlServer
// dotnet add package Microsoft.EntityFrameworkCore.Tools         
// dotnet add package Microsoft.EntityFrameworkCore.Design   

// треба створити міграцію та застосовати її до БД:
// dotnet ef migrations add InitialCreate
// dotnet ef database update !!!!!!!!!!!!!!!!!!!!!!!!!!
namespace BlazorApp1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents()
                .AddInteractiveWebAssemblyComponents();

            builder.Services.AddBlazoredToast();

            // HttpClient для Server-side компонентів
            builder.Services.AddScoped(sp =>
                new HttpClient
                {
                    BaseAddress = new Uri("https://localhost:7032/")
                });

            // ВАЖЛИВО: додаємо контролери для API
            builder.Services.AddControllers();

            // додаємо CORS (для реального застосунку варто налаштувати політику більш детально)
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            // DbContext
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Рядок 'DefaultConnection' не знайдено.");

            builder.Services.AddDbContextFactory<AppDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
                if (builder.Environment.IsDevelopment())
                {
                    options.EnableSensitiveDataLogging();
                    options.EnableDetailedErrors();
                }
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseCors();

            app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
            app.UseAntiforgery();
            app.MapStaticAssets();

            // ВАЖЛИВО: мапимо контролери
            app.MapControllers();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode()
                .AddInteractiveWebAssemblyRenderMode()
                .AddAdditionalAssemblies(typeof(Client._Imports).Assembly);

            app.Run();
        }
    }
}
