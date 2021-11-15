using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace DegreeProjectsSystem.Models.ViewModels
{
    public class EmailPersonViewModel
    {
        public EmailPerson EmailPerson { get; set; }
        public IEnumerable<SelectListItem> PersonList { get; set; }
    }
}
