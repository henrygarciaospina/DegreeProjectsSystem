using System.Collections.Generic;

namespace DegreeProjectsSystem.Models.ViewModels
{
    public class DetailsTeachingAssignmentView
    {
        public Solicitude Solicitude { get; set; }
        public List<Person> Teachers { get; set; }

        public List<TeachingFunction> TeachingFunctions { get; set; }
    }
}
