using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SjcVotersPortal.Data;
using SjcVotersPortal.Data.Models;

namespace SjcVotersPortal.Pages_Elections
{
    public class DeleteModel : PageModel
    {
        private readonly SjcVotersPortal.Data.ApplicationDbContext _context;

        public DeleteModel(SjcVotersPortal.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Election Election { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var election = await _context.Elections.Include(e => e.Association).FirstOrDefaultAsync(m => m.Id == id);

            if (election == null)
            {
                return NotFound();
            }
            else
            {
                Election = election;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var election = await _context.Elections.FindAsync(id);
            if (election != null)
            {
                Election = election;
                _context.Elections.Remove(Election);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
