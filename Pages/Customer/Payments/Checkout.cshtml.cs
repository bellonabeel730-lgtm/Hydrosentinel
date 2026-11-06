using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HydroSentinel.Services;

namespace HydroSentinel.Pages.Customer.Payments;

public class CheckoutModel(IDemoMode demo, PlanCatalog catalog) : PageModel
{
    [BindProperty(SupportsGet = true, Name = "plan")]
    public string? Plan { get; set; }
    [BindProperty] public string Outcome { get; set; } = "success";
    public PlanDefinition? SelectedPlan => catalog.Find(Plan);
    public string? Message { get; private set; }
    public IActionResult OnPost()
    {
        if (!demo.Enabled || SelectedPlan is null) return Page();
        var payment = demo.SimulatePayment(SelectedPlan.Id, Outcome);
        return RedirectToPage("/Customer/Payments/Result", new { reference = payment.Reference });
    }
}
