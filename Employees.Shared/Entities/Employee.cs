using System.ComponentModel.DataAnnotations;

namespace Employees.Shared.Entities;

public class Employee
{
    public int Id { get; set; }

    [MaxLength(30, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres.")]
    [Required(ErrorMessage = "El campo{0} es obligatorio.")]
    public string FirstName { get; set; } = null!;

    [MaxLength(30, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres.")]
    [Required(ErrorMessage = "El campo{0} es obligatorio.")]
    public string LastName { get; set; } = null!;

    public bool IsActive { get; set; }
    public DateTime HireDate { get; set; }

    [Required(ErrorMessage = "El campo{0} es obligatorio.")]
    [Range(1000000, double.MaxValue, ErrorMessage = "El campo {0} debe ser mayor o igual a ${1}.")]
    public decimal Salary { get; set; }
}