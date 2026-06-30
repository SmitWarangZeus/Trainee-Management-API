using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TraineeManagement.api.Models;

public class SubmissionFile
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int SubmissionId { get; set; }

    [Required]
    [Column(TypeName = "varchar(200)")]
    public string OriginalFileName { get; set; } = null!;

    [Required]
    [Column(TypeName = "varchar(50)")]
    public string StorageFileName { get; set; } = null!;

    [Required]
    [Column(TypeName = "varchar(50)")]
    public string ContentType { get; set; } = null!;

    [Required]
    public long SizeBytes { get; set; }

    [Required]
    [Column(TypeName = "varchar(200)")]
    public string Checksum { get; set; } = null!;

    [Required]
    public int UploadedBy { get; set; }

    public DateTime TimeStamp { get; set; }
}
