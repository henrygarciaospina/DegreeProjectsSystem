using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DegreeProjectsSystem.Models
{
    public class ModalitySubmodality
    {
        [Key]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe seleccionar una Modalidad")]
        public int ModalityId { get; set; }

        [Display(Name = "Modalidad")]
        //Foreign key
        [ForeignKey("ModalityId")]
        public Modality Modality { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe seleccionar una Submodalidad")]
        public int SubmodalityId { get; set; }

        [Display(Name = "Submodalidad")]
        //Foreign key
        [ForeignKey("SubmodalityId")]
        public Submodality Submodality { get; set; }

        [Display(Name = "Estado")]
        public bool Active { get; set; }
    }
}