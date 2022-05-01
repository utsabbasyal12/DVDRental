using DVDRental.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DVDRental.Areas.Identity.Data;

public class AppDBContext : IdentityDbContext<DVDRentalUser>
{
    public AppDBContext(DbContextOptions<AppDBContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);

        builder.ApplyConfiguration(new ApplicationUserEntityCongiguration());
    }

    
}

public class ApplicationUserEntityCongiguration : IEntityTypeConfiguration<DVDRentalUser>
{
    public void Configure(EntityTypeBuilder<DVDRentalUser> builder)
    {
        //builder.Property(u => u.ShopNumber).HasMaxLength(10);
        builder.Property(u => u.ShopName).HasMaxLength(255);
        //throw new NotImplementedException();
    }

}