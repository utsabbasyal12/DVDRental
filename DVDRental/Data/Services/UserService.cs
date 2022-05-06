using DVDRental.Areas.Identity.Data;
using DVDRental.Models;

namespace DVDRental.Data.Services
{
    public class UserService : IUserService
    {
        private readonly AppDBContext appDbContext;

        public UserService(AppDBContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public ApplicationUser Get(string id)
        {
            return appDbContext.Users
                .FirstOrDefault(user => user.Id == id);
        }
        public async Task<ApplicationUser> Update(ApplicationUser applicationUser)
        {
            appDbContext.Update(applicationUser);
            await appDbContext.SaveChangesAsync();
            return applicationUser;
        }
    }
}
