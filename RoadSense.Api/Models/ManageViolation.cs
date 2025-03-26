namespace RoadSense.Api.Models;

public class ManageViolation
{
    public string Id { get; set; }
    public string? Info { get; set; }
    public string? PlateNumber { get; set; }
    public string? Name { get; set; }
    public string? Type { get; set; }
    public DateTime? Created { get; set; }
    public string? Status { get; set; }
}
