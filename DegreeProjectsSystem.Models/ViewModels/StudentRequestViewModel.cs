using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace DegreeProjectsSystem.Models.ViewModels
{
    public class StudentRequestViewModel
    {
        public StudentRequest StudentRequest { get; set; }
        public IEnumerable<SelectListItem> SolicitudeList { get; set; }
        public IEnumerable<SelectListItem> PersonList { get; set; }
    }
}
