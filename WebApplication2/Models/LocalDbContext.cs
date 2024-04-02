using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace AEMAssessment.Models
{
    public partial class LocalDbContext : DbContext
    {
        public LocalDbContext()
        {

        }

        public LocalDbContext(DbContextOptions<LocalDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Platform> Platform { get; set; }
        public virtual DbSet<Well> Well { get; set; }
    }
}