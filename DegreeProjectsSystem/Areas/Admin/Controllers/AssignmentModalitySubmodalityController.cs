using AspNetCoreHero.ToastNotification.Abstractions;
using DegreeProjectsSystem.DataAccess.Data;
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
    public class AssignmentModalitySubmodalityController : Controller

    {
        private readonly IUnitWork _unitWork;
        private readonly INotyfService _notyfService;
        private readonly ApplicationDbContext _db;

        public AssignmentModalitySubmodalityController(IUnitWork unitWork, INotyfService notyfService, ApplicationDbContext db)
        {
            _unitWork = unitWork;
            _notyfService = notyfService;
            _db = db;
        }
        enum Action
        {
            Create,
            Update,
            None
        }
        string msg = " ";
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult InsertOrUpdateAssignmentModalitySubmodality(int? id)
        {

            AssignmentModalitySubmodalityViewModel assignmentModalitySubmodalityViewModel = new AssignmentModalitySubmodalityViewModel()
            {

                AssignmentModalitySubmodality = new AssignmentModalitySubmodality(),

                StudentRequestList = _unitWork.StudentRequest.GetAll(sr => sr.Active == true, orderBy: sr => sr.OrderBy(sr => sr.Solicitude.TitleDegreeWork), includeProperties: "Solicitude").Select(sr => new SelectListItem
                {
                    Text = sr.Solicitude.TitleDegreeWork + " - " + "Acta No. " + sr.Solicitude.ActNumber,
                    Value = sr.Id.ToString()
                }).ToList(),

                ModalitySubmodalityList = _unitWork.ModalitySubmodality
                .GetAll(ms => ms.Active == true, orderBy: sm => sm.OrderBy(sm => sm.Modality.Name), includeProperties: "Modality,Submodality")
                .Select(ms => new SelectListItem
                {
                    Text = ms.Modality.Name + " " + ms.Submodality.Name,
                    Value = ms.Id.ToString()
                }).ToList(),

            };


            // Crea un nuevo registro
            if (id == null)
            {
                assignmentModalitySubmodalityViewModel.AssignmentModalitySubmodality.Active = true;
                return View(assignmentModalitySubmodalityViewModel);
            }

            // Actualiza el registro
            assignmentModalitySubmodalityViewModel.AssignmentModalitySubmodality = _unitWork.AssignmentModalitySubmodality.Get(id.GetValueOrDefault());
            if (assignmentModalitySubmodalityViewModel.AssignmentModalitySubmodality == null)
            {
                return NotFound();
            }

            return View(assignmentModalitySubmodalityViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult InsertOrUpdateAssignmentModalitySubmodality(AssignmentModalitySubmodalityViewModel assignmentModalitySubmodalityViewModel)
        {
            if (ModelState.IsValid)
            {
                Action action = Action.None;
                if (assignmentModalitySubmodalityViewModel.AssignmentModalitySubmodality.Id == 0)
                {
                    action = Action.Create;

                    try
                    {
                        _unitWork.AssignmentModalitySubmodality.Add(assignmentModalitySubmodalityViewModel.AssignmentModalitySubmodality);
                    }
                    catch (Exception es)
                    {
                        msg = "La modalidad y submodalidad seleccionadas ya se encuentran registrados al proyecto";
                        LoadLists(assignmentModalitySubmodalityViewModel, msg + es.Message);
                    }

                }
                else
                {
                    action = Action.Update;
                    try
                    {
                        _unitWork.AssignmentModalitySubmodality.Update(assignmentModalitySubmodalityViewModel.AssignmentModalitySubmodality);
                    }
                    catch (Exception)
                    {

                        msg = "La modalidad y submodalidad seleccionadas ya se encuentran registrados al proyecto";
                        LoadLists(assignmentModalitySubmodalityViewModel, msg);
                    }

                }
                try
                {
                    _unitWork.Save();
                    if (action == Action.Create)
                    {
                        _notyfService.Success("La modalidad y submodalidad registrada correctamente al proyecto.");
                    }
                    if (action == Action.Update)
                    {
                        _notyfService.Success("Modalidad y submodalidad del proyecto  actualizada correctamente.");
                    }

                    return RedirectToAction(nameof(Index));

                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("IX_AssignmentModalitySubmodality_ModalitySubmodalityId_StudentRequestId"))
                    {
                        var msg = "La modalidad y submodalidad seleccionada ya se encuientra asignada a la solicitud";
                        LoadLists(assignmentModalitySubmodalityViewModel, msg);
                    }
                    else
                    {
                        var msg = "La modalidad y submodalidad seleccionada ya se encuientra asignada a la solicitud";
                        LoadLists(assignmentModalitySubmodalityViewModel, msg);
                    }
                }
                catch (Exception exception)
                {
                    var msg = $"Se produjo un error de base de datos { exception}";
                    LoadLists(assignmentModalitySubmodalityViewModel, msg);
                }
            }
            else
            {
                var msg = "";
                LoadLists(assignmentModalitySubmodalityViewModel, msg);

                if (assignmentModalitySubmodalityViewModel.AssignmentModalitySubmodality.Id != 0)
                {
                    assignmentModalitySubmodalityViewModel.AssignmentModalitySubmodality = _unitWork.AssignmentModalitySubmodality.Get(assignmentModalitySubmodalityViewModel.AssignmentModalitySubmodality.Id);
                }
            }

            return View(assignmentModalitySubmodalityViewModel);
        }

        public ViewResult LoadLists(AssignmentModalitySubmodalityViewModel assignmentModalitySubmodalityViewModel, string message)
        {

            assignmentModalitySubmodalityViewModel.StudentRequestList = _unitWork.StudentRequest
                .GetAll(includeProperties: "Solicitude")
                .Select(sr => new SelectListItem
                {
                    Text = sr.Solicitude.TitleDegreeWork + " Acta No. " + sr.Solicitude.ActNumber,
                    Value = sr.Id.ToString()
                });

            assignmentModalitySubmodalityViewModel.ModalitySubmodalityList = _unitWork.ModalitySubmodality
                .GetAll(includeProperties: "Modality,Submodality")
                .Select(ams => new SelectListItem
                {
                    Text = ams.Modality.Name + " " + ams.Submodality.Name,
                    Value = ams.Id.ToString()
                });

            if (!string.IsNullOrEmpty(message))
            {
                _notyfService.Error(message);
            }

            return View(assignmentModalitySubmodalityViewModel);

        }

        #region API

        [HttpGet]
        public IActionResult GetAllAssignmentModalitySubmodalities()
        {
            var assignmentModalitySubmodalities = _unitWork.AssignmentModalitySubmodality
                .GetAll(includeProperties: "StudentRequest.Solicitude,ModalitySubmodality.Modality,ModalitySubmodality.Submodality");
            return Json(new { data = assignmentModalitySubmodalities });
        }

        //Eliminación de registro lógica
        [HttpPost]
        public IActionResult DeleteAssignmentModalitySubmodality(int id)
        {
            // Actualiza el registro
            var assignmentModalitySubmodalitytDb = _unitWork.AssignmentModalitySubmodality.Get(id);

            if (assignmentModalitySubmodalitytDb == null)
            {
                return Json(new { succes = false, message = "!!Error al borrar asignación de modalidad y submodalidad!!." });
            }

            assignmentModalitySubmodalitytDb.Active = false;
            _unitWork.AssignmentModalitySubmodality.Update(assignmentModalitySubmodalitytDb);
            _unitWork.Save();

            return Json(new { succes = true, message = "Asignación de modalidad y submodalidad  borrada exitosamente." });

        }

        #endregion
    }
}