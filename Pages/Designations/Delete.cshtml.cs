using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SjcVotersPortal.Data;
using SjcVotersPortal.Data.Models;

namespace SjcVotersPortal.Pages.Designations
{
    public class DeleteModel : SiteadminBasePageModel
    {
        private readonly SjcVotersPortal.Data.ApplicationDbContext _context;

        public DeleteModel(SjcVotersPortal.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Designation Designation { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var designation = await _context.Designations.FirstOrDefaultAsync(m => m.Id == id);

            if (designation == null)
            {
                return NotFound();
            }
            else
            {
                Designation = designation;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var designation = await _context.Designations.FindAsync(id);
            if (designation != null)
            {
                Designation = designation;
                _context.Designations.Remove(Designation);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
