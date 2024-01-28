public class AssociationDesignation
{
    public int Id { get; set; }

    public int AssociationId { set; get; }

    public int DesignationID { get; set; }

    public Association Association { get; set; }
    public Designation Designation { get; set; }
}