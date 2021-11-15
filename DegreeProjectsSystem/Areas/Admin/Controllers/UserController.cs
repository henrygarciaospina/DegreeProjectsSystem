using DegreeProjectsSystem.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace DegreeProjectsSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _db;

        public UserController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }


        #region API

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var userList = _db.ApplicationUser.ToList();
            var userRole = _db.UserRoles.ToList();
            var roles = _db.Roles.ToList();

            foreach (var user in userList)
            {
                var roleId = userRole.FirstOrDefault(u => u.UserId == user.Id).RoleId;
                user.Role = roles.FirstOrDefault(u => u.Id == roleId).Name;
            }

            return Json(new { data = userList });
        }


        [HttpPost]
        public IActionResult LockedUnlocked([FromBody] string id)
        {
            string message = " ";
            var user = _db.ApplicationUser.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                message = "Error de Usuario";
                return Json(new { success = false, message  });
            }

            if (user.LockoutEnd != null && user.LockoutEnd > DateTime.Now)
            {
                message = "Usuario desbloqueado";
                // User lockout, assign date today and change state by unlocked
                user.LockoutEnd = DateTime.Now;
            }
            else
            {
                message = "Usuario bloqueado";
                // User unlocked, add 1000 years to today and change state by locked
                user.LockoutEnd = DateTime.Now.AddYears(1000);
            }
            _db.SaveChanges();
            return Json(new { success = true, message });

        }

        #endregion
    }
}