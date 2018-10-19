using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SimpleSurvey.Models
{
    public partial class SurveyContext : DbContext
    {
        public SurveyContext()
        {
        }

        public SurveyContext(DbContextOptions<SurveyContext> options)
            : base(options)
        {
        }

        public virtual DbSet<SurveyEntry> SurveyEntries { get; set; }
        public virtual DbSet<SurveyResponse> SurveyResponses { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {

        //        optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=Survey;Persist Security Info=True;User ID=scartag;Password=father");
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SurveyEntry>(entity =>
            {
                entity.ToTable("SurveyEntry");

                entity.Property(e => e.SurveyQuestion).IsRequired();
            });

            modelBuilder.Entity<SurveyResponse>(entity =>
            {
                entity.HasKey(e => e.EntryId);

                entity.ToTable("SurveyResponse");

                entity.Property(e => e.ResponseDate).HasColumnType("datetime");

                entity.Property(e => e.UserEmail).HasMaxLength(200);

                entity.HasOne(d => d.Survey)
                    .WithMany(p => p.SurveyResponses)
                    .HasForeignKey(d => d.SurveyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SurveyResponse_SurveyEntry");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
