using System.ComponentModel.DataAnnotations;
using TraineeManagement.api.DTOs;
using System.ComponentModel.DataAnnotations.Schema;

namespace TraineeManagement.api.Models;

public class TaskAssignment
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int TraineeId { get; set; }

    [Required]
    public int MentorId { get; set; }

    [Required]
    public int LearningTaskId { get; set; }

    public DateTime AssignedDate { get; set; }

    public DateTime DueDate { get; set; }

    [Required]
    [Column(TypeName = "varchar(50)")]
    public string Status { get; set; } = null!;

    [Required]
    [Column(TypeName = "varchar(50)")]
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
