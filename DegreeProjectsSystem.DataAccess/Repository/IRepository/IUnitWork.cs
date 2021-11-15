using System;

namespace DegreeProjectsSystem.DataAccess.Repository.IRepository
{
    public interface IUnitWork : IDisposable
    {
        IApplicationUserRepository ApplicationUser { get; }
        ICareerRepository Career { get; }
        ICareerPersonRepository CareerPerson { get; }
        ICityRepository City { get; }
        IDepartmentRepository Department{ get; }
        IDepartmentFacultyRepository DepartmentFaculty { get; }
        IEducationLevelRepository EducationLevel { get; }
        IEmailPersonRepository EmailPerson { get;  }
        IFacultyRepository Faculty { get; }
        IGenderRepository Gender { get;  }
        IIdentityDocumentTypeRepository IdentityDocumentType { get; }
        IInstitutionRepository Institution { get; }
        IInstitutionContactRepository InstitutionContact { get; }
        IInstitutionContactChargeRepository InstitutionContactCharge { get; }
        IInstitutionTypeRepository InstitutionType { get; }
        IModalityRepository Modality { get; }
        IPersonRepository Person { get; }
        IPersonTypePersonRepository PersonTypePerson { get; }
        IProgramTypeRepository ProgramType { get; }
        IRecognitionRepository Recognition { get; }
        ISolicitudeRepository Solicitude { get;  }
        IStudentRequestRepository StudentRequest { get; }
        ISubmodalityRepository Submodality { get; }
        IModalitySubmodalityRepository ModalitySubmodality { get; }
        ITeachingFunctionRepository TeachingFunction { get; }
        ITeachingAssignmentRepository TeachingAssignment { get; }
        ITypePersonRepository TypePerson { get; }
        void Save();
    }
}
