using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ProiectIP.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            //userIdentity.AddClaim(new Claim("Age", this.Age.ToString()));
            //userIdentity.AddClaim(new Claim("Sex", this.Sex.ToString()));
            //userIdentity.AddClaim(new Claim("Occupation", this.Occupation.ToString()));
            //userIdentity.AddClaim(new Claim("Zipcode", this.Zipcode.ToString()));
            return userIdentity;
        }
        public int Age { get; set; }
        public string Sex { get; set; }
        public string Occupation { get; set; }
        public int Zipcode { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Place> Places { get; set; }
        public DbSet<RatingModel> RatingModels { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}