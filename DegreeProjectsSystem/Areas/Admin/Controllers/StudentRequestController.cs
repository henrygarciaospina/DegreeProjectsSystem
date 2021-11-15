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
    public class StudentRequestController : Controller
    {
        private readonly IUnitWork _unitWork;
        private readonly INotyfService _notyfService;
        private readonly ApplicationDbContext _db;

        public StudentRequestController(IUnitWork unitWork, INotyfService notyfService, ApplicationDbContext db)
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

        public IActionResult InsertOrUpdateStudentRequest(int? id)
        {
            StudentRequestViewModel studentRequestViewModel = new StudentRequestViewModel()
            {
                StudentRequest = new StudentRequest(),

                SolicitudeList = _unitWork.Solicitude.GetAll(sl => sl.Active == true, orderBy: sl => sl.OrderBy(sl => sl.TitleDegreeWork)).Select(sl => new SelectListItem
                {
                    Text = sl.TitleDegreeWork + " - " + "Acta No. " + sl.ActNumber,
                    Value = sl.Id.ToString()
                }).ToList(),

                PersonList = _unitWork.Person.GetAll(pe => pe.Active == true, orderBy: pe => pe.OrderBy(pe => pe.Surnames + pe.Names)).Select(pe => new SelectListItem
                {
                    Text = pe.Names + " " + pe.Surnames,
                    Value = pe.Id.ToString()
                }).ToList(),
            };

            // Crea un nuevo registro
            if (id == null)
            {
                studentRequestViewModel.StudentRequest.Active = true;
                return View(studentRequestViewModel);
            }

            // Actualiza el registro
            studentRequestViewModel.StudentRequest = _unitWork.StudentRequest.Get(id.GetValueOrDefault());
            if (studentRequestViewModel.StudentRequest == null)
            {
                return NotFound();
            }

            return View(studentRequestViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult InsertOrUpdateStudentRequest(StudentRequestViewModel studentRequestViewModel)
        {

            if (ModelState.IsValid)
            {
                Action action = Action.None;
                if (studentRequestViewModel.StudentRequest.Id == 0)
                {
                    action = Action.Create;
                    try
                    {
                        _unitWork.StudentRequest.Add(studentRequestViewModel.StudentRequest);
                    }
                    catch (Exception)
                    {
                        msg = "El estudiante y solicitud ingresado ya se encuentran registrados";
                        LoadLists(studentRequestViewModel, msg);
                    }

                }
                else
                {
                    action = Action.Update;
                    try
                    {
                        _unitWork.StudentRequest.Update(studentRequestViewModel.StudentRequest);
                    }
                    catch (Exception)
                    {
                        msg = "El estudiante y solicitud ingresado ya se encuentran registrados";
                        LoadLists(studentRequestViewModel, msg);
                    }

                }

                try
                {
                    _unitWork.Save();
                    if (action == Action.Create)
                    {
                        _notyfService.Success("Estudiante Solicitud creado correctamente.");
                    }
                    if (action == Action.Update)
                    {
                        _notyfService.Success("Estudiante Solictud actualizada correctamente.");
                    }

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("IX_StudentRequests_PersonId_Solicitude_Id"))
                    {
                        var msg = "El estudiante y solicitud ingresada ya se encuentran registrados";
                        LoadLists(studentRequestViewModel, msg);
                    }
                    else
                    {
                        var msg = "El estudiante y solicitud ingresada ya se encuentran registrados";
                        LoadLists(studentRequestViewModel, msg);
                    }
                }
                catch (Exception exception)
                {
                    var msg = $"Se produjo un error de base de datos { exception}";
                    LoadLists(studentRequestViewModel, msg);
                }
            }
            else
            {
                var msg = " ";
                LoadLists(studentRequestViewModel, msg);

                if (studentRequestViewModel.StudentRequest.Id != 0)
                {
                    studentRequestViewModel.StudentRequest = _unitWork.StudentRequest.Get(studentRequestViewModel.StudentRequest.Id);
                }
            }
            return View(studentRequestViewModel);
        }

        public ViewResult LoadLists(StudentRequestViewModel studentRequestViewModel, string message)
        {
            studentRequestViewModel.SolicitudeList = _unitWork.Solicitude.GetAll().Select(sl => new SelectListItem
            {
                Text = sl.TitleDegreeWork + " - " + "Acta No. " + sl.ActNumber,
                Value = sl.Id.ToString()
            }).ToList();

            studentRequestViewModel.PersonList = _unitWork.Person.GetAll(pe => pe.Active == true, orderBy: pe => pe.OrderBy(pe => pe.Surnames + pe.Names)).Select(pe => new SelectListItem
            {
                Text = pe.Names + " " + pe.Surnames,
                Value = pe.Id.ToString()
            }).ToList();

            if (!string.IsNullOrEmpty(message))
            {
                _notyfService.Error(message);
            }

            return View(studentRequestViewModel);

        }

        #region API

        [HttpGet]
        public IActionResult GetAllStudentRequests()
        {
            var studentRequests = _unitWork.StudentRequest.GetAll(includeProperties: "Solicitude,Person");
            return Json(new { data = studentRequests });
        }

        //Eliminación de registro lógica
        [HttpPost]
        public IActionResult DeleteStudentRequest(int id)
        {
            // Actualiza el registro
            var studentRequestDb = _unitWork.StudentRequest.Get(id);

            if (studentRequestDb == null)
            {
                return Json(new { succes = false, message = "!!Error al borrar solictud asignado a un Estudiante!!." });
            }

            studentRequestDb.Active = false;
            _unitWork.StudentRequest.Update(studentRequestDb);
            _unitWork.Save();

            return Json(new { succes = true, message = "Solicitud asignada a un Estudiante borrada exitosamente." });

        }

        #endregion
    }
}