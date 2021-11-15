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
using System.Collections.Generic;

namespace DegreeProjectsSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EmailPersonController : Controller
    {
        private readonly IUnitWork _unitWork;
        private readonly INotyfService _notyfService;
        private readonly ApplicationDbContext _db;

        public EmailPersonController(IUnitWork unitWork, INotyfService notyfService, ApplicationDbContext db)
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

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult InsertOrUpdateEmailPerson(int? id)
        {
            EmailPersonViewModel emailPersonViewModel = new EmailPersonViewModel()
            {
                EmailPerson = new EmailPerson(),

                PersonList = _unitWork.Person.GetAll(orderBy: pe => pe.OrderBy(pe => pe.Surnames + pe.Names)).Select(pe => new SelectListItem
                {
                    Text = pe.Names + " " + pe.Surnames,
                    Value = pe.Id.ToString()
                }).ToList()
            };

            // Crea un nuevo registro
            if (id == null)
            {
                emailPersonViewModel.EmailPerson.Active = true;
                return View(emailPersonViewModel);
            }

            // Actualiza el registro
            emailPersonViewModel.EmailPerson  = _unitWork.EmailPerson .Get(id.GetValueOrDefault());
            if (emailPersonViewModel.EmailPerson == null)
            {
                return NotFound();
            }
            return View(emailPersonViewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult InsertOrUpdateEmailPerson(EmailPersonViewModel emailPersonViewModel)
        {
            if (ModelState.IsValid)
            {
                Action action = Action.None;
                if (emailPersonViewModel.EmailPerson.Id == 0)
                {
                    action = Action.Create;
                    _unitWork.EmailPerson.Add(emailPersonViewModel.EmailPerson);
                }
                else
                {
                    action = Action.Update;
                    _unitWork.EmailPerson.Update(emailPersonViewModel.EmailPerson);
                }

                try
                {
                    _unitWork.Save();
                    if (action == Action.Create)
                    {
                        _notyfService.Success("Email Persona creado correctamente.");
                    }
                    if (action == Action.Update)
                    {
                        _notyfService.Success("Email Persona actualizado correctamente.");
                    }

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("IX_EmailPeople_Email"))
                    {

                        _notyfService.Error("Ya existe una Persona con el mismo correo.");

                        emailPersonViewModel.PersonList = _unitWork.Person.GetAll().Select(pe => new SelectListItem
                        {
                            Text = pe.Names + " " + pe.Surnames,
                            Value = pe.Id.ToString()
                        }).ToList();

                        return View(emailPersonViewModel);
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
                emailPersonViewModel.PersonList = _unitWork.Person.GetAll(orderBy: pe => pe.OrderBy(pe => pe.Surnames + pe.Names)).Select(pe => new SelectListItem
                {
                    Text = pe.Names + " " + pe.Surnames,
                    Value = pe.Id.ToString()
                }).ToList();

                if (emailPersonViewModel.EmailPerson.Id!=0)
                {
                    emailPersonViewModel.EmailPerson = _unitWork.EmailPerson.Get(emailPersonViewModel.EmailPerson.Id);
                }
             }
            return View(emailPersonViewModel);

        }

        #region API

        [HttpGet]
        public IActionResult GetAllEmailPeople()
        {
            var emailPeople = _unitWork.EmailPerson.GetAll(includeProperties: "Person");
            return Json(new { data = emailPeople });
        }

        //Eliminación de registro lógica
        [HttpPost]
        public IActionResult DeleteEmailPerson(int id)
        {
            // Actualiza el registro
            var emailPersonDb = _unitWork.EmailPerson.Get(id);

            if (emailPersonDb == null)
            {
                return Json(new { succes = false, message = "!!Error al borrar correo  asignado a una Persona!!." });
            }

            emailPersonDb.Active = false;
            _unitWork.EmailPerson.Update(emailPersonDb);
            _unitWork.Save();

            return Json(new { succes = true, message = "Correo asignado a un a Persona borrado exitosamente." });

        }

        #endregion
    }
}