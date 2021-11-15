using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace DegreeProjectsSystem.Models.ViewModels
{
    public class TeachingAssignmentViewModel
    {
        public TeachingAssignment TeachingAssignment { get; set; }
        public IEnumerable<SelectListItem> StudentRequestList { get; set; }
        public IEnumerable<SelectListItem> PersonTypePersonList { get; set; }
        public IEnumerable<SelectListItem> TeachingFunctionList { get; set; }
    }
}
