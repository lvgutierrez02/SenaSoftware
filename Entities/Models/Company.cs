using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Company
    {

        [Column("CompanyId")] //especificará que la propiedad Id se asignará con un nombre diferente en la base de datos
        public Guid Id { get; set; } //GUID: Tipo de dato id unico a nivel global
        [Required(ErrorMessage = "Company name is a required field.")] //declara la propiedad como obligatoria y está aquí para fines de validación
        [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")] //define su longitud máxima y está aquí para fines de validación
        public string Name { get; set; }
        [Required(ErrorMessage = "Company address is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Address is 60 characters")]
        public string Address { get; set; }
        public string Country { get; set; }
        public ICollection<Employee> Employees { get; set; } //propiedad de navegación: sirven para definir la relación entre nuestros modelos
    }
}

