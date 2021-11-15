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
    public class InstitutionContactController : Controller
    {
        private readonly IUnitWork _unitWork;
        private readonly INotyfService _notyfService;
        private readonly ApplicationDbContext _db;

        public InstitutionContactController(IUnitWork unitWork, INotyfService notyfService, ApplicationDbContext db)
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

        public IActionResult InsertOrUpdateInstitutionContact(int? id)
        {
            InstitutionContactViewModel institutionContactViewModel = new InstitutionContactViewModel()
            {
                InstitutionContact = new InstitutionContact(),

                InstitutionList = _unitWork.Institution.GetAll(orderBy: i => i.OrderBy(i => i.Name)).Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),

                PersonList = _unitWork.Person.GetAll(orderBy: pe => pe.OrderBy(pe => pe.Surnames + pe.Names)).Select(pe => new SelectListItem
                {
                    Text = pe.Names + " " + pe.Surnames,
                    Value = pe.Id.ToString()
                }).ToList(),


                InstitutionContactChargeList = _unitWork.InstitutionContactCharge.GetAll(orderBy: icc => icc.OrderBy(icc => icc.Name)).Select(icc => new SelectListItem
                {
                    Text = icc.Name,
                    Value = icc.Id.ToString()
                })
            };

            // Crea un nuevo registro
            if (id == null)
            {
                institutionContactViewModel.InstitutionContact.Active = true;
                return View(institutionContactViewModel);
            }

            // Actualiza el registro
            institutionContactViewModel.InstitutionContact  = _unitWork.InstitutionContact.Get(id.GetValueOrDefault());
            if (institutionContactViewModel.InstitutionContact  == null)
            {
                return NotFound();
            }
            return View(institutionContactViewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult InsertOrUpdateInstitutionContact(InstitutionContactViewModel institutionContactViewModel)
        {
          
            if (ModelState.IsValid)
            {
                Action action = Action.None;
                if (institutionContactViewModel.InstitutionContact.Id == 0)
                {
                    action = Action.Create;
                    _unitWork.InstitutionContact.Add(institutionContactViewModel.InstitutionContact);
                }
                else
                {
                    action = Action.Update;
                    _unitWork.InstitutionContact.Update(institutionContactViewModel.InstitutionContact);
                }

                try
                {
                    _unitWork.Save();
                    if (action == Action.Create)
                    {
                        _notyfService.Success("Contacto institución creado correctamente.");
                    }
                    if (action == Action.Update)
                    {
                        _notyfService.Success("Contacto institución actualizado correctamente.");
                    }

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("IX_InstitutionContacts_InstitutionId_PersonId_InstitutionContactChargeId"))
                    {

                        _notyfService.Error("Ya existe una Contacto Institución con los mismos datos.");


                        institutionContactViewModel.InstitutionList = _unitWork.Institution.GetAll().Select(i => new SelectListItem
                        {
                            Text = i.Name,
                            Value = i.Id.ToString()
                        });

                        institutionContactViewModel.PersonList = _unitWork.Person.GetAll().Select(pe => new SelectListItem
                        {
                            Text = pe.Names + " " + pe.Surnames,
                            Value = pe.Id.ToString()
                        }).ToList();

                        institutionContactViewModel.InstitutionContactChargeList = _unitWork.InstitutionContactCharge.GetAll().Select(icc => new SelectListItem
                        {
                            Text = icc.Name,
                            Value = icc.Id.ToString()
                        });

                        institutionContactViewModel.InstitutionContactChargeList = _unitWork.InstitutionContactCharge.GetAll().OrderBy(ci => ci.Name).Select(icc => new SelectListItem
                        {
                            Text = icc.Name,
                            Value = icc.Id.ToString()
                        });

                        return View(institutionContactViewModel);
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
                institutionContactViewModel.PersonList = _unitWork.Person.GetAll().Select(pe => new SelectListItem
                {
                    Text = pe.Names + " " + pe.Surnames,
                    Value = pe.Id.ToString()
                }).ToList();

                institutionContactViewModel.InstitutionContactChargeList = _unitWork.InstitutionContactCharge.GetAll().Select(icc => new SelectListItem
                {
                    Text = icc.Name,
                    Value = icc.Id.ToString()
                });

                institutionContactViewModel.InstitutionContactChargeList = _unitWork.InstitutionContactCharge.GetAll().OrderBy(ci => ci.Name).Select(icc => new SelectListItem
                {
                    Text = icc.Name,
                    Value = icc.Id.ToString()
                });

                if (institutionContactViewModel.InstitutionContact.Id!=0)
                {
                    institutionContactViewModel.InstitutionContact = _unitWork.InstitutionContact.Get(institutionContactViewModel.InstitutionContact.Id);
                }
             }
            return View(institutionContactViewModel);

        }

        //Details InstitutionContact
        public IActionResult DetailInstitutionContact(int? id)
        {
            InstitutionContactViewModel institutionContactViewModel = new InstitutionContactViewModel()
            {
                InstitutionList = _unitWork.Institution.GetAll(orderBy: i => i.OrderBy(i => i.Name)).Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),

                PersonList = _unitWork.Person.GetAll(orderBy: pe => pe.OrderBy(pe => pe.Surnames + pe.Names)).Select(pe => new SelectListItem
                {
                    Text = pe.Names + " " + pe.Surnames,
                    Value = pe.Id.ToString()
                }).ToList(),

                InstitutionContactChargeList = _unitWork.InstitutionContactCharge.GetAll(orderBy: icc => icc.OrderBy(icc => icc.Name)).Select(icc => new SelectListItem
                {
                    Text = icc.Name,
                    Value = icc.Id.ToString()
                })

            };

            institutionContactViewModel.InstitutionContact = _unitWork.InstitutionContact.Get(id.GetValueOrDefault());
            if (institutionContactViewModel.InstitutionContact == null)
            {
                return NotFound();
            }

            return View(institutionContactViewModel);
        }

        #region API

        [HttpGet]
        public IActionResult GetAllInstitutionContacts()
        {
            var institutionContacts = _unitWork.InstitutionContact.GetAll(includeProperties: "Institution,Person,InstitutionContactCharge");
            return Json(new { data = institutionContacts });
        }

        //Eliminación de registro lógica
        [HttpPost]
        public IActionResult DeleteInstitutionContact(int id)
        {
            // Actualiza el registro
            var institutionContactDb = _unitWork.InstitutionContact.Get(id);

            if (institutionContactDb == null)
            {
                return Json(new { succes = false, message = "!!Error al borrar contacto institución!!." });
            }

            institutionContactDb.Active = false;
            _unitWork.InstitutionContact.Update(institutionContactDb);
            _unitWork.Save();


            return Json(new { succes = true, message = "Contacto institución borrado exitosamente." });

        }

        #endregion
    }
}