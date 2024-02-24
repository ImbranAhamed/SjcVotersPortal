using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SjcVotersPortal.Data;

namespace SjcVotersPortal.Pages.Students;

[Authorize(Roles = NamedConstants.RoleNames.Student)]
public class StudentProfile : PageModel
{
    private readonly ApplicationDbContext _context;
    public Student Student { get; set; }

    public StudentProfile(ApplicationDbContext context)
    {
        _context = context;
    }
    public void OnGet()
    {
        Student = _context.Students.Single(e => e.EmailId.ToLower() == User.Identity!.Name.ToLower());
    }
}