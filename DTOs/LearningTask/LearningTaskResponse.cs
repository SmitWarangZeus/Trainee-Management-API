using TraineeManagement.api.Models;

namespace TraineeManagement.api.DTOs
{
    public class LearningTaskResponse
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string ExpectedTechStack { get; set; } = null!;

        public DateTime DueDate { get; set; }

        public string Status { get; set; } = null!;

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public LearningTaskResponse(LearningTask learningTask)
        {
            Id = learningTask.Id;
            Title = learningTask.Title;
            Description = learningTask.Description;
            ExpectedTechStack = learningTask.ExpectedTechStack;
            DueDate = learningTask.DueDate;
            Status = learningTask.Status;
            CreatedDate = learningTask.CreatedDate;
            UpdatedDate = learningTask.UpdatedDate;
        }

        public LearningTaskResponse(){}
    }
}
