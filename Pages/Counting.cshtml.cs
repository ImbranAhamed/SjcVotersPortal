using Microsoft.EntityFrameworkCore;
using SjcVotersPortal.Data;
using SjcVotersPortal.Data.Models;

namespace SjcVotersPortal.Pages;

public class Counting : SiteadminBasePageModel
{
    private readonly ApplicationDbContext _context;

    public Counting(ApplicationDbContext context)
    {
        _context = context;
    }

    public Dictionary<(int ElectionId, string AssociationName), Dictionary<(int DesignationId, string DesignantionName), List<Nomimation>>> Data;

    public void OnGet()
    {
        var now = DateTimeHelper.Now;
        var currentElections = _context.Elections.Include(e => e.Association).Where(e => now > e.NominationStart /*&& e.VotingEnd > now*/);
        Data = currentElections.ToDictionary(election => (election.Id, election.Association.Name),
            election => _context.AssociationDesignations.Where(associationDesignation => associationDesignation.AssociationId == election.AssociationId)
                .Select(associationDesignation => associationDesignation.Designation).ToDictionary(designation => (designation.Id, designation.Name),
                    _ => new List<Nomimation>()));

        foreach (var item in Data)
        {
            foreach (var item2 in item.Value)
            {
                var nominations = _context.Nominations.Include(e => e.Votes).Include(e => e.Student).Where(e => e.ElectionId == item.Key.ElectionId && e.DesignationId == item2.Key.DesignationId);
                item2.Value.AddRange(nominations);
            }
        }
    }
}