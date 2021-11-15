using DegreeProjectsSystem.DataAccess.Data;
using DegreeProjectsSystem.DataAccess.Repository.IRepository;
using DegreeProjectsSystem.Models;
using System.Linq;

namespace DegreeProjectsSystem.DataAccess.Repository
{
    public class EmailPersonRepository : Repository<EmailPerson>, IEmailPersonRepository
    {
        private readonly ApplicationDbContext _db;

        public EmailPersonRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(EmailPerson emailPerson)
        {
            var emailPersonDb = _db.EmailPeople.FirstOrDefault(epe => epe.Id == emailPerson.Id);
            if (emailPersonDb != null)
            {
                emailPersonDb.PersonId = emailPerson.PersonId;
                emailPersonDb.Email = emailPerson.Email;
                emailPersonDb.Observations = emailPerson.Observations;
                emailPersonDb.Active = emailPerson.Active;
            }
        }

    }
}
