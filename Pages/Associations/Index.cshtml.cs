using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SjcVotersPortal.Data;
using SjcVotersPortal.Data.Models;

namespace SjcVotersPortal.Pages.Associations
{
    public class IndexModel : SiteadminBasePageModel
    {
        private readonly SjcVotersPortal.Data.ApplicationDbContext _context;

        public IndexModel(SjcVotersPortal.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Association> Associations { get;set; } = default!;
        public Dictionary<int, string[]> MappedDesignations;
        public async Task OnGetAsync()
        {
            Associations = await _context.Associations.ToListAsync();

            MappedDesignations = new Dictionary<int, string[]>(Associations.Count);
            foreach(var association in Associations)
            {
                var designations = _context.AssociationDesignations.Where(e => e.AssociationId == association.Id).Select(e=> e.Designation.Name).ToArray();
                MappedDesignations.Add(association.Id, designations);
            }            
        }
    }
}
