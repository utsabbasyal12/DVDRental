using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DVDRental.Models;

namespace DVDRental.DBContext
{
    public class ApplicationDBContext: IdentityDbContext<IdentityUser>
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
        }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Studio> Studios { get; set; }
        public DbSet<CastMember> CastMembers { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<DVDCategory> DVDCategory { get; set; }
        public DbSet<DVDTitle> DVDTitles { get; set; }
        public DbSet<DVDCopy> DVDCopies { get; set; }
        public DbSet<Loan> Loans { get; set; }  
        public DbSet<LoanType> LoanTypes { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<MembershipCategory> MembershipCategories { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<CastMember>().HasKey(am => new
            {
                am.ActorId,
                am.DVDNumber
            });
            builder.Entity<CastMember>()
                .HasOne(a => a.Actor)
                .WithMany(ab => ab.CastMembers)
                .HasForeignKey(a => a.ActorId);

            builder.Entity<CastMember>()
                .HasOne(a => a.DVDTitle)
                .WithMany(ab => ab.CastMembers)
                .HasForeignKey(a => a.DVDNumber);

            

        }
    }
}
