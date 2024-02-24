using System.ComponentModel.DataAnnotations;

public class Student
{
    [Key]
    [Required]
    [MaxLength(10)]

    [Display(Name = "Roll Number")]
    public string RollNumber { get; set; }

    [Required]
    [MaxLength(50)]
    public string EmailId { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; }

    [Required]
    [MaxLength(7)]
    public string Batch { get; set; }

    [Required]
    [MaxLength(10)]
    public string CourseId { get; set; }

    [Required]
    public byte[] IdCardFile { get; set; }

    [Required]
    [MaxLength(100)]
    public string IdCarFileMime { get; set; }

    public bool? IsApproved {get; set;}
    
    [MaxLength(100)]
    public string RejectionReason { get; set; }
}