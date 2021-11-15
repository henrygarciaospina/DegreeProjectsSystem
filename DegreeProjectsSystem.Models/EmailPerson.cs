using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DegreeProjectsSystem.Models
{
    public class EmailPerson
    {
        [Key]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe seleccionar una Persona")]
        public int PersonId { get; set; }

        [Display(Name = "Persona")]
        //Foreign key
        [ForeignKey("PersonId")]
        public Person Person { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar el correo de la Persona")]
        [EmailAddress(ErrorMessage = "El formato de correo ingresado es inválido")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener menos de {1} caracteres")]
        [Display(Name = "Correo")]
        public string Email { get; set; }

        [MaxLength(200, ErrorMessage = "El campo {0} debe tener menos de {1} caracteres")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Observaciones")]
        public string Observations { get; set; }

        [Display(Name = "Estado")]
        public bool Active { get; set; }
    }
}