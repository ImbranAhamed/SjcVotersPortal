using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SjcVotersPortal.Data;

namespace SjcVotersPortal.Pages.Associations
{
    public class DeleteModel : SiteadminBasePageModel
    {
        private readonly SjcVotersPortal.Data.ApplicationDbContext _context;

        public DeleteModel(SjcVotersPortal.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Association Association { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var association = await _context.Associations.FirstOrDefaultAsync(m => m.Id == id);

            if (association == null)
            {
                return NotFound();
            }
            else
            {
                Association = association;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var association = await _context.Associations.FindAsync(id);
            if (association != null)
            {
                Association = association;
                _context.Associations.Remove(Association);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
