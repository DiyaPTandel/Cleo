using System.ComponentModel.DataAnnotations;

namespace cleo.Models;

public class SymptomLog
{
    [Key]
    public int Id { get; set; }
    [Required]
    public int UserId { get; set; }
    [Required]
    public string Date { get; set; } = string.Empty;
    public string Symptoms { get; set; } = string.Empty; // Store as comma-separated string
    public string? Notes { get; set; }
    public string? AITip { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
