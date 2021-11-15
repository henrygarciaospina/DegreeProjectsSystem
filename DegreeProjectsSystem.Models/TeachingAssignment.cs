using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DegreeProjectsSystem.Models
{
    public class TeachingAssignment
    {
        [Key]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe seleccionar una Solicitud")]
        public int StudentRequestId { get; set; }

        [Display(Name = "Solicitud")]
        //Foreign key
        [ForeignKey("StudentRequestId")]
        public StudentRequest StudentRequest { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe seleccionar un Docente")]
        public int PersonId { get; set; }

        [Display(Name = "Nombres y Apellidos")]
        //Foreign key
        [ForeignKey("PersonId")]
        public Person Person { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe seleccionar una función Docente")]
        public int TeachingFunctionId { get; set; }

        [Display(Name = "Función Docente")]
        //Foreign key
        [ForeignKey("TeachingFunctionId")]
        public TeachingFunction TeachingFunction { get; set; }

        [MaxLength(200, ErrorMessage = "El campo {0} debe tener menos de {1} caracteres")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Observaciones")]
        public DateTime AssigmentDate { get; set; }
        public string Observations { get; set; }

        [Display(Name = "Estado")]
        public bool Active { get; set; }
    }
}