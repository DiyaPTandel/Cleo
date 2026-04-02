namespace cleo.Services;

public interface IAIService
{
    Task<string> GetSymptomTipAsync(List<string> symptoms, string? notes);
    Task<string> GetMoodTipAsync(string mood, string? description);
}
