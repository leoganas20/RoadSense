using System.ComponentModel.DataAnnotations;

namespace RoadSense.UI.Models;

public class ManageViolationModel
{
    public string Id { get; set; }
    [Required(ErrorMessage = "Info is required.")]
    public string? Info { get; set; }

    [Required(ErrorMessage = "Plate Number is required.")]
    [StringLength(10, ErrorMessage = "Plate Number cannot exceed 10 characters.")]
    public string? PlateNumber { get; set; }

    [Required(ErrorMessage = "Name is required.")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Type is required.")]
    public string? Type { get; set; }

    public DateTime? Created { get; set; } = DateTime.Now;

    [Required(ErrorMessage = "Status is required.")]
    public string? Status { get; set; }
}
