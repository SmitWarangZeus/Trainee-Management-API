using System.ComponentModel.DataAnnotations;
using TraineeManagement.api.DTOs;

namespace TraineeManagement.api.Models;

public class Review
{
    [Key]
    public int Id { get; set; }

    public int SubmissionId { get; set; }

    public int MentorId { get; set; }

    public string Feedback { get; set; } = null!;

    public int Score { get; set; }

    public string ReviewStatus { get; set; } = null!;

    public DateTime ReviewedDate { get; set; }

    public Review(CreateReviewRequest createReview)
    {
        SubmissionId = createReview.SubmissionId;
        MentorId = createReview.MentorId;
        Feedback = createReview.Feedback;
        Score = createReview.Score;
        ReviewStatus = createReview.ReviewStatus;
        ReviewedDate = createReview.ReviewedDate;
    }

    public Review(){}
}
