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
    public class IndexModel : PageModel
    {
        private readonly SjcVotersPortal.Data.ApplicationDbContext _context;

        public IndexModel(SjcVotersPortal.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Election> Election { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Election = await _context.Elections
                .Include(e => e.Association).ToListAsync();
        }
    }
}
