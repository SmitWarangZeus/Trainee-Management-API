using System.ComponentModel.DataAnnotations;
using TraineeManagement.api.DTOs;

namespace TraineeManagement.api.Models;

public class TaskAssignment
{
    [Key]
    public int Id { get; set; }

    public int TraineeId { get; set; }

    public int MentorId { get; set; }

    public int LearningTaskId { get; set; }

    public DateTime AssignedDate { get; set; }

    public DateTime DueDate { get; set; }

    public string Status { get; set; } = null!;

    public string Remarks { get; set; } = null!;

    public TaskAssignment(CreateTaskAssignmentRequest createTaskAssignment)
    {
        TraineeId = createTaskAssignment.TraineeId;
        MentorId = createTaskAssignment.MentorId;
        LearningTaskId = createTaskAssignment.LearningTaskId;
        AssignedDate = createTaskAssignment.AssignedDate;
        DueDate = createTaskAssignment.DueDate;
        Status = createTaskAssignment.Status;
        Remarks = createTaskAssignment.Remarks;
    }

    public TaskAssignment(){}
}
