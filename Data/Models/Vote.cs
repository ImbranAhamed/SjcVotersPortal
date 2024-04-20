using System.ComponentModel.DataAnnotations.Schema;

namespace SjcVotersPortal.Data.Models;

public class Vote
{
    public int Id { get; set; }

    public int NomimationId { get; set; }
    public Nomimation Nomimation { get; set; }
    public string RollNumber { get; set; }

    [ForeignKey(nameof(RollNumber))]
    public Student Student { get; set; }

    public DateTime Timestamp { get; set; }
}