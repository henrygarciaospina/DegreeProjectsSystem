using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DegreeProjectsSystem.Models.ViewModels
{
    public class ModalityViewModel
    {
        public Modality Modality { get; set; }
        public IEnumerable<SelectListItem> EducationLevelList { get; set; }
    }
}
