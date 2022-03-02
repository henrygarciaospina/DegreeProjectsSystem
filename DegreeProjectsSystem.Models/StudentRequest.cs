using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DegreeProjectsSystem.Models
{
    public class StudentRequest
    {
        [Key]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe seleccionar una Solicitud")]
        public int SolicitudeId { get; set; }

        [Display(Name = "Solicitud")]
        //Foreign key
        [ForeignKey("SolicitudeId")]
        public Solicitude Solicitude { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe seleccionar un Estudiante")]
        public int PersonTypePersonId { get; set; }

        [Display(Name = "Nombres y Apellidos")]
        //Foreign key
        [ForeignKey("PersonTypePersonId")]
        public PersonTypePerson PersonTypePerson { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Observaciones")]
        public string Observations { get; set;  }
        
        [Display(Name = "Estado")]
        public bool Active { get; set; }
    }
}