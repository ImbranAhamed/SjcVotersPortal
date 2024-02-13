using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SjcVotersPortal.Data;

namespace SjcVotersPortal.Pages.Associations
{
    public class EditModel : SiteadminBasePageModel
    {
        private readonly SjcVotersPortal.Data.ApplicationDbContext _context;

        public EditModel(SjcVotersPortal.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public List<SelectListItem> Designations { get; set; }

        [BindProperty]
        public Association Association { get; set; } = default!;

        [BindProperty]
        public int[] DesignationsMapped { get ;set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var association =  await _context.Associations.FirstOrDefaultAsync(m => m.Id == id);
            if (association == null)
            {
                return NotFound();
            }
            var dm = _context.AssociationDesignations.Where(e => e.AssociationId == id).Select(e => e.DesignationID)?.ToArray() ?? [];
            Designations = _context.Designations.Select(e => new SelectListItem(e.Name, e.Id.ToString(), dm.Contains(e.Id))).ToList();

            Association = association;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Association).State = EntityState.Modified;

            //remove existing mappings
            var dm = _context.AssociationDesignations.Where(e => e.AssociationId == Association.Id);
            _context.AssociationDesignations.RemoveRange(dm);

            var ndms = new List<AssociationDesignation>();
            //add  new mappings
            foreach(var d in DesignationsMapped)
            {
                ndms.Add(new AssociationDesignation { Association = Association, DesignationID = d });
            }
            _context.AssociationDesignations.AddRange(ndms);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssociationExists(Association.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool AssociationExists(int id)
        {
            return _context.Associations.Any(e => e.Id == id);
        }
    }
}
