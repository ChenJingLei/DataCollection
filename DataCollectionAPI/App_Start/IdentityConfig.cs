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

        public async Task<ApplicationUser> FindAsyncByPhoneNumberAndWeChatId(string phoneNumber,string wechatId)
        {
            //phoneNumber&&wechatId->ApplicationUser
            var applicationUser = await (from u in db.Users
                                         join wx in db.WeChatAccounts on u.AspNetWeChatAccountId equals wx.Id
                                         where u.PhoneNumber == phoneNumber && wx.OpenId == wechatId //wx.UnionId == wechatId
                                         select u).FirstOrDefaultAsync();
            if(applicationUser != null)
            {
                return await Task.FromResult<ApplicationUser>(applicationUser);
            }
            return await Task.FromResult<ApplicationUser>(null);
        }

    }

    public struct IdentityRoleOption
    {
        public static readonly string SysAdmin      = "c04e2839-d9f7-42bc-a7e2-9c8bcee1040b";
        public static readonly string Admin         = "1614e96a-ce16-45a8-99db-ffbcb1d53718";
        public static readonly string Manager       = "b3000fd7-2ac5-409a-b582-d3c2d8457459";
        public static readonly string Aapplicant    = "d803555e-7801-45ae-b219-ec1cd40dd170";
    }
}
