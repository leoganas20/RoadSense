using System.ComponentModel.DataAnnotations;

namespace RoadSense.UI.Models;

public class UserLogin
{
    public int Id { get; set; }
    public string UserId { get; set; }

    [Required(ErrorMessage = "Username is required.")]
    [MinLength(1, ErrorMessage = "Username cannot be empty.")]
    public string UserName { get; set; }

    [Required]
    [StringLength(30, ErrorMessage = "Password must be at least 8 characters long.", MinimumLength = 8)]
    public string Password { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}