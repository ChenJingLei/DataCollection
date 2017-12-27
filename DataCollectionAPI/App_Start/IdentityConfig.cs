using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using DataCollectionAPI.Models;


using System.Data.Entity.Infrastructure;
using System.Data.Entity;
namespace DataCollectionAPI
{
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.

    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }

        private ApplicationDbContext db = new ApplicationDbContext();

        public async Task<ApplicationUser> FindAsyncByWeChatId(string wechatId)
        {
            var applicationUser = await (from u in db.ApplicationUsers
                                         where u.WeChatId == wechatId //&& u.PhoneNumber == ""
                                         select u).FirstOrDefaultAsync() as ApplicationUser;
            //db.ApplicationUsers.Where(u => u.WeChatId == wechatId //&& u.PhoneNumber == ""
            //                                                ).Select(u => u).FirstAsync(u=>u.WeChatId == wechatId);
            return applicationUser;
        }

    }
}
