using System.ComponentModel.DataAnnotations;

namespace TraineeManagement.api.Models;

public class User
{
    [Key]
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public void SetPassword(string plainPassword)
    {
        if (string.IsNullOrWhiteSpace(plainPassword))
        {
            throw new ArgumentException("Password cannot be empty");
        }
        PasswordHash = BCrypt.Net.BCrypt.HashPassword(plainPassword);
    }

    public string Role { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }
}
