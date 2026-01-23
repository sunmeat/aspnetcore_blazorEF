using System.ComponentModel.DataAnnotations;

namespace BlazorApp1.Models;

public class ContactInfoEntity
{
    public int Id { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [EmailAddress]
    public string? AlternativeEmail { get; set; }

    public string? Phone { get; set; }

    [MaxLength(500)]
    public string? Bio { get; set; }

    [Range(0, 50)]
    public int YearsOfExperience { get; set; }

    public DateTime? BirthDate { get; set; }

    [Required]
    public string FavoriteTech { get; set; } = string.Empty;

    [Required]
    public string RenderModePreference { get; set; } = "Auto";

    public bool ShowPhone { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}