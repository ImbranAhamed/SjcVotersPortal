using System.ComponentModel.DataAnnotations.Schema;

public class StudentAssociation
{
    public int Id { get; set; }
    
    public string RollNumber { get; set; }
    public int AssociationId { get; set; }
    
    public Association Association { get; set; }
    
    [ForeignKey(nameof(RollNumber))]
    public Student Student { get; set; }
}