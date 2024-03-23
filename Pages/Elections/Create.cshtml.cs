using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SjcVotersPortal.Data;
using SjcVotersPortal.Data.Models;

namespace SjcVotersPortal.Pages_Elections
{
    public class CreateModel : PageModel
    {
        private readonly SjcVotersPortal.Data.ApplicationDbContext _context;

        public CreateModel(SjcVotersPortal.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public class InputModel
        {
            public int Id { get; set; }
            public int AssociationId { get; set; }
            public DateTime NominationStart { get; set; }
            public DateTime NominationEnd { get; set; }
            public DateTime VotingStart { get; set; }
            public DateTime VotingEnd { get; set; }
        }

        public IActionResult OnGet()
        {
            ViewData["AssociationId"] = new SelectList(_context.Associations, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public InputModel Election { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var now = DateTimeHelper.Now;
            var isAnotherElectionAvailable = _context.Elections.Any(e => e.AssociationId == Election.AssociationId && e.VotingEnd > DateTimeHelper.Now);
            if (isAnotherElectionAvailable)
            {
                ModelState.AddModelError(nameof(Election.AssociationId), $"Another election is in progress");
            }

            if (Election.NominationStart <= now)
            {
                ModelState.AddModelError(nameof(Election.NominationStart), $"Should be later than current time ({now})");
            }

            if (Election.NominationEnd <= Election.NominationStart)
            {
                ModelState.AddModelError(nameof(Election.NominationEnd), $"Should be later than {nameof(Election.NominationStart)} ({Election.NominationStart})");
            }

            if (Election.VotingStart <= Election.NominationEnd)
            {
                ModelState.AddModelError(nameof(Election.VotingStart), $"Should be later than {nameof(Election.NominationEnd)} ({Election.NominationEnd})");
            }

            if (Election.VotingEnd <= Election.VotingStart)
            {
                ModelState.AddModelError(nameof(Election.VotingEnd), $"Should be later than {nameof(Election.VotingStart)} ({Election.VotingStart})");
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _context.Elections.Add(new Election()
            {
                AssociationId = Election.AssociationId,
                NominationStart = Election.NominationStart,
                NominationEnd = Election.NominationEnd,
                VotingStart = Election.VotingStart,
                VotingEnd = Election.VotingEnd,
                Timestamp = DateTimeHelper.Now
            });
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
