using DegreeProjectsSystem.DataAccess.Data;
using DegreeProjectsSystem.DataAccess.Repository.IRepository;

namespace DegreeProjectsSystem.DataAccess.Repository
{
    public class UnitWork : IUnitWork
    {
        private readonly ApplicationDbContext _db;
        public IApplicationUserRepository ApplicationUser { get; private set; }

        public IAssignmentModalitySubmodalityRepository AssignmentModalitySubmodality { get; private set; }
        public ICareerRepository Career { get; private set; }
        public ICareerPersonRepository CareerPerson { get; private set; }
        public ICityRepository City { get; private set; }
        public IConfigRepository Config { get; private set; }
        public IDepartmentRepository Department { get; private set; }
        public IDepartmentFacultyRepository DepartmentFaculty { get; private set; }
        public IInstitutionRepository Institution { get; private set; }
        public IIdentityDocumentTypeRepository IdentityDocumentType { get; private set; }
        public IEducationLevelRepository EducationLevel { get; private set; }
        public IEmailPersonRepository EmailPerson { get; private set; }
        public IFacultyRepository Faculty { get; private set; }
        public IGenderRepository Gender { get; private set; }
        public IInstitutionContactRepository InstitutionContact { get; private set; }
        public IInstitutionContactChargeRepository InstitutionContactCharge { get; private set; }
        public IInstitutionTypeRepository InstitutionType { get; private set; }
        public IModalityRepository Modality { get; private set; }
        public IPersonRepository Person { get; private set; }
        public IPersonTypePersonRepository PersonTypePerson { get; private set; }
        public IProgramTypeRepository ProgramType { get; private set; }
        public IRecognitionRepository Recognition { get; private set; }
        public ISolicitudeRepository Solicitude { get; private set; }
        public IStudentRequestRepository StudentRequest { get; private set; }
        public ISubmodalityRepository Submodality { get; private set; }
        public IModalitySubmodalityRepository ModalitySubmodality { get; private set; }
        public ITeachingAssignmentRepository TeachingAssignment { get; private set; }
        public ITeachingFunctionRepository TeachingFunction { get; private set; }
        public ITracingRepository Tracing { get; private set; }
        public ITypePersonRepository TypePerson { get; private set; }
        public UnitWork(ApplicationDbContext db)
        {
            _db = db;
            ApplicationUser = new ApplicationUserRepository(_db);
            AssignmentModalitySubmodality = new AssignmentModalitySubmodalityRepository(_db);
            Career = new CareerRepository(_db); // Inicializamos
            CareerPerson = new CareerPersonRepository(_db);
            City = new CityRepository(_db);
            Config = new ConfigRepository(_db);
            Department = new DepartmentRepository(_db);
            DepartmentFaculty = new DepartmentFacultyRepository(_db);
            EducationLevel = new EducationLevelRepository(_db);
            EmailPerson = new EmailPersonRepository(_db);
            Faculty = new FacultyRepository(_db);
            Gender = new GenderRepository(_db);
            Institution = new InstitutionRepository(_db);
            IdentityDocumentType = new IdentityDocumentTypeRepository(_db);
            InstitutionContact = new InstitutionContactRepository(_db);
            InstitutionContactCharge = new InstitutionContactChargeRepository(_db);
            InstitutionType = new InstitutionTypeRepository(_db);
            Modality = new ModalityRepository(_db);
            Person = new PersonRepository(_db);
            PersonTypePerson = new PersonTypePersonRepository(_db);
            ProgramType = new ProgramTypeRepository(_db);
            Recognition = new RecognitionRepository(_db);
            Solicitude = new SolicitudeRepository(_db);
            StudentRequest = new StudentRequestRepository(_db);
            Submodality = new SubmodalityRepository(_db);
            ModalitySubmodality = new ModalitySubmodalityRepository(_db);
            TeachingFunction = new TeachingFunctionRepository(_db);
            TeachingAssignment = new TeachingAssignmentRepository(_db);
            Tracing = new TracingRepository(_db);
            TypePerson = new TypePersonRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
