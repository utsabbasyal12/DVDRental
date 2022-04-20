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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
