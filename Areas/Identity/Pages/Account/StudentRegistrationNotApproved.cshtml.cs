using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SjcVotersPortal.Areas.Identity.Pages.Account;

[AllowAnonymous]
public class StudentRegistrationNotApproved : PageModel
{
    public bool? IsApproved { get; set; }
    public void OnGet(bool? isApproved)
    {
        IsApproved = isApproved;
    }
}