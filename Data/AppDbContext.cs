using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SchoolBill.Models;
using System.Formats.Asn1;

namespace SchoolBill.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

      



            public DbSet<BillMaster> BillMasters { get; set; }
            public DbSet<BillDetail> BillDetails { get; set; }
            public DbSet<ClassEntity> Classes { get; set; }
            public DbSet<SectionEntity> Sections { get; set; }
            public DbSet<SchoolEntity> Schools { get; set; }
            public DbSet<StudentEntity> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuring BillDetail to BillMaster relationship without enforcing FK constraints
            modelBuilder.Entity<BillDetail>()
                .HasOne<BillMaster>()
                .WithMany(b => b.BillDetails)
                .HasPrincipalKey(bm => bm.Billid)
                .HasForeignKey(bd => bd.Billid)
                .IsRequired(false);

            // Configuring StudentEntity to BillMaster relationship without enforcing FK constraints
            modelBuilder.Entity<StudentEntity>()
                .HasOne<BillMaster>()
                .WithMany(bm => bm.Students)
                .HasPrincipalKey(bm => bm.Billid)
                .HasForeignKey(se => se.Billid)
                .IsRequired(false);

           

        }
    }
}
