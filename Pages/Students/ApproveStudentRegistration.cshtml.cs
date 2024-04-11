using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SjcVotersPortal.Data;
using SjcVotersPortal.Data.Models;

namespace SjcVotersPortal.Pages.Students;

public class ApproveStudentRegistration : SiteadminBasePageModel
{
    private readonly ApplicationDbContext _context;

    public ApproveStudentRegistration(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<Student> Students { get;set; } = default!;

    public async Task OnGet()
    {
        Students = await _context.Students.OrderBy(student => student.RollNumber).ToListAsync();
    }

    public async Task<IActionResult> OnPostAsync(string? rollNumber, bool isApproved, string rejectionReason)
    {
        if (rollNumber == null)
        {
            return NotFound();
        }
        var student = await _context.Students.FirstOrDefaultAsync(e => e.RollNumber == rollNumber);
        if (student == null)
        {
            return NotFound();
        }

        student.IsApproved = isApproved;
        if (isApproved == false)
        {
            student.RejectionReason = rejectionReason;
        }
        await _context.SaveChangesAsync();

        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostDeleteRejected(string? rollNumber)
    {
        if (rollNumber == null)
        {
            return NotFound();
        }
        var student = await _context.Students.FirstOrDefaultAsync(e => e.RollNumber == rollNumber);
        if (student == null)
        {
            return NotFound();
        }

        if (student.IsApproved == false)
        {
            _context.RemoveRange(student);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage();
    }
}