using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SjcVotersPortal.Pages;

[Authorize(Roles = NamedConstants.RoleNames.Student)]
public class StudentBasePageModel : PageModel
{

}