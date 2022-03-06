using DegreeProjectsSystem.Models;
namespace DegreeProjectsSystem.DataAccess.Repository.IRepository
{
    public interface IAssignmentModalitySubmodalityRepository : IRepository<AssignmentModalitySubmodality>
    {
        void Update(AssignmentModalitySubmodality assignmentModalitySubmodality);
    }
}
