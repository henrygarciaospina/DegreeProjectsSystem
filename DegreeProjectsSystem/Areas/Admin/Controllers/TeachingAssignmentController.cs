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
    public class TeachingAssignmentController : Controller
    {
        private readonly IUnitWork _unitWork;
        private readonly INotyfService _notyfService;
        private readonly ApplicationDbContext _db;

        public TeachingAssignmentController(IUnitWork unitWork, INotyfService notyfService, ApplicationDbContext db)
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

        public IActionResult InsertOrUpdateTeachingAssignment(int? id)
        {
            var config = _unitWork.Config.GetFirst();

            TeachingAssignmentViewModel teachingAssignmentViewModel = new TeachingAssignmentViewModel()
            {

                TeachingAssignment = new TeachingAssignment(),

                StudentRequestList = _unitWork.StudentRequest
                .GetAll(includeProperties: "Solicitude")
                .Select(sr => new SelectListItem
                {
                    Text = sr.Solicitude.TitleDegreeWork + " Acta No. " + sr.Solicitude.ActNumber,
                    Value = sr.Solicitude.Id.ToString()
                }),

                PersonTypePersonList = _unitWork.PersonTypePerson
                .GetAll(pe => pe.TypePerson.Id == config.TeacherTypeId, includeProperties: "Person")
                .Select(pe => new SelectListItem
                {
                    Text = pe.Person.Names + " " + pe.Person.Surnames,
                    Value = pe.Person.Id.ToString()
                }),

                TeachingFunctionList = _unitWork.TeachingFunction
                .GetAll(tf => tf.Active, orderBy: tf => tf.OrderBy(tf => tf.Name))
                .Select(tf => new SelectListItem
                {
                    Text = tf.Name,
                    Value = tf.Id.ToString()
                }),
            };

            // Crea un nuevo registro
            if (id == null)
            {
                teachingAssignmentViewModel.TeachingAssignment.Active = true;
                return View(teachingAssignmentViewModel);
            }

            // Actualiza el registro
            teachingAssignmentViewModel.TeachingAssignment = _unitWork.TeachingAssignment.Get(id.GetValueOrDefault());
            if (teachingAssignmentViewModel.TeachingAssignment == null)
            {
                return NotFound();
            }

            return View(teachingAssignmentViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult InsertOrUpdateTeachingAssignment(TeachingAssignmentViewModel teachingAssignmentViewModel)
        {

            if (ModelState.IsValid)
            {
                Action action = Action.None;
                if (teachingAssignmentViewModel.TeachingAssignment.Id == 0)
                {
                    action = Action.Create;
                    _unitWork.TeachingAssignment.Add(teachingAssignmentViewModel.TeachingAssignment);
                }
                else
                {
                    action = Action.Update;
                    _unitWork.TeachingAssignment.Update(teachingAssignmentViewModel.TeachingAssignment);
                }
                try
                {
                    _unitWork.Save();
                    if (action == Action.Create)
                    {
                        _notyfService.Success("Docente asignado correctamente.");
                    }
                    if (action == Action.Update)
                    {
                        _notyfService.Success("Asignación docente actualizada correctamente.");
                    }

                    return RedirectToAction(nameof(Index));

                }
                catch (DbUpdateException dbUpdateException)

                {


                    if (dbUpdateException.InnerException.Message.Contains("IX_TeachingAssigments_StudentRequestId_PersonTypePersonId"))
                    {
                        var msg = "El docente ingresado ya se encuentra asignado a esta solicitud";
                        LoadLists(teachingAssignmentViewModel, msg);
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
                LoadLists(teachingAssignmentViewModel, msg);

                if (teachingAssignmentViewModel.TeachingAssignment.Id != 0)
                {
                    teachingAssignmentViewModel.TeachingAssignment = _unitWork.TeachingAssignment.Get(teachingAssignmentViewModel.TeachingAssignment.Id);
                }
            }

            return View(teachingAssignmentViewModel);
        }

        public ViewResult LoadLists(TeachingAssignmentViewModel teachingAssignmentViewModel, string message)
        {
            var config = _unitWork.Config.GetFirst();

            teachingAssignmentViewModel.StudentRequestList = _unitWork.StudentRequest
                .GetAll(includeProperties: "Solicitude").Distinct()
                .Select(sr => new SelectListItem
                {
                    Text = sr.Solicitude.TitleDegreeWork + " Acta No. " + sr.Solicitude.ActNumber,
                    Value = sr.Solicitude.Id.ToString()
                });

            teachingAssignmentViewModel.PersonTypePersonList = _unitWork.PersonTypePerson
                .GetAll(pe => pe.TypePerson.Id == config.TeacherTypeId, includeProperties: "Person")
                .Select(pe => new SelectListItem
                {
                    Text = pe.Person.Names + " " + pe.Person.Surnames,
                    Value = pe.Person.Id.ToString()
                });


            teachingAssignmentViewModel.TeachingFunctionList = _unitWork.TeachingFunction
                .GetAll(tf => tf.Active == true, orderBy: tf => tf.OrderBy(tf => tf.Name))
                .Select(tf => new SelectListItem
                {
                    Text = tf.Name,
                    Value = tf.Id.ToString()
                });

            if (!string.IsNullOrEmpty(message))
            {
                _notyfService.Error(message);
            }

            return View(teachingAssignmentViewModel);

        }

        #region API

        [HttpGet]
        public IActionResult GetAllTeachingAssignments()
        {
            var teachingAssignments = _unitWork.TeachingAssignment.GetAll(includeProperties: "StudentRequest,PersonTypePerson,TeachingFunction");
            return Json(new { data = teachingAssignments });
        }

        //Eliminación de registro lógica
        [HttpPost]
        public IActionResult DeleteTeachingAssignment(int id)
        {
            // Actualiza el registro
            var teachingAssignmenttDb = _unitWork.TeachingAssignment.Get(id);

            if (teachingAssignmenttDb == null)
            {
                return Json(new { succes = false, message = "!!Error al borrar asignación de docente!!." });
            }

            teachingAssignmenttDb.Active = false;
            _unitWork.TeachingAssignment.Update(teachingAssignmenttDb);
            _unitWork.Save();

            return Json(new { succes = true, message = "Asignación de docente borrada exitosamente." });

        }

        #endregion
    }
}