using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using Indelible.Models;

[assembly: OwinStartupAttribute(typeof(Indelible.Startup))]
namespace Indelible
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesandUsers();
        }

        // In this method we will create default User roles and Admin user for login
        private void CreateRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            // creating Creating Manager role
            if (!roleManager.RoleExists("Publisher"))
            {
                var role = new IdentityRole();
                role.Name = "Publisher";
                roleManager.Create(role);
            }
            
        }
    }
}
