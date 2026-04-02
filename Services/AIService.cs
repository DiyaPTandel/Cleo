using System.Text;

namespace cleo.Services;

public class AIService : IAIService
{
    public Task<string> GetSymptomTipAsync(List<string> symptoms, string? notes)
    {
        if (symptoms == null || !symptoms.Any())
        {
            return Task.FromResult("Keep tracking your symptoms to get personalized insights!");
        }

        var sb = new StringBuilder();
        sb.Append("Based on your symptoms: ");

        foreach (var symptom in symptoms)
        {
            switch (symptom.ToLower())
            {
                case "cramps":
                    sb.Append("Try a warm compress or a heating pad to relax your muscles. ");
                    break;
                case "bloating":
                    sb.Append("Reducing salt intake and staying hydrated can help with bloating. ");
                    break;
                case "headache":
                    sb.Append("Ensure you're getting enough rest and staying hydrated. ");
                    break;
                case "fatigue":
                    sb.Append("Listen to your body and prioritize extra sleep today. ");
                    break;
                case "anxiety":
                    sb.Append("Try some deep breathing exercises or gentle meditation. ");
                    break;
                case "mood swings":
                    sb.Append("Be kind to yourself; hormonal shifts can affect your emotions. ");
                    break;
                case "high energy":
                    sb.Append("This is a great time for more intense workouts! ");
                    break;
                case "increased appetite":
                    sb.Append("Focus on nutrient-dense meals with fiber and protein. ");
                    break;
            }
        }

        if (!string.IsNullOrEmpty(notes))
        {
            sb.Append("Regarding your notes, remember that tracking these details helps identify patterns over time.");
        }

        return Task.FromResult(sb.ToString().Trim());
    }

    public Task<string> GetMoodTipAsync(string mood, string? description)
    {
        string tip = mood.ToLower() switch
        {
            "happy" or "energetic" or "calm" => "It's great that you're feeling good! Capture this positive energy and perhaps try something creative today.",
            "sad" or "depressed" or "low" => "It's okay to have low days. Try to do one small thing that brings you comfort, like listening to your favorite music or a short walk.",
            "stressed" or "anxious" or "irritable" => "Take a moment to breathe. A 5-minute mindfulness session could help ground you.",
            "tired" or "exhausted" => "Your body needs rest. Aim for an earlier bedtime tonight if possible.",
            _ => "Tracking your mood is a great step toward emotional well-of being. Notice if any specific activities affect how you feel."
        };

        return Task.FromResult(tip);
    }
}
