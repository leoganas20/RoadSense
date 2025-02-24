namespace RoadSense.Api.Models;

public class LoginModel
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Role { get; set; }
}