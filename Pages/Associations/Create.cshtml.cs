using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SjcVotersPortal.Data;

namespace SjcVotersPortal.Pages.Associations
{
    public class CreateModel : SiteadminBasePageModel
    {
        private readonly SjcVotersPortal.Data.ApplicationDbContext _context;

        public CreateModel(SjcVotersPortal.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        public List<SelectListItem> Designations { get; set; }

        public IActionResult OnGet()
        {
            Designations = _context.Designations.Select(e => new SelectListItem(e.Name, e.Id.ToString())).ToList();
            return Page();
        }


        [BindProperty]
        public Association Association { get; set; } = default!;

        [BindProperty]
        public int[] DesignationsMapped { get ;set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Debug.WriteLine(DesignationsMapped.Humanize());
            _context.Associations.Add(Association);
            foreach(var d in DesignationsMapped)
            {
                _context.AssociationDesignations.Add(new AssociationDesignation { Association = Association, DesignationID = d });
            }                
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
