using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ApplicationDBContext : DbContext,IApplicationDBContext
    {
        private const string Varchar255Type = "varchar(255)";
        private const string TextType = "text";
        private const string BoolType = "boolean";
        private const string TimeSpanType = "timestamp";
        private const string IntType = "int";
        public ApplicationDBContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SetDataType(modelBuilder);
            SetNotNull(modelBuilder);
            SetPrimaryKey(modelBuilder);
            SetRealtionship(modelBuilder);
        }
        public DbSet<Member> Members { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        #region Private method
        private void SetDataType(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Member>(a =>
            {
                a.Property(a => a.Name).HasColumnType(Varchar255Type);
                a.Property(a => a.Email).HasColumnType(Varchar255Type);
                a.Property(a => a.Password).HasColumnType(Varchar255Type);
                a.Property(a => a.MobileNumber).HasColumnType(Varchar255Type);
                a.Property(a => a.Gender).HasColumnType(IntType);
                a.Property(a => a.Dob).HasColumnType(TimeSpanType);
                a.Property(a => a.EmailOpt).HasColumnType(Varchar255Type);
            });

            modelBuilder.Entity<RefreshToken>(a =>
            {
                a.Property(a => a.Id).ValueGeneratedOnAdd();
                a.Property(a => a.Email).HasColumnType(Varchar255Type);
                a.Property(a => a.Token).HasColumnType(Varchar255Type);
                a.Property(a => a.JwtId).HasColumnType(TextType);
                a.Property(a => a.IsUsed).HasColumnType(BoolType);
                a.Property(a => a.IsRevoked).HasColumnType(BoolType);
                a.Property(a => a.ExpiryDate).HasColumnType(TimeSpanType);
                a.Property(a => a.AddedDate).HasColumnType(TimeSpanType);
                a.Property(a => a.DeathTime).HasColumnType(TimeSpanType);
            });
        }
        private void SetRealtionship(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RefreshToken>()
            .HasOne(rt => rt.Member)
            .WithMany(mem => mem.RefreshTokens)
            .HasForeignKey(k => k.Email)
            .OnDelete(DeleteBehavior.ClientCascade);
        }
        private void SetPrimaryKey(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Member>().HasKey(a => a.Email);
        }
        private void SetNotNull(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Member>(a =>
            {
                a.Property(a => a.Name).IsRequired();
                a.Property(a => a.Email).IsRequired();
                a.Property(a => a.Password).IsRequired();
                a.Property(a => a.MobileNumber).IsRequired();
                a.Property(a => a.Gender).IsRequired();
                a.Property(a => a.Dob).IsRequired();
            });
        }
        #endregion
    }
}

