using Microsoft.EntityFrameworkCore;
using TraineeManagement.api.DTOs;
using TraineeManagement.api.Models;
using TraineeManagement.api.Data;
using TraineeManagement.api.Exceptions;

namespace TraineeManagement.api.Services
{
    public class SubmissionService
    {
        private readonly AppDbContext _appDbContext;

        private readonly ILogger<SubmissionService> _logger;

        private readonly RedisCacheService _cache;

        public SubmissionService(AppDbContext AppDbContext, ILogger<SubmissionService> logger, RedisCacheService cache)
        {
            _appDbContext = AppDbContext;
            _logger = logger;
            _cache = cache;
        }

        public async Task<List<SubmissionResponse>> GetAllAsync()
        {
            return await _appDbContext.Submissions.Select(t => new SubmissionResponse(t)).AsNoTracking().ToListAsync();
        }

        public async Task<SubmissionResponse> GetByIdAsync(int Id)
        {
            string cacheKey = $"submission:{Id}";
            SubmissionResponse? cachedData = await _cache.GetKeyAsync<SubmissionResponse>(cacheKey);
            if (cachedData!=null)
            {
                _logger.LogInformation("Cached submission with id {} was not found", Id);
                return cachedData;
            }
            Submission? submission = await _appDbContext.Submissions.FindAsync(Id);
            if (submission==null)
            {
                _logger.LogInformation("Submission with id {} was not found", Id);
                throw new NotFoundException("Task assignment not found");
            }
            SubmissionResponse submissionResponse = new SubmissionResponse(submission);
            await _cache.SetKeyAsync(cacheKey, submissionResponse);
            _logger.LogInformation("Submission with id {} found", Id);
            return submissionResponse;
        }

        public async Task<SubmissionResponse> CreateAsync(CreateSubmissionRequest createSubmission)
        {
            TaskAssignment? taskAssignment = await _appDbContext.TaskAssignments.FindAsync(createSubmission.TaskAssignmentId);
            if (taskAssignment==null)
            {
                _logger.LogInformation("Task assignment with id {} was not found", createSubmission.TaskAssignmentId);
                throw new NotFoundException("Task assignment not found");
            }
            Submission submission = new Submission(createSubmission);
            _appDbContext.Submissions.Add(submission);
            await _appDbContext.SaveChangesAsync();
            _logger.LogInformation("Submission with id {} created successfully", submission.Id);
            return new SubmissionResponse(submission);
        }
    }
}
