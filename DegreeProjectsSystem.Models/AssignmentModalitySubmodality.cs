using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DegreeProjectsSystem.Models
{
    public class AssignmentModalitySubmodality
    {
        [Key]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe seleccionar una solicitud")]
        public int StudentRequestId { get; set; }

        //Foreign key
        [ForeignKey("StudentRequestId")]
        public StudentRequest StudentRequest { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe seleccionar una modalidad/submodalidad")]
        public int ModalitySubmodalityId { get; set; }

        //Foreign key
        [ForeignKey("ModalitySubmodalityId")]
        public ModalitySubmodality ModalitySubmodality { get; set; }

        [MaxLength(200, ErrorMessage = "El campo {0} debe tener menos de {1} caracteres")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Observaciones")]
        public string Observations { get; set; }
        
        [Required]
        [Display(Name = "Estado")]
        public bool Active { get; set; }
    }
}
