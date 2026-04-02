using System.ComponentModel.DataAnnotations;

namespace cleo.Models;

public class MoodNote
{
    [Key]
    public int Id { get; set; }
    [Required]
    public int UserId { get; set; }
    [Required]
    public string Mood { get; set; } = "Happy"; // Happy, Neutral, Sad, etc.
    public string? Note { get; set; }
    public string? AITip { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;
}
