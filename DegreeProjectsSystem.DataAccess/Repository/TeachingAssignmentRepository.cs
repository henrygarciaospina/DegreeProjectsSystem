using DegreeProjectsSystem.DataAccess.Repository.IRepository;
using DegreeProjectsSystem.DataAccess.Data;
using DegreeProjectsSystem.Models;
using System.Linq;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DegreeProjectsSystem.DataAccess.Repository
{
    public class TeachingAssignmentRepository : Repository<TeachingAssignment>, ITeachingAssignmentRepository
    {
        private readonly ApplicationDbContext _db;

        public TeachingAssignmentRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(TeachingAssignment teachingAssignment)
        {
            var teachingAssignmentDb = _db.TeachingAssigments.FirstOrDefault(ta => ta.Id == teachingAssignment.Id);
            try
            {
                if (teachingAssignmentDb != null)
                {
                    teachingAssignmentDb.SolicitudeId = teachingAssignment.SolicitudeId;
                    teachingAssignmentDb.PersonTypePersonId = teachingAssignment.PersonTypePersonId;
                    teachingAssignmentDb.TeachingFunctionId = teachingAssignment.TeachingFunctionId;
                    teachingAssignmentDb.AssigmentDate = teachingAssignment.AssigmentDate;
                    teachingAssignmentDb.Observations = teachingAssignment.Observations;
                    teachingAssignmentDb.Active = teachingAssignment.Active;
                }
            }
            catch (Exception ex)
            {

                throw(ex.InnerException);
            }
            
        }

        public List<TeachingAssignment> GetTeachersById(int teachingAssignmentId)
        {
            List<TeachingAssignment> teachings = new List<TeachingAssignment>();
            var teachingAssignmentDb = _db.TeachingAssigments.FirstOrDefault(ta => ta.Id == teachingAssignmentId);
            teachings = _db.TeachingAssigments.Where(w => w.SolicitudeId == teachingAssignmentDb.SolicitudeId)
                           .Include(so=> so.Solicitude)  
                           .Include(pt => pt.PersonTypePerson)
                           .Include(pe => pe.PersonTypePerson.Person)
                           .Include(tc => tc.PersonTypePerson.TypePerson)
                           .Include(tf=> tf.TeachingFunction)
                           .ToList();

            return teachings;
        }

    }
}