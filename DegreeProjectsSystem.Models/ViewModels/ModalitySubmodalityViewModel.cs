using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace DegreeProjectsSystem.Models.ViewModels
{
    public class ModalitySubmodalityViewModel
    {
        public ModalitySubmodality ModalitySubmodality { get; set; }
        public IEnumerable<SelectListItem> ModalityList { get; set; }
        public IEnumerable<SelectListItem> SubmodalityList { get; set; }
    }
}
