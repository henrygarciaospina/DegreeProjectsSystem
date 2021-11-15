using DegreeProjectsSystem.Models;

namespace DegreeProjectsSystem.DataAccess.Repository.IRepository
{
    public interface ITeachingAssignmentRepository : IRepository<TeachingAssignment>
    {
        void Update(TeachingAssignment teachingAssignment);
    }
}
