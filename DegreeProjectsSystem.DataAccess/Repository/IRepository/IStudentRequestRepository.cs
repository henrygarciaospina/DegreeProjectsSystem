using DegreeProjectsSystem.Models;

namespace DegreeProjectsSystem.DataAccess.Repository.IRepository
{
    public interface IStudentRequestRepository : IRepository<StudentRequest>
    {
        void Update(StudentRequest studentRequest);
    }
}
