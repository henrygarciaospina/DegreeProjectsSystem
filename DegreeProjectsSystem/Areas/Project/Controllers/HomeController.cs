using DegreeProjectsSystem.DataAccess.Repository.IRepository;
using DegreeProjectsSystem.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace DegreeProjectsSystem.Areas.Project.Controllers
{
    [Area("Project")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitWork _unitWork;

        public HomeController(ILogger<HomeController> logger, IUnitWork unitWork)
        {
            _logger = logger;
            _unitWork = unitWork;
        }

        
        public IActionResult Index()
        {
           var faculty = _unitWork.Faculty.GetFirst(f => f.Active == true);
            if (faculty != null)
            {
                ViewData["Faculty"] = faculty.Name.ToUpper();
            }
            else
            {
                ViewData["Faculty"] = "NO SE HA CREADO EL REGISTRO DE LA FACULTAD";
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
