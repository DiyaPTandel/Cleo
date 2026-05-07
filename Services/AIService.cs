using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace cleo.Services;

public class AIService : IAIService
{
    private readonly IConfiguration _config;
    private readonly HttpClient _httpClient;

    public AIService(IConfiguration config)
    {
        _config = config;
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "CleoHealthApp/1.0");
    }

    public async Task<string> GetSymptomTipAsync(List<string> symptoms, string? notes)
    {
        if (symptoms == null || !symptoms.Any()) return GetFallbackSymptomTip(symptoms, notes);
        
        string prompt = $"As a health assistant, provide a short, helpful tip (max 2 sentences) for: {string.Join(", ", symptoms)}. Notes: {notes ?? "none"}. Focus on natural relief.";
        return await CallGeminiAPI(prompt, GetFallbackSymptomTip(symptoms, notes));
    }

    public async Task<string> GetMoodTipAsync(string mood, string? description)
    {
        string prompt = $"Provide a caring AI insight (max 2 sentences) for a user feeling '{mood}'. Description: {description ?? "none"}.";
        return await CallGeminiAPI(prompt, GetFallbackMoodTip(mood, description));
    }

    private async Task<string> CallGeminiAPI(string prompt, string fallback)
    {
        string apiKey = _config["Gemini:ApiKey"] ?? "";
        if (string.IsNullOrEmpty(apiKey)) return fallback;

        string[] models = { "gemini-2.0-flash", "gemini-flash-latest" };
        bool wasRateLimited = false;

        foreach (var model in models)
        {
            try
            {
                var requestBody = new { contents = new[] { new { parts = new[] { new { text = prompt } } } } };
                var json = JsonSerializer.Serialize(requestBody);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                
                string url = $"https://generativelanguage.googleapis.com/v1beta/models/{model}:generateContent?key={apiKey}";
                var response = await _httpClient.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    using var doc = JsonDocument.Parse(jsonResponse);
                    var text = doc.RootElement.GetProperty("candidates")[0].GetProperty("content").GetProperty("parts")[0].GetProperty("text").GetString();
                    if (!string.IsNullOrEmpty(text)) return text.Trim();
                }
                else if ((int)response.StatusCode == 429)
                {
                    wasRateLimited = true;
                    continue; 
                }
            }
            catch { continue; }
        }

        if (wasRateLimited)
        {
            return "✨ AI is resting: Cleo has reached her limit of tips for now. Please try again in 1 minute!";
        }

        return fallback;
    }

    private string GetFallbackSymptomTip(List<string>? symptoms, string? notes)
    {
        if (symptoms == null || !symptoms.Any()) return "Keep tracking your symptoms for insights!";
        return "Cleo Tip: Prioritize hydration and rest today. Tracking these patterns helps identify cycle health.";
    }

    private string GetFallbackMoodTip(string mood, string? description)
    {
        return $"Cleo Tip: You're feeling {mood}. Be gentle with yourself and notice how your cycle affects your energy.";
    }
}
