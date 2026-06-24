namespace TraineeManagement.api.Models;

public class ProcessingJobResponse
{
    public int Id { get; set; }

    public string CorrelationId { get; set; } = null!;

    public string Status { get; set; } = null!;

    public int Attempts { get; set; }

    public string ErrorSummary { get; set; } = null!;

    public DateTime StartedTime { get; set; }

    public DateTime CompletedTime { get; set; }

    public ProcessingJobResponse(ProcessingJob processingJob)
    {
        Id = processingJob.Id;
        CorrelationId = processingJob.CorrelationId;
        Status = processingJob.Status;
        Attempts = processingJob.Attempts;
        ErrorSummary = processingJob.ErrorSummary;
        StartedTime = processingJob.StartedTime;
        CompletedTime = processingJob.CompletedTime;
    }

    public ProcessingJobResponse(){}
}
