using DegreeProjectsSystem.Models;

namespace DegreeProjectsSystem.DataAccess.Repository.IRepository
{
    public interface ITracingRepository : IRepository<Tracing>
    {
        void Update(Tracing tracing);
    }
}
