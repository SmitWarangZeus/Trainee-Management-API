using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TraineeManagement.api.Models;

public class User
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "varchar(50)")]
    public string Username { get; set; } = null!;

    [Required]
    public string Email { get; set; } = null!;

    [Required]
    public string PasswordHash { get; set; } = null!;

    public void SetPassword(string plainPassword)
    {
        if (string.IsNullOrWhiteSpace(plainPassword))
        {
            throw new ArgumentException("Password cannot be empty");
        }
        PasswordHash = BCrypt.Net.BCrypt.HashPassword(plainPassword);
    }

    [Required]
    [Column(TypeName = "varchar(50)")]
    public string Role { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }
}
