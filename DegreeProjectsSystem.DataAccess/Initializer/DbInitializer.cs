using DegreeProjectsSystem.DataAccess.Data;
using DegreeProjectsSystem.Models;
using DegreeProjectsSystem.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DegreeProjectsSystem.DataAccess.Initializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(ApplicationDbContext db, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager) 
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }

      public void Initialize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception ex)
            {

                var mensaje = "Error message: " + ex.Message;
            }

            try
            {
                if (_db.Roles.Any(r => r.Name == DS.Role_Admin)) return;
            }
            catch (Exception ex)
            {

                var mensaje = "Error message: " + ex.Message;
            }


            try
            {
                _roleManager.CreateAsync(new IdentityRole(DS.Role_Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(DS.Role_Assistant)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(DS.Role_Consult)).GetAwaiter().GetResult();

            }
            catch (Exception ex)
            {

                var mensaje = "Error message: " + ex.Message;
            }

            try
            {
                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "administrador",
                    Email = "project.siglo@gmail.com",
                    EmailConfirmed = true,
                    Names = "Henry",
                    Surnames = "García Ospina",
                    Dependence = "Telemática"
                    /* Must comply with rules Configuration password */
                }, "Admin123*").GetAwaiter().GetResult();

                ApplicationUser user = _db.ApplicationUser.Where(u => u.UserName == "administrador").FirstOrDefault();

                _userManager.AddToRoleAsync(user, DS.Role_Admin).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {

                var mensaje = "Error message: " + ex.Message;
            }
            
        }
    }
}
