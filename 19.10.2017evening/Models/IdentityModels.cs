using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _19._10._2017evening.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {

        [Display(Name = "Study Date")]
        [DataType(DataType.Date)]
        public DateTime? StudyDate { get; set; }

        public string UserLastName { get; set; }

        public int Age { get; set; }
        [Display(Name = "Study date")]
        public string StudyDateString { get; set; }
        [Display(Name = "Registered Date")]
        public string RegisteredDateString { get; set; }
        
        public DateTime? RegisteredDate { get; set; }



        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base () { }
        public ApplicationRole(string roleName): base(roleName) { }
    } 

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

     //   public System.Data.Entity.DbSet<_19._10._2017evening.Models.ApplicationUser> ApplicationUsers { get; set; }
    }
}