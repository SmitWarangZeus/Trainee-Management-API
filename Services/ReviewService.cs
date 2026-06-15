using Microsoft.EntityFrameworkCore;
using TraineeManagement.api.DTOs;
using TraineeManagement.api.Models;
using TraineeManagement.api.Data;
using TraineeManagement.api.Exceptions;

namespace TraineeManagement.api.Services
{
    public class ReviewService
    {
        private readonly AppDbContext _appDbContext;

        private readonly ILogger<ReviewService> _logger;

        public ReviewService(AppDbContext AppDbContext, ILogger<ReviewService> logger)
        {
            _appDbContext = AppDbContext;
            _logger = logger;
        }

        public async Task<List<ReviewResponse>> GetAllAsync()
        {
            return await _appDbContext.Reviews.Select(t => new ReviewResponse(t)).AsNoTracking().ToListAsync();
        }

        public async Task<ReviewResponse> GetByIdAsync(int Id)
        {
            Review? review = await _appDbContext.Reviews.FindAsync(Id);
            if (review==null)
            {
                _logger.LogInformation("Review with id {} was not found", Id);
                throw new NotFoundException("Review not found");
            }
            ReviewResponse reviewResponse = new ReviewResponse(review);
            _logger.LogInformation("Review with id {} found", Id);
            return reviewResponse;
        }

        public async Task<ReviewResponse> CreateAsync(CreateReviewRequest createReview)
        {
            Submission? submission = await _appDbContext.Submissions.FindAsync(createReview.SubmissionId);
            if (submission==null)
            {
                _logger.LogInformation("Submission with id {} found", createReview.SubmissionId);
                throw new NotFoundException("Submission not found");
            }
            Mentor? mentor = await _appDbContext.Mentors.FindAsync(createReview.MentorId);
            if (mentor==null)
            {
                _logger.LogInformation("Mentor with id {} found", createReview.MentorId);
                throw new NotFoundException("Mentor not found");
            }
            Review review = new Review(createReview);
            _appDbContext.Reviews.Add(review);
            await _appDbContext.SaveChangesAsync();
            _logger.LogInformation("Review with id {} created successfully", review.Id);
            return new ReviewResponse(review);
        }
    }
}
