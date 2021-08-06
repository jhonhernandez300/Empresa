using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Empresa.Models
{
    public class Client
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nombres requeridos")]
        [DataType(DataType.Text, ErrorMessage = "Los nombres deben ser un texto")]
        [StringLength(30, ErrorMessage = "Longitud máxima 30", MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Apellidos requeridos")]
        [DataType(DataType.Text, ErrorMessage = "Los apellidos deben ser un texto")]
        [StringLength(30, ErrorMessage = "Longitud máxima 30 y mínima 3", MinimumLength = 3)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Documento de identidad requeridos")]
        [DataType(DataType.Text, ErrorMessage = "El documento de identidad debe ser un número")]
        [Range(2, 15, ErrorMessage = "Debe ser un número con longitud entre {1} y {2}")]
        public int IdentityDocument { get; set; }
                
        [DataType(DataType.Date, ErrorMessage = "La fecha de nacimiento debe ser una fecha")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }
        
        [DataType(DataType.Text, ErrorMessage = "La dirección deben ser un texto")]
        [StringLength(30, ErrorMessage = "Longitud máxima 30 y mínima 10", MinimumLength = 10)]
        public string  Address { get; set; }
        
        [DataType(DataType.Text, ErrorMessage = "El celular deben ser un texto")]
        [StringLength(15, ErrorMessage = "Longitud máxima 15 y mínima 10", MinimumLength = 10)]
        public string CellPhone { get; set; }
        
        [DataType(DataType.Text, ErrorMessage = "El correo debe ser un texto")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "El correo no es válido")]
        [StringLength(30, ErrorMessage = "Longitud máxima 30 y mínima 9", MinimumLength = 9)]
        public string Email { get; set; }
        
        public bool Active { get; set; }
    }
}
