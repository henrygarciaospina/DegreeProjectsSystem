using DegreeProjectsSystem.Models;

namespace DegreeProjectsSystem.DataAccess.Repository.IRepository
{
    public interface IEmailPersonRepository : IRepository<EmailPerson>
    {
        void Update(EmailPerson emailPerson);
    }
}
