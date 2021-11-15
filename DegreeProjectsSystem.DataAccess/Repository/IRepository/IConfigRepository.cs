using DegreeProjectsSystem.Models;

namespace DegreeProjectsSystem.DataAccess.Repository.IRepository
{
    public interface IConfigRepository : IRepository<Config>
    {
        void Update(Config config);
    }
}
