using AspNetCoreHero.ToastNotification.Abstractions;
using DegreeProjectsSystem.DataAccess.Repository.IRepository;
using DegreeProjectsSystem.Models;
using DegreeProjectsSystem.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DegreeProjectsSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CareerController : Controller
    {
        private readonly IUnitWork _unitWork;
        private readonly INotyfService _notyfService;
        public CareerController(IUnitWork unitWork, INotyfService notyfService)
        {
            _unitWork = unitWork;
            _notyfService = notyfService;
        }

        enum Action
        {
            Create,
            Update,
            None
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult InsertOrUpdateCareer(int? id)
        {
            CareerViewModel careerViewModel = new CareerViewModel()
            {
                Career = new Career(),
                ProgramTypeList = _unitWork.ProgramType.GetAll().Select(pt => new SelectListItem
                {
                    Text = pt.Name,
                    Value = pt.Id.ToString()
                })
            };

            if (id == null)
            {
                careerViewModel.Career.Active = true;
                // Crea un nuevo registro
                return View(careerViewModel);
            }

            // Actualiza el registro
            careerViewModel.Career = _unitWork.Career.Get(id.GetValueOrDefault());
            if (careerViewModel == null)
            {
                return NotFound();
            }
            return View(careerViewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult InsertOrUpdateCareer(CareerViewModel careerViewModel)
        {
            Action action = Action.None;
            if (ModelState.IsValid)
            {
                if (careerViewModel.Career.Id == 0)
                {
                    action = Action.Create;
                    _unitWork.Career.Add(careerViewModel.Career);
                }
                else
                {
                    action = Action.Update;
                    _unitWork.Career.Update(careerViewModel.Career);
                }

                try
                {
                    _unitWork.Save();

                    if (action == Action.Create)
                    {
                        _notyfService.Success("Programa creado correctamente.");
                    }
                    if (action == Action.Update)
                    {
                        _notyfService.Success("Programa actualizado correctamente.");
                    }

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("IX_Careers_Name"))
                    {
                        var msg = "El Programa  ya se encuentra registrado";
                        LoadLists(careerViewModel, msg);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            else
            {
                var msg = "";
                LoadLists(careerViewModel, msg);
                
                if (careerViewModel.Career.Id != 0)
                {
                    careerViewModel.Career = _unitWork.Career.Get(careerViewModel.Career.Id);
                }
            }
            return View(careerViewModel);
        }

        public ViewResult LoadLists(CareerViewModel careerViewModel, string message)
        {
            careerViewModel.ProgramTypeList = _unitWork.ProgramType.GetAll().Select(pt => new SelectListItem
            {
                Text = pt.Name,
                Value = pt.Id.ToString()
            });

            if (!string.IsNullOrEmpty(message))
            {
                _notyfService.Error(message);
            }

            return View(careerViewModel);

        }


        #region API
        [HttpGet]
        public IActionResult GetAllCareers()
        {
            var careers = _unitWork.Career.GetAll(includeProperties: "ProgramType");
            return Json(new { data = careers });
        }

        //Eliminación lógica de registro 
        [HttpPost]
        public IActionResult DeleteCareer(int id)
        {
            // Actualiza el registro
            var careerDb = _unitWork.Career.Get(id);

            if (careerDb == null)
            {
                return Json(new { succes = false, message = "!!Error al borrar el programa!!." });
            }

            careerDb.Active = false;
            _unitWork.Career.Update(careerDb);
            _unitWork.Save();

            return Json(new { succes = true, message = "Programa borrado exitosamente." });
        }


        #endregion
    }
}
