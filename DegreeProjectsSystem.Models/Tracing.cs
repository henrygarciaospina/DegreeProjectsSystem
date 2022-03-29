using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DegreeProjectsSystem.Models
{
    //Seguimiento
    public class Tracing
    {
        [Key]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe seleccionar una Modalidad/Submodalidad")]
        public int ModalitySubmodalityId { get; set; }

        [Display(Name = "Modalidad/Submodalidad")]
        //Foreign key
        [ForeignKey("ModalitySubmodalityId")]
        public ModalitySubmodality ModalitySubmodality { get; set; }


        [MaxLength(500, ErrorMessage = "El campo {0} debe tener menos de {1} caracteres")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Descripción entrega")]
        public string DeliveryDescription { get; set; }

        [Display(Name = "Estado")]
        public bool Active { get; set; }
    }
}
