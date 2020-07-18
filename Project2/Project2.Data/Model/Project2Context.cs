using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Project2.Data.Model
{
    public partial class Project2Context : DbContext
    {
        public Project2Context()
        {
        }

        public Project2Context(DbContextOptions<Project2Context> options)
            : base(options)
        {
        }

        public virtual DbSet<DoctorEntity> DoctorEntity { get; set; }
        public virtual DbSet<IllnessEntity> IllnessEntity { get; set; }
        public virtual DbSet<NurseEntity> NurseEntity { get; set; }
        public virtual DbSet<OpsRoomEntity> OpsRoomEntity { get; set; }
        public virtual DbSet<PatientEntity> PatientEntity { get; set; }
        public virtual DbSet<PatientRoomEntity> PatientRoomEntity { get; set; }
        public virtual DbSet<TreatmentDetailsEntity> TreatmentDetailsEntity { get; set; }
        public virtual DbSet<TreatmentEntity> TreatmentEntity { get; set; }
        public virtual DbSet<WorkingDetailsEntity> WorkingDetailsEntity { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DoctorEntity>(entity =>
            {
                entity.HasKey(e => e.DoctorId)
                    .HasName("PK_Doctor");

                entity.Property(e => e.DoctorId).HasColumnName("DoctorID");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<IllnessEntity>(entity =>
            {
                entity.HasKey(e => e.IllnessId);

                entity.Property(e => e.IllnessId).HasColumnName("IllnessID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<NurseEntity>(entity =>
            {
                entity.HasKey(e => e.NurseId)
                    .HasName("PK_Nurse");

                entity.Property(e => e.NurseId).HasColumnName("NurseID");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<OpsRoomEntity>(entity =>
            {
                entity.HasKey(e => e.OpsRoomId)
                    .HasName("PK_OpsRoom");

                entity.Property(e => e.OpsRoomId).HasColumnName("OpsRoomID");
            });

            modelBuilder.Entity<PatientEntity>(entity =>
            {
                entity.HasKey(e => e.PatientId);

                entity.Property(e => e.PatientId).HasColumnName("PatientID");

                entity.Property(e => e.DoctorId).HasColumnName("DoctorID");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.IllnessId).HasColumnName("IllnessID");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.PatientRoomId).HasColumnName("PatientRoomID");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.PatientEntity)
                    .HasForeignKey(d => d.DoctorId);

                entity.HasOne(d => d.Illness)
                    .WithMany(p => p.PatientEntity)
                    .HasForeignKey(d => d.IllnessId);

                entity.HasOne(d => d.PatientRoom)
                    .WithMany(p => p.PatientEntity)
                    .HasForeignKey(d => d.PatientRoomId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<PatientRoomEntity>(entity =>
            {
                entity.HasKey(e => e.PatientRoomId)
                    .HasName("PK_PatientRoom");

                entity.Property(e => e.PatientRoomId).HasColumnName("PatientRoomID");
            });

            modelBuilder.Entity<TreatmentDetailsEntity>(entity =>
            {
                entity.HasKey(e => new { e.OpsRoomId, e.PatientId });

                entity.Property(e => e.OpsRoomId).HasColumnName("OpsRoomID");

                entity.Property(e => e.PatientId).HasColumnName("PatientID");

                entity.Property(e => e.StartTime)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.OpsRoom)
                    .WithMany(p => p.TreatmentDetailsEntity)
                    .HasForeignKey(d => d.OpsRoomId);

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.TreatmentDetailsEntity)
                    .HasForeignKey(d => d.PatientId);
            });

            modelBuilder.Entity<TreatmentEntity>(entity =>
            {
                entity.HasKey(e => new { e.IllnessId, e.DoctorId });

                entity.Property(e => e.IllnessId).HasColumnName("IllnessID");

                entity.Property(e => e.DoctorId).HasColumnName("DoctorID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.TreatmentEntity)
                    .HasForeignKey(d => d.DoctorId);

                entity.HasOne(d => d.Illness)
                    .WithMany(p => p.TreatmentEntity)
                    .HasForeignKey(d => d.IllnessId);
            });

            modelBuilder.Entity<WorkingDetailsEntity>(entity =>
            {
                entity.HasKey(e => new { e.NurseId, e.DoctorId })
                    .HasName("PK_WorkingDetails");

                entity.Property(e => e.NurseId).HasColumnName("NurseID");

                entity.Property(e => e.DoctorId).HasColumnName("DoctorID");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.WorkingDetailsEntity)
                    .HasForeignKey(d => d.DoctorId)
                    .HasConstraintName("FK_WorkingDetails_DoctorEntity_DoctorID");

                entity.HasOne(d => d.Nurse)
                    .WithMany(p => p.WorkingDetailsEntity)
                    .HasForeignKey(d => d.NurseId)
                    .HasConstraintName("FK_WorkingDetails_NurseEntity_NurseID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
