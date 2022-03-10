using DegreeProjectsSystem.Models;
using System.Collections.Generic;

namespace DegreeProjectsSystem.DataAccess.Repository.IRepository
{
    public interface ITeachingAssignmentRepository : IRepository<TeachingAssignment>
    {
        void Update(TeachingAssignment teachingAssignment);
        List<TeachingAssignment> GetTeachersById(int teachingAssignmentId);
    }
}
