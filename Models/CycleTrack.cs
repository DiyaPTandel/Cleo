using System.ComponentModel.DataAnnotations;

namespace cleo.Models;

public class CycleTrack
{
    [Key]
    public int Id { get; set; }
    [Required]
    public int UserId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsCurrentCycle { get; set; } = false;
    public string? Flow { get; set; }
}
