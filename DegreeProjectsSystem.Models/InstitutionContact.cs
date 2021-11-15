using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DegreeProjectsSystem.Models
{
    public class InstitutionContact
    {
        [Key]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe seleccionar una Institución")]
        public int InstitutionId { get; set; }

        [Display(Name = "Institución")]
        //Foreign key
        [ForeignKey("InstitutionId")]
        public Institution Institution { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe seleccionar un Contacto...")]
        public int PersonId { get; set; }

        [Display(Name = "Contacto")]
        //Foreign key
        [ForeignKey("PersonId")]
        public Person Person { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe seleccionar el cargo del Contacto...")]
        public int InstitutionContactChargeId { get; set; }

        [Display(Name = "Cargo")]
        //Foreign key
        [ForeignKey("InstitutionContactChargeId")]
        public InstitutionContactCharge InstitutionContactCharge { get; set; }

        [Display(Name = "Estado")]
        public bool Active { get; set; }
    }
}
