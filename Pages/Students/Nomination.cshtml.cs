using System.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SjcVotersPortal.Data;
using SjcVotersPortal.Data.Models;

namespace SjcVotersPortal.Pages.Students;

public class Nomination : PageModel
{
    private readonly ApplicationDbContext _context;

    public Nomination(ApplicationDbContext context)
    {
        _context = context;
    }
    public Dictionary<(int ElectionId, string AssociationName), List<Designation>> ElectionDesignationsDictionary { get; set; }
    public void OnGet()
    {
        var now = DateTime.Now;
        var currentElections = _context.Elections.Include(e => e.Association).Where(e => now > e.NominationStart && now < e.NominationEnd);
        ElectionDesignationsDictionary = currentElections.ToDictionary(e => (e.Id, e.Association.Name),
            e => _context.AssociationDesignations.Where(associationDesignation => associationDesignation.AssociationId == e.AssociationId)
                .Select(associationDesignation => associationDesignation.Designation).ToList());
    }
}