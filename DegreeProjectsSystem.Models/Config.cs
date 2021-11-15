using System.ComponentModel.DataAnnotations;

namespace DegreeProjectsSystem.Models
{
    public class Config
    {
        [Key]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe seleccionar tipo estudiante")]
        [Display(Name = "Estudiante")]
        public int StudenTypeId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe seleccionar tipo docente")]
        [Display(Name = "Docente")]
        public int TeacherTypeId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe seleccionar tipo contacto")]
        [Display(Name = "Contacto")]
        public int ContactTypeId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe seleccionar tipo administrativo")]
        [Display(Name = "Administrativo")]
        public int AdministrativeTypeId { get; set; }
    }
}
