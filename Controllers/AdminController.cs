using Microsoft.AspNetCore.Mvc;
using cleo.Data;
using cleo.Models;
using Microsoft.EntityFrameworkCore;

namespace cleo.Controllers;

public class AdminController : Controller
{
    private readonly CleoDbContext _db;

    public AdminController(CleoDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public IActionResult Index() => RedirectToAction(nameof(Dashboard));

    [HttpGet]
    public IActionResult Login() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(string email, string password)
    {
        // Simple authentication check against the database
        var admin = await _db.Admins.FirstOrDefaultAsync(a => a.Email == email && a.Password == password);
        if (admin != null)
        {
            HttpContext.Session.SetString("Role", "Admin");
            HttpContext.Session.SetString("Email", admin.Email);
            HttpContext.Session.SetString("AdminName", admin.Name);
            return RedirectToAction(nameof(Dashboard));
        }

        ViewBag.Error = "Invalid credentials";
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Dashboard()
    {
        var guard = EnsureAdminSession();
        if (guard != null) return guard;

        ViewBag.AdminEmail = HttpContext.Session.GetString("Email") ?? "admin@cleo.app";
        ViewBag.Stats = new
        {
            TotalUsers = await _db.Users.CountAsync(),
            ActiveUsers = await _db.Users.CountAsync(u => u.Status == AccountStatus.Active),
            CyclesLogged = await _db.CycleTracks.CountAsync(),
            SystemHealth = "98%",
            StorageUsed = "42%"
        };

        var latestUsers = await _db.Users.OrderByDescending(u => u.Id).Take(3).ToListAsync();
        var recentActivity = latestUsers.Select(u => new { 
            User = u.Name, 
            Action = "Joined CLEO", 
            Time = u.JoinDate.ToString("MMM dd, yyyy") 
        }).ToList();
        
        if (!recentActivity.Any())
        {
            recentActivity.Add(new { User = "System", Action = "DB Initialized", Time = "10 mins ago" });
        }

        ViewBag.RecentActivity = recentActivity;

        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Users()
    {
        var guard = EnsureAdminSession();
        if (guard != null) return guard;

        ViewBag.Stats = new
        {
            TotalUsers = await _db.Users.CountAsync(),
            ActiveUsers = await _db.Users.CountAsync(u => u.Status == AccountStatus.Active),
            BlockedUsers = await _db.Users.CountAsync(u => u.Status == AccountStatus.Blocked)
        };

        ViewBag.UsersList = await _db.Users.ToListAsync();
        return View();
    }
    

    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditUser(UserAccount model)
    {
        var guard = EnsureAdminSession();
        if (guard != null) return guard;
        
        if (!ModelState.IsValid) return View(model);
        
        var existingUser = await _db.Users.FindAsync(model.Id);
        if (existingUser == null) return NotFound();
        
        existingUser.Name = model.Name;
        existingUser.Status = model.Status;
        existingUser.CycleLength = model.CycleLength;
        existingUser.PeriodLength = model.PeriodLength;
        
        await _db.SaveChangesAsync();
        TempData["AdminMessage"] = "User updated successfully!";
        return RedirectToAction(nameof(Users));
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> BlockUser(int id)
    {
        var guard = EnsureAdminSession();
        if (guard != null) return guard;
        
        var user = await _db.Users.FindAsync(id);
        if (user != null)
        {
            user.Status = (user.Status == AccountStatus.Blocked) ? AccountStatus.Active : AccountStatus.Blocked;
            await _db.SaveChangesAsync();
            TempData["AdminMessage"] = $"User status changed to {user.Status}!";
        }
        return RedirectToAction(nameof(Users));
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var guard = EnsureAdminSession();
        if (guard != null) return guard;
        
        var user = await _db.Users.FindAsync(id);
        if (user != null)
        {
            _db.Users.Remove(user);
            await _db.SaveChangesAsync();
            TempData["AdminMessage"] = "User deleted successfully!";
        }
        return RedirectToAction(nameof(Users));
    }



    [HttpGet]
    public async Task<IActionResult> Content()
    {
        var guard = EnsureAdminSession();
        if (guard != null) return guard;

        ViewBag.Stats = new
        {
            TotalArticles = await _db.Articles.CountAsync(),
            Published = await _db.Articles.CountAsync(a => a.Status == "Published"),
            Drafts = await _db.Articles.CountAsync(a => a.Status == "Draft"),
            Categories = await _db.Articles.Select(a => a.Category).Distinct().CountAsync()
        };

        ViewBag.ArticlesList = await _db.Articles.ToListAsync();
        return View();
    }
    

    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditArticle(ContentArticle model)
    {
        var guard = EnsureAdminSession();
        if (guard != null) return guard;
        
        if (!ModelState.IsValid) return View(model);
        
        var existing = await _db.Articles.FindAsync(model.Id);
        if (existing == null) return NotFound();
        
        existing.Title = model.Title;
        existing.Category = model.Category;
        existing.Content = model.Content;
        existing.Status = model.Status;
        
        await _db.SaveChangesAsync();
        TempData["AdminMessage"] = "Article updated successfully!";
        return RedirectToAction(nameof(Content));
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteArticle(int id)
    {
        var guard = EnsureAdminSession();
        if (guard != null) return guard;
        
        var article = await _db.Articles.FindAsync(id);
        if (article != null)
        {
            _db.Articles.Remove(article);
            await _db.SaveChangesAsync();
            TempData["AdminMessage"] = "Article deleted successfully!";
        }
        return RedirectToAction(nameof(Content));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddArticle(string title, string category, string content)
    {
        var guard = EnsureAdminSession();
        if (guard != null) return guard;

        if (!string.IsNullOrEmpty(title) && !string.IsNullOrEmpty(content))
        {
            _db.Articles.Add(new ContentArticle { Title = title, Category = category ?? "General", Content = content });
            await _db.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Content));
    }

    [HttpGet]
    public IActionResult Settings(string tab = "general")
    {
        var guard = EnsureAdminSession();
        if (guard != null) return guard;

        ViewBag.Tab = tab.ToLower();
        ViewBag.Settings = new
        {
            SystemName = "CLEO Admin Portal",
            AdminEmail = "admin@cleo.app",
            EnableLogs = true,
            MaintenanceMode = false
        };

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Logout()
    {
        var guard = EnsureAdminSession();
        if (guard != null) return guard;

        HttpContext.Session.Clear();
        return RedirectToAction(nameof(Login));
    }

    private IActionResult? EnsureAdminSession()
    {
        var role = HttpContext.Session.GetString("Role");
        return string.Equals(role, "Admin", StringComparison.OrdinalIgnoreCase)
            ? null
            : RedirectToAction(nameof(Login));
    }
}
