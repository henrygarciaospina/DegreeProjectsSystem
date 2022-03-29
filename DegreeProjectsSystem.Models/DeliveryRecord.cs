using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DegreeProjectsSystem.Models
{
    //Registro Entrega
    public class DeliveryRecord
    {
        [Key]
        public int Id { get; set; }
        
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe seleccionar un Requerimiento de Estudiante")]

        public int StudentRequestId { get; set; }

        [Display(Name = "Requerimiento Estudiante")]
        //Foreign key
        [ForeignKey("StudentRequestId")]
        public StudentRequest StudentRequest { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe seleccionar un seguimiento")]
        public int TracingId { get; set; }

        [Display(Name = "Seguimiento")]
        //Foreign key
        [ForeignKey("TracingId")]
        public Tracing Tracing { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha de entrega")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DeliveryDate { get; set; }


        public string FileUrl { get; set; }

        [MaxLength(500, ErrorMessage = "El campo {0} debe tener menos de {1} caracteres")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Observaciones")]
        public string Observations { get; set; }
    }
}
