using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HydroSentinel.Services;

namespace HydroSentinel.Pages.Customer.Payments;

public class ResultModel(IDemoMode demo) : PageModel
{
    [BindProperty(SupportsGet = true, Name = "reference")]
    public string? Reference { get; set; }

    public bool HasReference => !string.IsNullOrWhiteSpace(Reference) && Reference.Length <= 256;
    public bool HasInvalidReference => !string.IsNullOrWhiteSpace(Reference) && Reference.Length > 256;
    public DemoPayment? Payment => demo.Snapshot.Payments.FirstOrDefault(x => x.Reference.Equals(Reference, StringComparison.OrdinalIgnoreCase));
}
