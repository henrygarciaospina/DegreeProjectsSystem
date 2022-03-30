using DegreeProjectsSystem.DataAccess.Data;
using DegreeProjectsSystem.DataAccess.Repository.IRepository;
using DegreeProjectsSystem.Models;
using System.Linq;

namespace DegreeProjectsSystem.DataAccess.Repository
{
    public class TracingRepository : Repository<Tracing>, ITracingRepository
    {
        private readonly ApplicationDbContext _db;

        public TracingRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Tracing tracing)
        {
            var tracingDb = _db.Tracings.FirstOrDefault(t => t.Id == tracing.Id);
            if (tracingDb != null)
            {
                tracingDb.ModalitySubmodalityId = tracing.ModalitySubmodalityId;
                tracingDb.DeliveryDescription = tracing.DeliveryDescription;
                tracingDb.Active = tracing.Active;
            }
        }

    }
}