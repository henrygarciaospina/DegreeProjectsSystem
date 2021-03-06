using DegreeProjectsSystem.DataAccess.Repository.IRepository;
using DegreeProjectsSystem.DataAccess.Data;
using DegreeProjectsSystem.Models;
using System.Linq;

namespace DegreeProjectsSystem.DataAccess.Repository
{
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        private readonly ApplicationDbContext _db;

        public DepartmentRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Department department)
        {
            var departmentDb = _db.Departments.FirstOrDefault(d => d.Id == department.Id);
            if (departmentDb !=null)
            {
                departmentDb.Name = department.Name;
                departmentDb.Active = department.Active;
            }
        }

    }
}
