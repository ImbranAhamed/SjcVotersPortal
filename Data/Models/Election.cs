namespace SjcVotersPortal.Data.Models;

public class Election
{
    public int Id { get; set; }
    public int AssociationId { get; set; }
    public DateTime NominationStart { get; set; }
    public DateTime NominationEnd { get; set; }
    public DateTime VotingStart { get; set; }
    public DateTime VotingEnd { get; set; }
    
    public Association Association { get; set; }
}