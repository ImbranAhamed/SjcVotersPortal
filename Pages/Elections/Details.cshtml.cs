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
    public class DetailsModel : PageModel
    {
        private readonly SjcVotersPortal.Data.ApplicationDbContext _context;

        public DetailsModel(SjcVotersPortal.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Election Election { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var election = await _context.Elections.FirstOrDefaultAsync(m => m.Id == id);
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
    }
}
