using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SjcVotersPortal.Data;

namespace SjcVotersPortal.Pages.Designations
{
    public class DetailsModel : PageModel
    {
        private readonly SjcVotersPortal.Data.ApplicationDbContext _context;

        public DetailsModel(SjcVotersPortal.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
