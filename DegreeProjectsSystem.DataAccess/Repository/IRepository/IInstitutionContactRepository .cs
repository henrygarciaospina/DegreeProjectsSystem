using DegreeProjectsSystem.Models;

namespace DegreeProjectsSystem.DataAccess.Repository.IRepository
{
    public interface IInstitutionContactRepository : IRepository<InstitutionContact>
    {
        void Update(InstitutionContact institutionContact);
    }
}
