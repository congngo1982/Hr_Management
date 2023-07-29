using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace BusinessObject.Models
{
    public partial class HospitalManagementDBContext : DbContext
    {
        public HospitalManagementDBContext()
        {
        }

        public HospitalManagementDBContext(DbContextOptions<HospitalManagementDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<DoctorInformation> DoctorInformations { get; set; }
        public virtual DbSet<StaffAccount> StaffAccounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                //optionsBuilder.UseSqlServer("Server=localhost;Uid=sa;Pwd=12345;Database= HospitalManagementDB ");
                Console.WriteLine("Connection: " + GetConnectionString());
                optionsBuilder.UseSqlServer(GetConnectionString());
            }
        }

        public string GetConnectionString()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfiguration configuration = builder.Build();
            return configuration.GetConnectionString("HospitalManagementDB");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("Department");

                entity.Property(e => e.DepartmentId).ValueGeneratedNever();

                entity.Property(e => e.DepartmentLocation).HasMaxLength(250);

                entity.Property(e => e.DepartmentName)
                    .IsRequired()
                    .HasMaxLength(120);

                entity.Property(e => e.ShortDescription).HasMaxLength(250);

                entity.Property(e => e.TelephoneNumber).HasMaxLength(20);
            });

            modelBuilder.Entity<DoctorInformation>(entity =>
            {
                entity.HasKey(e => e.DoctorId)
                    .HasName("PK__DoctorIn__2DC00EDF6701CBF4");

                entity.ToTable("DoctorInformation");

                entity.Property(e => e.DoctorId)
                    .HasMaxLength(20)
                    .HasColumnName("DoctorID");

                entity.Property(e => e.Birthday).HasColumnType("datetime");

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

                entity.Property(e => e.DoctorAddress).HasMaxLength(200);

                entity.Property(e => e.DoctorLicenseNumber).HasMaxLength(25);

                entity.Property(e => e.DoctorName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.DoctorInformations)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__DoctorInf__Depar__286302EC");
            });

            modelBuilder.Entity<StaffAccount>(entity =>
            {
                entity.HasKey(e => e.HraccountId)
                    .HasName("PK__StaffAcc__9A64147C821CF1B9");

                entity.ToTable("StaffAccount");

                entity.Property(e => e.HraccountId)
                    .HasMaxLength(50)
                    .HasColumnName("HRAccountId");

                entity.Property(e => e.Hremail)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("HREmail");

                entity.Property(e => e.Hrfullname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("HRFullname");

                entity.Property(e => e.Hrpassword)
                    .HasMaxLength(50)
                    .HasColumnName("HRPassword");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
