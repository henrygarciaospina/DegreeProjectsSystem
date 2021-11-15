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
    public class ModalitySubmodalityController : Controller
    {
        private readonly IUnitWork _unitWork;
        private readonly INotyfService _notyfService;
        private readonly ApplicationDbContext _db;

        public ModalitySubmodalityController(IUnitWork unitWork, INotyfService notyfService, ApplicationDbContext db)
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

        public IActionResult InsertOrUpdateModalitySubmodality(int? id)
        {
            ModalitySubmodalityViewModel modalitySubmodalityViewModel = new ModalitySubmodalityViewModel()
            {
                ModalitySubmodality = new ModalitySubmodality(),

                ModalityList = _unitWork.Modality.GetAll(orderBy: mo => mo.OrderBy(mo => mo.Name)).Select(mo => new SelectListItem
                {
                    Text = mo.Name,
                    Value = mo.Id.ToString()
                }),

                SubmodalityList = _unitWork.Submodality.GetAll(orderBy: su => su.OrderBy(su => su.Name)).Select(su => new SelectListItem
                {
                    Text = su.Name,
                    Value = su.Id.ToString()
                }).ToList(),
            };

            // Crea un nuevo registro
            if (id == null)
            {
                modalitySubmodalityViewModel.ModalitySubmodality.Active = true;
                return View(modalitySubmodalityViewModel);
            }

            // Actualiza el registro
            modalitySubmodalityViewModel.ModalitySubmodality  = _unitWork.ModalitySubmodality .Get(id.GetValueOrDefault());
            if (modalitySubmodalityViewModel.ModalitySubmodality  == null)
            {
                return NotFound();
            }
            return View(modalitySubmodalityViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult InsertOrUpdateModalitySubmodality(ModalitySubmodalityViewModel modalitySubmodalityViewModel)
        {
            if (ModelState.IsValid)
            {
                Action action = Action.None;
                if (modalitySubmodalityViewModel.ModalitySubmodality.Id == 0)
                {
                    action = Action.Create;
                    _unitWork.ModalitySubmodality.Add(modalitySubmodalityViewModel.ModalitySubmodality);
                }
                else
                {
                    action = Action.Update;
                    _unitWork.ModalitySubmodality.Update(modalitySubmodalityViewModel.ModalitySubmodality);
                }

                try
                {
                    _unitWork.Save();
                    if (action == Action.Create)
                    {
                        _notyfService.Success("Modalidad/Submodalidad creada correctamente.");
                    }
                    if (action == Action.Update)
                    {
                        _notyfService.Success("Modalidad/Submodalidad actualizada correctamente.");
                    }

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("IX_ModalitySubmodalities_ModalityId_SubmodalityId"))
                    {

                        _notyfService.Error("Ya existe la submodalidad ingresada asociada la modalidad .");

                        modalitySubmodalityViewModel.ModalityList = _unitWork.Modality.GetAll().Select(mo => new SelectListItem
                        {
                            Text = mo.Name,
                            Value = mo.Id.ToString()
                        });

                        modalitySubmodalityViewModel.SubmodalityList = _unitWork.Submodality.GetAll().Select(su => new SelectListItem
                        {
                            Text = su.Name,
                            Value = su.Id.ToString()
                        }).ToList();

                        return View(modalitySubmodalityViewModel);
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
                modalitySubmodalityViewModel.ModalityList = _unitWork.Modality.GetAll().Select(mo => new SelectListItem
                {
                    Text = mo.Name,
                    Value = mo.Id.ToString()
                });

                modalitySubmodalityViewModel.SubmodalityList = _unitWork.Submodality.GetAll(orderBy: su => su.OrderBy(su => su.Name)).Select(su => new SelectListItem
                {
                    Text = su.Name,
                    Value = su.Id.ToString()
                });

                if (modalitySubmodalityViewModel.ModalitySubmodality.Id!=0)
                {
                    modalitySubmodalityViewModel.ModalitySubmodality = _unitWork.ModalitySubmodality.Get(modalitySubmodalityViewModel.ModalitySubmodality.Id);
                }
             }
            return View(modalitySubmodalityViewModel);

        }

        #region API

        [HttpGet]
        public IActionResult GetAllModalitySubmodalities()
        {
            var modalitySubmodalities = _unitWork.ModalitySubmodality.GetAll(includeProperties: "Modality,Submodality");
            return Json(new { data = modalitySubmodalities });
        }

        //Eliminación de registro lógica
        [HttpPost]
        public IActionResult DeleteModalitySubmodality(int id)
        {
            // Actualiza el registro
            var modalitySubmodalityDb = _unitWork.ModalitySubmodality.Get(id);

            if (modalitySubmodalityDb == null)
            {
                return Json(new { succes = false, message = "!!Error al borrar Modalidad/Submodalidad!!." });
            }

            modalitySubmodalityDb.Active = false;
            _unitWork.ModalitySubmodality.Update(modalitySubmodalityDb);
            _unitWork.Save();

            return Json(new { succes = true, message = "Modalidad/Submodalidad borrada exitosamente." });

        }

        #endregion
    }
}