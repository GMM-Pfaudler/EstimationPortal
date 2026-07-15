using SUP_CORE.EFModel;
using SUP_CORE.EFModelViews;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace SUP_DAL.EFContextProvider
{
    public class SupplierDbContext : DbContext
    {
        public SupplierDbContext() : base("DefaultConnection")
        {

        }
        public DbSet<Tbl_UserMasters> Tbl_UserMasters { get; set; }
        public DbSet<Tbl_LoginLogs> Tbl_LoginLogs { get; set; }
        public DbSet<Tbl_Roles> Tbl_Roles { get; set; }
        public DbSet<Tbl_SalesPersonMasters> Tbl_SalesPersonMasters { get; set; }
        public DbSet<Tbl_InqTypeMasters> Tbl_InqTypeMasters { get; set; }
        public DbSet<Tbl_SubTypeMasters> Tbl_SubTypeMasters { get; set; }
        public DbSet<Tbl_EquipTypeMasters> Tbl_EquipTypeMasters { get; set; }
        public DbSet<Tbl_EngineerMasters> Tbl_EngineerMasters { get; set; }
        public DbSet<Tbl_InquiryMasters> Tbl_InquiryMasters { get; set; }
        public DbSet<Tbl_EnqStatusMasters> Tbl_EnqStatusMasters { get; set; }
        public DbSet<Tbl_TqRegisters> Tbl_TqRegisters { get; set; }
        public DbSet<Tbl_EmailMasters> Tbl_EmailMasters { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            Database.SetInitializer<SupplierDbContext>(null);
        }
    }
}
