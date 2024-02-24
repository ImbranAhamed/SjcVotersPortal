using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SjcVotersPortal.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    public DbSet<Association> Associations { get; set; }
    public DbSet<Designation> Designations { get; set; }
    public DbSet<AssociationDesignation> AssociationDesignations { get; set; }
    public DbSet<Student> Students { get; set; }
}
