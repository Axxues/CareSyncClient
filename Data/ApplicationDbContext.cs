using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CareSync.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
    {
        //Patient
        public virtual DbSet<PatientPersonalInformation> PatientPersonalInformations { get; set; }
        public virtual DbSet<PatientHealthInformation> PatientHealthInformations { get; set; }
        public virtual DbSet<PatientEmergencyContact> PatientEmergencyContacts { get; set; }

        //Consultations
        public virtual DbSet<ConsultationDetail> ConsultationDetails { get; set; }
        public virtual DbSet<ConsultationPrescription> ConsultationPrescriptions { get; set; }

        //Inventory
        public virtual DbSet<InventoryItemDetail> InventoryItemDetails { get; set; }
        public virtual DbSet<InventoryStockDetail> InventoryStockDetails { get; set; }
        public virtual DbSet<InventoryDispenseDetail> InventoryDispenseDetails { get; set; }

        //--------------------------- Database Configuration
        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);

        //    //Prevent "Multiple Cascade Paths" error when updating database
        //    builder.Entity<InventoryDispenseDetail>()
        //        .HasOne<PatientPersonalInformation>(d => d.PatientPersonalInformation) // Adjust this name if your navigation property is named differently
        //        .WithMany()
        //        .HasForeignKey(d => d.PatientPersonalInformationId)
        //        .OnDelete(DeleteBehavior.Restrict);
        //}

        //---------------------------For Updated At
        public override int SaveChanges()
        {
            SetUpdatedTimestamps();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetUpdatedTimestamps();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void SetUpdatedTimestamps()
        {
            var modifiedEntries = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Modified);

            foreach (var entry in modifiedEntries)
            {
                var property = entry.Properties.FirstOrDefault(p => p.Metadata.Name == "UpdatedAt");
                if (property != null)
                {
                    property.CurrentValue = DateTime.Now; // or DateTime.UtcNow for consistency
                }
            }
        }
        //---------------------------For Updated At End
    }
}