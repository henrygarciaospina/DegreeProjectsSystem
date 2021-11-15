using DegreeProjectsSystem.DataAccess.Repository.IRepository;
using DegreeProjectsSystem.DataAccess.Data;
using DegreeProjectsSystem.Models;
using System.Linq;

namespace DegreeProjectsSystem.DataAccess.Repository
{
    public class InstitutionContactRepository : Repository<InstitutionContact>, IInstitutionContactRepository
    {
        private readonly ApplicationDbContext _db;

        public InstitutionContactRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(InstitutionContact institutionContact)
        {
            var institutionContactDb = _db.InstitutionContacts.FirstOrDefault(ic => ic.Id == institutionContact.Id);
            if (institutionContactDb != null)
            {
                institutionContactDb.InstitutionId = institutionContact.InstitutionId;
                institutionContactDb.PersonId = institutionContact.PersonId;
                institutionContactDb.InstitutionContactChargeId = institutionContact.InstitutionContactChargeId;
                institutionContactDb.Active = institutionContact.Active;
            }
        }

    }
}