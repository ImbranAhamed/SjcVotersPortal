using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SjcVotersPortal.Data;

namespace SjcVotersPortal.Pages.Students;

public class Index : SiteadminBasePageModel
{
    private readonly ApplicationDbContext _context;

    public Index(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<Student> Students { get;set; } = default!;

    public async Task OnGet()
    {
        Students = await _context.Students.ToListAsync();
    }
}