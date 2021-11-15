using DegreeProjectsSystem.DataAccess.Repository.IRepository;
using DegreeProjectsSystem.DataAccess.Data;
using DegreeProjectsSystem.Models;
using System.Linq;

namespace DegreeProjectsSystem.DataAccess.Repository
{
    public class TeachingAssignmentRepository : Repository<TeachingAssignment>, ITeachingAssignmentRepository
    {
        private readonly ApplicationDbContext _db;

        public TeachingAssignmentRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(TeachingAssignment teachingAssignment)
        {
            var teachingAssignmentDb = _db.TeachingAssigments.FirstOrDefault(ta => ta.Id == teachingAssignment.Id);
            try
            {
                if (teachingAssignmentDb != null)
                {
                    teachingAssignmentDb.StudentRequest.Id = teachingAssignment.StudentRequestId;
                    teachingAssignmentDb.PersonTypePersonId = teachingAssignment.PersonTypePersonId;
                    teachingAssignmentDb.TeachingFunctionId = teachingAssignment.TeachingFunctionId;
                    teachingAssignmentDb.AssigmentDate = teachingAssignment.AssigmentDate;
                    teachingAssignmentDb.Observations = teachingAssignment.Observations;
                    teachingAssignmentDb.Active = teachingAssignment.Active;
                }
            }
            catch (System.Exception ex)
            {

                throw(ex);
            }
            
        }

    }
}