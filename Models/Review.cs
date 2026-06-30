using System.ComponentModel.DataAnnotations;
using TraineeManagement.api.DTOs;
using System.ComponentModel.DataAnnotations.Schema;

namespace TraineeManagement.api.Models;

public class Review
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int SubmissionId { get; set; }

    [Required]
    public int MentorId { get; set; }

    [Required]
    [Column(TypeName = "varchar(200)")]
    public string Feedback { get; set; } = null!;

    public int Score { get; set; }

    [Required]
    [Column(TypeName = "varchar(50)")]
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
