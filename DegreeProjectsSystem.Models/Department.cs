using System.ComponentModel.DataAnnotations;

namespace DegreeProjectsSystem.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar el nombre de un Departamento")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener menos de {1} caracteres")]
        [Display(Name ="Departamento")]
        public string Name { get; set; }
        
        [Display(Name = "Estado")]
        public bool Active { get; set; }
    }
}
