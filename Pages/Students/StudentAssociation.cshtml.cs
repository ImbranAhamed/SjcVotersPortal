using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SjcVotersPortal.Data;

namespace SjcVotersPortal.Pages.Students;

public class StudentAssociation : StudentBasePageModel
{
    private readonly ApplicationDbContext _context;

    public StudentAssociation(ApplicationDbContext context)
    {
        _context = context;
    }
    public record DisplayModel
    {
        public int AssociationId { get; set; }
        public string AssociationName { get; set; }
        public bool IsMember { get; set; }
    }
    public IList<DisplayModel> AssociationMemberships { get; set; }
    
    public void OnGet()
    {
        var allAssociations  = _context.Associations.Select(e => new DisplayModel
        {
            AssociationId = e.Id,
            AssociationName = e.Name,
            IsMember = false
        }).ToDictionary(e => e.AssociationId, e => e);
        
        var rollNumber = _context.Students.Single(e => e.EmailId.ToLower() == User.Identity!.Name!.ToLower()).RollNumber;
        var memberOfAssociations = _context.StudentAssociations.Where(e => e.RollNumber == rollNumber).Select(e => e.AssociationId);
        foreach (var a in memberOfAssociations)
        {
            allAssociations[a].IsMember = true;
        }

        AssociationMemberships = allAssociations.Values.ToList();
    }

    public async Task<IActionResult> OnPostAsync(int associationId)
    {
        var rollNumber = _context.Students.Single(e => e.EmailId.ToLower() == User.Identity!.Name!.ToLower()).RollNumber;
        var studentAssociation = _context.StudentAssociations.Where(e => e.RollNumber == rollNumber && e.AssociationId == associationId);
        var isElectionInProgress = _context.Elections.Any(e => e.AssociationId == associationId && e.VotingEnd > DateTimeHelper.Now);
        if (isElectionInProgress)
        {
            return RedirectToPage("./ElectionIsInProgress");
        }
        
        if (studentAssociation.Any())
        {
            _context.StudentAssociations.RemoveRange(studentAssociation);
        }
        else
        {
            _context.StudentAssociations.Add(new global::SjcVotersPortal.Data.Models.StudentAssociation() { RollNumber = rollNumber, AssociationId = associationId, TimeStamp = DateTimeHelper.Now});
        }

        await _context.SaveChangesAsync();
        return RedirectToPage();
    }
}