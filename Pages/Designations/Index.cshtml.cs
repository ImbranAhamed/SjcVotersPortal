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
    public class IndexModel : PageModel
    {
        private readonly SjcVotersPortal.Data.ApplicationDbContext _context;

        public IndexModel(SjcVotersPortal.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Designation> Designation { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Designation = await _context.Designations.ToListAsync();
        }
    }
}
