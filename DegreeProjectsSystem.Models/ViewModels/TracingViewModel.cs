using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace DegreeProjectsSystem.Models.ViewModels
{
    public class TracingViewModel
    {
        public Tracing Tracing { get; set; }
        public IEnumerable<SelectListItem> ModalitySubmodalityList { get; set; }
    }
}
