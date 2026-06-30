using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TraineeManagement.api.DTOs;

namespace TraineeManagement.api.Models;

public class LearningTask
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "varchar(50)")]
    public string Title { get; set; } = null!;

    [Required]
    [Column(TypeName = "varchar(200)")]
    public string Description { get; set; } = null!;

    [Required]
    [Column(TypeName = "varchar(50)")]
    public string ExpectedTechStack { get; set; } = null!;

    [Required]
    public DateTime DueDate { get; set; }

    [Required]
    [Column(TypeName = "varchar(50)")]
    public string Status { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public LearningTask(CreateLearningTaskRequest createLearningTask)
    {
        Title = createLearningTask.Title;
        Description = createLearningTask.Description;
        ExpectedTechStack = createLearningTask.ExpectedTechStack;
        DueDate = createLearningTask.DueDate;
        Status = createLearningTask.Status;
        CreatedDate = DateTime.UtcNow;
        UpdatedDate = DateTime.UtcNow;
    }

    public LearningTask(){}
}
