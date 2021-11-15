using DegreeProjectsSystem.Models;

namespace DegreeProjectsSystem.DataAccess.Repository.IRepository
{
    public interface IModalitySubmodalityRepository : IRepository<ModalitySubmodality>
    {
        void Update(ModalitySubmodality modalitySubmodality);
    }
}
