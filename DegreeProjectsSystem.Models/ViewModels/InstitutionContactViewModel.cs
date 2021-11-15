using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace DegreeProjectsSystem.Models.ViewModels
{
    public class InstitutionContactViewModel
    {
        public InstitutionContact InstitutionContact { get; set; }
        public IEnumerable<SelectListItem> InstitutionList { get; set; }
        public IEnumerable<SelectListItem> PersonList { get; set; }
        public IEnumerable<SelectListItem> InstitutionContactChargeList { get; set; }
    }
}
