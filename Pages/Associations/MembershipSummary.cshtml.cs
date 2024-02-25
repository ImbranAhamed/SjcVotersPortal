using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SjcVotersPortal.Data;

namespace SjcVotersPortal.Pages.Associations
{
    public class MembershipSummaryModel : SiteadminBasePageModel
    {
        private readonly ApplicationDbContext _context;

        public MembershipSummaryModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public class DisplayModel
        {
            public int AssociationId { get; set; }
            public string AssociationName { get; set; }
            public int Count { get; set; }
        }

        public IList<DisplayModel> Summary;
        public void OnGet()
        {
            var allAssociations = _context.Associations.Select(e => new DisplayModel
            {
                AssociationId = e.Id,
                AssociationName = e.Name,
                Count = 0
            }).ToDictionary(e => e.AssociationId, e => e);

            var groupBy = _context.StudentAssociations.GroupBy(e => e.AssociationId).Select(e => new { e.Key, Count =  e.Count() } );

            foreach (var item in groupBy)
            {
                allAssociations[item.Key].Count = item.Count;
            }

            Summary = allAssociations.Values.ToList();
        }
    }
}
