using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SjcVotersPortal.Data;
using SjcVotersPortal.Data.Models;

namespace SjcVotersPortal.Pages.Students;

public class Nomination : StudentBasePageModel
{
    private readonly ApplicationDbContext _context;

    public Nomination(ApplicationDbContext context)
    {
        _context = context;
    }

    public Dictionary<(int ElectionId, string AssociationName, bool IsNominationOpen), List<Designation>> ElectionDesignationsDictionary { get; set; }
    public new Dictionary<int, int> CurrentNominationDict;

    public void OnGet()
    {
        var now = DateTimeHelper.Now;
        var currentElections = _context.Elections.Include(e => e.Association);
        ElectionDesignationsDictionary = currentElections.ToDictionary(e => (e.Id, e.Association.Name, now > e.NominationStart && now < e.NominationEnd),
            e => _context.AssociationDesignations.Where(associationDesignation => associationDesignation.AssociationId == e.AssociationId)
                .Select(associationDesignation => associationDesignation.Designation).ToList());

        CurrentNominationDict = new Dictionary<int, int>();
        foreach (var item in ElectionDesignationsDictionary)
        {
            var rollNumber = _context.Students.Single(e => e.EmailId.ToLower() == User.Identity!.Name!.ToLower()).RollNumber;

            var currentNomination = _context.Nominations.FirstOrDefault(e => e.ElectionId == item.Key.ElectionId && e.RollNumber == rollNumber);
            CurrentNominationDict.Add(item.Key.ElectionId, currentNomination?.DesignationId ?? 0);
        }
    }

    public async Task<IActionResult> OnPostAsync(int electionId, int designationId)
    {
        var now = DateTimeHelper.Now;
        var isNominationOpen = _context.Elections.Any(e => e.Id == electionId && now > e.NominationStart && now < e.NominationEnd);
        if (isNominationOpen == false)
        {
            TempData[NamedConstants.TempKeys.Failure] = "Nomination not open";
            return RedirectToPage();
        }

        var rollNumber = _context.Students.Single(e => e.EmailId.ToLower() == User.Identity!.Name!.ToLower()).RollNumber;

        var currentNominations = _context.Nominations.Where(e => e.ElectionId == electionId && e.RollNumber == rollNumber);
        if (currentNominations.Any())
        {
            _context.Nominations.RemoveRange(currentNominations);
        }

        if (designationId != 0)
        {
            _context.Nominations.Add(new Nomimation() { ElectionId = electionId, DesignationId = designationId, RollNumber = rollNumber, Timestamp = now });
            TempData[NamedConstants.TempKeys.Success] = "Nomination done successfully";
        }
        else
        {
            TempData[NamedConstants.TempKeys.Failure] = "Nomination withdrawn successfully";
        }
        await _context.SaveChangesAsync();
        return RedirectToPage();
    }
}