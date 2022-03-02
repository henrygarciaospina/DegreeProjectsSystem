using DegreeProjectsSystem.DataAccess.Data;
using DegreeProjectsSystem.DataAccess.Repository.IRepository;
using DegreeProjectsSystem.Models;
using System.Linq;

namespace DegreeProjectsSystem.DataAccess.Repository
{
    public class StudentRequestRepository : Repository<StudentRequest>, IStudentRequestRepository
    {
        private readonly ApplicationDbContext _db;

        public StudentRequestRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(StudentRequest studentRequest)
        {
            var studentRequestDb = _db.StudentRequests.FirstOrDefault(sre => sre.Id == studentRequest.Id);
            if (studentRequestDb != null)
            {
                studentRequestDb.SolicitudeId = studentRequest.SolicitudeId;
                studentRequestDb.PersonTypePersonId = studentRequest.PersonTypePersonId;
                studentRequestDb.Observations = studentRequest.Observations;
                studentRequestDb.Active = studentRequest.Active;
            }
        }

    }
}
