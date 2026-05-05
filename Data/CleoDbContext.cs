using Microsoft.EntityFrameworkCore;
using cleo.Models;

namespace cleo.Data;

public class CleoDbContext : DbContext
{
    public CleoDbContext(DbContextOptions<CleoDbContext> options)
        : base(options)
    {
    }

    public DbSet<UserAccount> Users { get; set; } = null!;
    public DbSet<AdminMember> Admins { get; set; } = null!;
    public DbSet<ContentArticle> Articles { get; set; } = null!;
    public DbSet<CycleTrack> CycleTracks { get; set; } = null!;
    public DbSet<MoodNote> MoodNotes { get; set; } = null!;
    public DbSet<SymptomLog> SymptomLogs { get; set; } = null!;
    public DbSet<Reminder> Reminders { get; set; } = null!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Seed some data for initial state
        modelBuilder.Entity<AdminMember>().HasData(
            new AdminMember { Id = 1, Name = "Admin", Email = "admin@cleo.app", Password = "password123", IsSuperAdmin = true },
            new AdminMember { Id = 2, Name = "Diya", Email = "diya@cleo.app", Password = "password123", IsSuperAdmin = true },
            new AdminMember { Id = 3, Name = "Hensy", Email = "hensy@cleo.app", Password = "password123", IsSuperAdmin = true }
        );

        modelBuilder.Entity<ContentArticle>().HasData(
            new ContentArticle { Id = 1, Title = "Focus on Iron-Rich Foods", Category = "Quick-Nutrition", Content = "During your period, your body loses iron. Focus on leafy greens, lentils, and lean meats to maintain energy levels.", Views = 1240 },
            new ContentArticle { Id = 2, Title = "Yoga for Cramp Relief", Category = "Exercise", Content = "Gentle yoga poses like Child's Pose and Cat-Cow can help relax pelvic muscles and reduce menstrual cramping.", Views = 952 },
            new ContentArticle { Id = 3, Title = "Understanding LH Surge", Category = "Wellness", Content = "The Luteinizing Hormone (LH) surge triggers ovulation. Tracking this can help you identify your most fertile days.", Views = 1520 },
            new ContentArticle { Id = 4, Title = "Managing PMS Bloating", Category = "PMS-Relief", Content = "Reduce salt intake and stay hydrated to combat hormonal water retention during the luteal phase.", Views = 840 },
            new ContentArticle { Id = 5, Title = "Magnesium for Mood", Category = "Quick-Nutrition", Content = "Magnesium-rich foods like dark chocolate and pumpkin seeds can help stabilize mood swings and reduce anxiety.", Views = 620 },
            new ContentArticle { Id = 6, Title = "The Power of Walking", Category = "Exercise", Content = "A 20-minute brisk walk increases blood flow and releases endorphins, which can alleviate low-level period pain.", Views = 750 },
            new ContentArticle { Id = 7, Title = "The Follicular Phase", Category = "Wellness", Content = "Estrogen rises during this phase, often leading to increased energy and improved cognitive focus. It's a great time for new projects.", Views = 1100 },
            new ContentArticle { Id = 8, Title = "Sleep Hygiene & Cycles", Category = "Hygiene", Content = "Your body temperature changes throughout your cycle, which can affect sleep. Keep your room cool during the luteal phase.", Views = 430 },
            new ContentArticle { Id = 9, Title = "Herbal Tea Benefits", Category = "Quick-Nutrition", Content = "Raspberry leaf and ginger teas are known for their ability to soothe the uterus and reduce inflammation.", Views = 510 },
            new ContentArticle { Id = 10, Title = "Strength Training Timing", Category = "Exercise", Content = "You might feel strongest during your follicular phase. High-intensity training is often most effective during this time.", Views = 890 }
        );
    }
}
