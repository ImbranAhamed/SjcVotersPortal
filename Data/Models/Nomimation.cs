using System.ComponentModel.DataAnnotations.Schema;

namespace SjcVotersPortal.Data.Models;

public class Nomimation
{
    public int Id { get; set; }
    
    public int ElectionId { get; set; }
    
    public Election Election { get; set; }
    
    public int DesignationId { get; set; }
    
    public Designation Designation { get; set; }
    public string RollNumber { get; set; }
    
    [ForeignKey(nameof(RollNumber))]
    public Student Student { get; set; }
    
    public DateTime Timestamp { get; set; } 
    
    public List<Vote> Votes { get; set; }
}