using DVDRental.Areas.Identity.Data;
using DVDRental.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DVDRental.Areas.Identity.Data;

public class AppDBContext : IdentityDbContext<ApplicationUser>
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public AppDBContext(DbContextOptions<AppDBContext> options)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        : base(options)
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

        builder.ApplyConfiguration(new ApplicationUserEntityCongiguration());
    }

    
}

public class ApplicationUserEntityCongiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        //builder.Property(u => u.ShopNumber).HasMaxLength(10);
        builder.Property(u => u.ShopName).HasMaxLength(255);
        //throw new NotImplementedException();
    }

}