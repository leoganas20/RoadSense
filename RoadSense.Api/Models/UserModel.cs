namespace RoadSense.Api.Models;

public class UserModel
{
    public string Id { get; set; }
    public string? UserId { get; set; }
    public string? UserName { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? MiddleName { get; set; }
    public string? Suffix { get; set; }
    public string? Password { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Role { get; set; } 
    public string? PlateNumber { get; set; }
    public DateTime Created { get; set; }
}