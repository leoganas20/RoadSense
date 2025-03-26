using System;
using System.ComponentModel.DataAnnotations;

namespace RoadSense.UI.Models;

public class UserModel
{
    public string Id { get; set; }
     
    public string? UserId { get; set; }

    [Required(ErrorMessage = "Username is required.")]
    public string? UserName { get; set; }

    [Required(ErrorMessage = "First name is required.")]
    public string? FirstName { get; set; }

    [Required(ErrorMessage = "Last name is required.")]
    public string? LastName { get; set; }

    public string? MiddleName { get; set; }
    public string? Suffix { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    public string? Password { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Phone number is required.")]
    [Phone(ErrorMessage = "Invalid phone number.")]
    public string? PhoneNumber { get; set; }

    [Required(ErrorMessage = "Role is required.")]
    public string? Role { get; set; }

    public string? PlateNumber { get; set; }

    [Required]
    public DateTime Created { get; set; } = DateTime.UtcNow;
}