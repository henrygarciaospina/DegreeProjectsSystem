using DegreeProjectsSystem.DataAccess.Data;
using DegreeProjectsSystem.DataAccess.Repository.IRepository;
using DegreeProjectsSystem.Models;
using System.Linq;

namespace DegreeProjectsSystem.DataAccess.Repository
{
    public class ConfigRepository : Repository<Config>, IConfigRepository
    {
        private readonly ApplicationDbContext _db;

        public ConfigRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Config config)
        {
            var configDb = _db.Configs.FirstOrDefault(g => g.Id == config.Id);
            if (configDb != null)
            {
                configDb.StudenTypeId = config.StudenTypeId;
                configDb.TeacherTypeId = config.TeacherTypeId;
                configDb.ContactTypeId = config.ContactTypeId;
                configDb.AdministrativeTypeId = config.AdministrativeTypeId;
            }
        }

    }
}
