using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace DegreeProjectsSystem.Models.ViewModels
{
    public class AssignmentModalitySubmodalityViewModel
    {
        public AssignmentModalitySubmodality AssignmentModalitySubmodality { get; set; }
        public IEnumerable<SelectListItem> StudentRequestList { get; set; }
        public IEnumerable<SelectListItem> ModalitySubmodalityList { get; set; }
    }
}
