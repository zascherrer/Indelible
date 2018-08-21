using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using Indelible.Models;
using System.Collections.Generic;

[assembly: OwinStartupAttribute(typeof(Indelible.Startup))]
namespace Indelible
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesandUsers();
            //SeedContractReceipts();
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

        //private void SeedContractReceipts()
        //{
        //    Dictionary<string, Nethereum.RPC.Eth.DTOs.TransactionReceipt> Contracts = new Dictionary<string, Nethereum.RPC.Eth.DTOs.TransactionReceipt>();
        //    ContractReceiptDictionary ContractReceiptsDict = new ContractReceiptDictionary() { ContractReceipts = Contracts };
        //    ContractReceipt ContractReceipts = new ContractReceipt() { Contracts = ContractReceiptsDict };
        //    ApplicationDbContext db = new ApplicationDbContext();

        //    db.ContractReceipts.Add(ContractReceipts);
        //    db.SaveChanges();
        //}
}
}
