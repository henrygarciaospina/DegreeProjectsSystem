using DegreeProjectsSystem.DataAccess.Repository.IRepository;
using DegreeProjectsSystem.DataAccess.Data;
using DegreeProjectsSystem.Models;
using System;
using System.Linq;

namespace DegreeProjectsSystem.DataAccess.Repository
{
    public class AssignmentModalitySubmodalityRepository : Repository<AssignmentModalitySubmodality>, IAssignmentModalitySubmodalityRepository
    {
        private readonly ApplicationDbContext _db;

        public AssignmentModalitySubmodalityRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(AssignmentModalitySubmodality assignmentModalitySubmodality)
        {
            var assignmentModalitySubmodalityDb = _db.AssignmentModalitySubmodalities.FirstOrDefault(asm => asm.Id == assignmentModalitySubmodality.Id);

            if (assignmentModalitySubmodalityDb != null)
            {
                assignmentModalitySubmodalityDb.ModalitySubmodalityId = assignmentModalitySubmodality.ModalitySubmodalityId;
                assignmentModalitySubmodalityDb.StudentRequestId = assignmentModalitySubmodality.StudentRequestId;
                assignmentModalitySubmodalityDb.Observations = assignmentModalitySubmodality.Observations;
                assignmentModalitySubmodalityDb.Active = assignmentModalitySubmodality.Active;
            }
        }

    }
}