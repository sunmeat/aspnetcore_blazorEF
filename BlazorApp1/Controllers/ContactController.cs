using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlazorApp1.Data;
using BlazorApp1.Models;

[ApiController]
[Route("api/contact")]
public class ContactController : ControllerBase
{
    private readonly IDbContextFactory<AppDbContext> _dbFactory;

    public ContactController(IDbContextFactory<AppDbContext> dbFactory)
    {
        _dbFactory = dbFactory;
    }

    [HttpPost]
    [Consumes("application/json", "text/json", "application/json;charset=utf-8")]
    public async Task<IActionResult> SaveContact([FromBody] ContactPayload payload)
    {
        // Console.WriteLine($"Реальний Content-Type: '{Request.ContentType ?? "відсутній"}'");
        // Console.OutputEncoding = System.Text.Encoding.UTF8;
        // Console.WriteLine("=== POST /api/contact викликано ===");
        // Console.WriteLine($"Content-Length: {Request.ContentLength ?? -1}");
        // Console.WriteLine($"Body (перші 200 символів):");
        // Request.EnableBuffering();
        // using var reader = new StreamReader(Request.Body);
        // var body = await reader.ReadToEndAsync();
        // Console.WriteLine(body.Substring(0, Math.Min(200, body.Length)));
        // Request.Body.Position = 0; // повертаємо позицію для подальшого читання
        // Console.WriteLine($"Body length: {Request.ContentLength ?? 0}");

        if (payload == null)
        {
            return BadRequest("Payload не прийшов");
        }

        // Console.WriteLine($"Email: {payload.Email}");
        // Console.WriteLine($"AlternativeEmail: {payload.AlternativeEmail}");
        // Console.WriteLine($"Phone: {payload.Phone}");

        await using var context = await _dbFactory.CreateDbContextAsync();

        var entity = new ContactInfoEntity
        {
            Email = payload.Email,
            AlternativeEmail = payload.AlternativeEmail,
            Phone = payload.Phone,
            Bio = payload.Bio,
            YearsOfExperience = payload.YearsOfExperience,
            BirthDate = payload.BirthDate,
            FavoriteTech = payload.FavoriteTech,
            RenderModePreference = payload.RenderModePreference,
            ShowPhone = payload.ShowPhone
        };

        context.ContactInfos.Add(entity);
        await context.SaveChangesAsync();

        return Ok(new { Id = entity.Id, Message = "Збережено" });
    }
}

public class ContactPayload
{
    public string Email { get; set; } = string.Empty;
    public string? AlternativeEmail { get; set; }
    public string? Phone { get; set; }
    public string? Bio { get; set; }
    public int YearsOfExperience { get; set; }
    public DateTime? BirthDate { get; set; }
    public string FavoriteTech { get; set; } = string.Empty;
    public string RenderModePreference { get; set; } = "Auto";
    public bool ShowPhone { get; set; }
}