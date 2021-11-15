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

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe asignar un Estudiante a la Solicitud")]
        public int PersonId { get; set; }

        [Display(Name = "Estudiante")]
        //Foreign key
        [ForeignKey("PersonId")]
        public Person Person { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Observaciones")]
        public string Observations { get; set;  }
        
        [Display(Name = "Estado")]
        public bool Active { get; set; }
    }
}