using Microsoft.AspNetCore.Mvc;
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
        public int SolicitudeId { get; set; }

        [Display(Name = "Solicitud")]
        //Foreign key
        [ForeignKey("SolicitudeId")]
        public Solicitude Solicitude { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe seleccionar un Docente")]
        public int PersonTypePersonId { get; set; }

        [Display(Name = "Nombres y Apellidos")]
        //Foreign key
        [ForeignKey("PersonTypePersonId")]
        public PersonTypePerson PersonTypePerson { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe seleccionar una función Docente")]
        public int TeachingFunctionId { get; set; }

        [Display(Name = "Función Docente")]
        //Foreign key
        [ForeignKey("TeachingFunctionId")]
        public TeachingFunction TeachingFunction { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha de asignación")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime AssigmentDate { get; set; }

        [MaxLength(200, ErrorMessage = "El campo {0} debe tener menos de {1} caracteres")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Observaciones")]
        public string Observations { get; set; }

        [Display(Name = "Estado")]
        public bool Active { get; set; }
    }
}