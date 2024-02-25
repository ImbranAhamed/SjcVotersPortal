using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SjcVotersPortal.Data;
using SjcVotersPortal.Data.Models;

namespace SjcVotersPortal.Pages.Students;


public class StudentProfile : StudentBasePageModel
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