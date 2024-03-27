using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SjcVotersPortal.Data;
using SjcVotersPortal.Data.Models;

namespace SjcVotersPortal.Pages;

public class Vote : PageModel
{
    private readonly ApplicationDbContext _context;

    public Vote(ApplicationDbContext context)
    {
        _context = context;
    }

    public Dictionary<(int ElectionId, string AssociationName), Dictionary<(int DesignationId, string DesignantionName), List<Student>>> Data;
    public List<Data.Models.Vote> Votes;
    
    public void OnGet()
    {
        var now = DateTimeHelper.Now;
        var currentElections = _context.Elections.Include(e => e.Association).Where(e => now > e.VotingStart && now < e.VotingEnd);
        Data = currentElections.ToDictionary(election => (election.Id, election.Association.Name),
            election => _context.AssociationDesignations.Where(associationDesignation => associationDesignation.AssociationId == election.AssociationId)
                .Select(associationDesignation => associationDesignation.Designation).ToDictionary(designation => (designation.Id, designation.Name),
                    _ => new List<Student>()));
    
        foreach (var item in Data)
        {
            foreach (var item2 in item.Value)
            {
                var students = _context.Nominations.Where(e => e.ElectionId == item.Key.ElectionId && e.DesignationId == item2.Key.DesignationId)
                    .Select(e => e.Student);
                item2.Value.AddRange(students);
            }
        }
        
        var currentStudent = _context.Students.Single(e => e.EmailId.ToLower() == User.Identity!.Name!.ToLower());
        Votes = _context.Votes.Include(e => e.Nomimation).Where(e => e.RollNumber == currentStudent.RollNumber).ToList();
    }

    public async Task<IActionResult> OnPostAsync(int electionId, int designationId, string rollNumber)
    {
        var currentStudent = _context.Students.Single(e => e.EmailId.ToLower() == User.Identity!.Name!.ToLower());
        var votes = _context.Votes.Where(e => e.Nomimation.ElectionId == electionId && e.Nomimation.DesignationId == designationId && e.RollNumber == currentStudent.RollNumber);
        if (votes.Any() == false)
        {
            var nomination = await _context.Nominations.SingleAsync(e => e.ElectionId == electionId && e.DesignationId == designationId && e.RollNumber == rollNumber);
            _context.Votes.Add(new Data.Models.Vote() { Nomimation = nomination, Student = currentStudent, Timestamp = DateTimeHelper.Now });
            await _context.SaveChangesAsync();
            TempData[NamedConstants.TempKeys.Success] = "Vote casted successfully.";
        }
        else
        {
            TempData[NamedConstants.TempKeys.Failure] = "Vote already casted.";
        }
        return RedirectToPage();
    }
}