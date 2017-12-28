using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace DataCollectionAPI.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }

        public Guid AspNetWeChatAccountId { get; set; }

        public virtual AspNetWeChatAccount WeChatAccount { get; set; }

        public int AspNetDepartmentId { get; set; }

        public virtual AspNetDepartment Department { get; set; }
    }

    [Table("AspNetWeChatAccounts")]
    public class AspNetWeChatAccount
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Display(Name = "微信标识")]
        public string OpenId { get; set; }
        [StringLength(256)]
        public string NickName { get; set; }
        public string AvatarUrl { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public string UnionId { get; set; }
        public string Lauguage { get; set; }
    }

    [Table("AspNetDepartments")]
    public class AspNetDepartment
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Display(Name = "部门名称")]
        [StringLength(256)]
        public string Name { get; set; }
        [StringLength(256)]
        public string C1 { get; set; }
        [StringLength(256)]
        public string C2 { get; set; }
        [StringLength(256)]
        public string C3 { get; set; }
        [StringLength(256)]
        public string C4 { get; set; }
        [StringLength(256)]
        public string C5 { get; set; }
        [StringLength(256)]
        public string C6 { get; set; }
        [StringLength(256)]
        public string C7 { get; set; }
        [StringLength(256)]
        public string C8 { get; set; }

    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            this.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<AspNetWeChatAccount> WeChatAccounts { get; set; }

        public System.Data.Entity.DbSet<AspNetDepartment> Department { get; set; }
    }
}