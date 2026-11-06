using HydroSentinel.Services;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HydroSentinel.Pages.Admin;

public class AdminPageModel(IAdminPortalProjection portal, IDemoMode demo) : PageModel
{
    public IAdminPortalProjection Portal { get; } = portal;
    public AdminPortalSnapshot Data => Portal.Snapshot;
    public bool IsDemo => Portal.IsDevelopmentDemo;
    [BindProperty(SupportsGet = true)] public string? Id { get; set; }
    [BindProperty] public AdminLoginInput Input { get; set; } = new();
    public void OnGet() { }

    public IActionResult OnPostSignIn()
    {
        if (!IsDemo)
        {
            ModelState.AddModelError(string.Empty, "Administrator authentication is not connected in this environment.");
            return Page();
        }

        if (!ModelState.IsValid) return Page();
        TempData["AdminDemoSignedIn"] = true;
        return RedirectToPage("/Admin/Dashboard");
    }

    public IActionResult OnPostMarkNotificationsRead()
    {
        if (!IsDemo) return NotFound();
        demo.MarkNotificationsRead();
        return RedirectToPage("/Admin/Notifications");
    }
}

public sealed class AdminLoginInput
{
    [Required, EmailAddress, Display(Name = "Email address")]
    public string Email { get; set; } = string.Empty;

    [Required, MinLength(8), DataType(DataType.Password), Display(Name = "Password")]
    public string Password { get; set; } = string.Empty;

    public bool RememberMe { get; set; }
}
