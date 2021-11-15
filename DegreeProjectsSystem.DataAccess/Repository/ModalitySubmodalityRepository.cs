using DegreeProjectsSystem.DataAccess.Data;
using DegreeProjectsSystem.DataAccess.Repository.IRepository;
using DegreeProjectsSystem.Models;
using System.Linq;

namespace DegreeProjectsSystem.DataAccess.Repository
{
    public class ModalitySubmodalityRepository : Repository<ModalitySubmodality>, IModalitySubmodalityRepository
    {
        private readonly ApplicationDbContext _db;

        public ModalitySubmodalityRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ModalitySubmodality modalitySubmodality)
        {
            var modalitySubmodalityDb = _db.ModalitySubmodalities.FirstOrDefault(ms => ms.Id == modalitySubmodality.Id);
            if (modalitySubmodalityDb != null)
            {
                modalitySubmodalityDb.ModalityId = modalitySubmodality.ModalityId;
                modalitySubmodalityDb.SubmodalityId = modalitySubmodality.SubmodalityId;
                modalitySubmodalityDb.Active = modalitySubmodality.Active;
            }
        }

    }
}
