using System.ComponentModel.DataAnnotations;

public class Student
{
    [Key]
    public string RollNumber { get; set; }
    public string EmailId { get; set; }
    public string Name { get; set; }
    public string Batch { get; set; }
    public string CourseId { get; set; }
    public byte[] IdCardFile { get; set; }
    public string IdCarFileMime { get; set; }
}