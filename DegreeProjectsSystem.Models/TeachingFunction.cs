using System.ComponentModel.DataAnnotations;

namespace DegreeProjectsSystem.Models
{
    public class TeachingFunction
    {
        [Key]
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar el nombre de una función docente")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener menos de {1} caracteres")]
        [Display(Name = "Función")]
        public string Name { get; set; }
     
        [Display(Name = "Estado")]
        public bool Active { get; set; }
    }
}
