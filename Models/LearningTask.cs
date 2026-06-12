using System.ComponentModel.DataAnnotations;
using TraineeManagement.api.DTOs;

namespace TraineeManagement.api.Models;

public class LearningTask
{
    [Key]
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string ExpectedTechStack { get; set; } = null!;

    public DateTime DueDate { get; set; }

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
