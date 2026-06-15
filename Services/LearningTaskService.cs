using Microsoft.EntityFrameworkCore;
using TraineeManagement.api.DTOs;
using TraineeManagement.api.Models;
using TraineeManagement.api.Data;
using TraineeManagement.api.Exceptions;

namespace TraineeManagement.api.Services
{
    public class LearningTaskService : ILearningTaskService
    {
        private readonly AppDbContext _appDbContext;

        private readonly ILogger<LearningTaskService> _logger;

        public LearningTaskService(AppDbContext AppDbContext, ILogger<LearningTaskService> logger)
        {
            _appDbContext = AppDbContext;
            _logger = logger;
        }

        public async Task<PagedResponse<LearningTaskResponse>> GetAllAsync(PaginationParams paginationParams)
        {
            IQueryable<LearningTask> query = _appDbContext.LearningTasks.AsQueryable();
            if (!string.IsNullOrEmpty(paginationParams.SearchTerm))
            {
                string searchLower = paginationParams.SearchTerm.ToLower();
                query = query.Where(t => t.Title.ToLower().Contains(searchLower)
                || t.Description.ToLower().Contains(searchLower)
                || t.ExpectedTechStack.ToLower().Contains(searchLower));
            }
            if (!string.IsNullOrEmpty(paginationParams.Status))
            {
                query = query.Where(t => t.Status == paginationParams.Status);
            }
            int totalRecords = await query.CountAsync();
            List<LearningTaskResponse> learningTasks = await query.Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
            .Take(paginationParams.PageSize).Select(t => new LearningTaskResponse(t)).AsNoTracking().ToListAsync();
            PagedResponse<LearningTaskResponse> pagedResponse = new PagedResponse<LearningTaskResponse>(learningTasks, paginationParams.PageNumber, paginationParams.PageSize, totalRecords);
            return pagedResponse;
        }

        public async Task<LearningTaskResponse> GetByIdAsync(int Id)
        {
            LearningTask? learningTask = await _appDbContext.LearningTasks.FindAsync(Id);
            if (learningTask==null)
            {
                _logger.LogInformation("LearningTask with id {} was not found", Id);
                throw new NotFoundException("Learning Task not found");
            }
            LearningTaskResponse learningTaskResponse = new LearningTaskResponse(learningTask);
            _logger.LogInformation("LearningTask with id {} found", Id);
            return learningTaskResponse;
        }

        public async Task<LearningTaskResponse> CreateAsync(CreateLearningTaskRequest createLearningTask)
        {
            LearningTask learningTask = new LearningTask(createLearningTask);
            _appDbContext.LearningTasks.Add(learningTask);
            await _appDbContext.SaveChangesAsync();
            _logger.LogInformation("LearningTask with id {} created successfully", learningTask.Id);
            return new LearningTaskResponse(learningTask);
        }

        public async Task<LearningTaskResponse> UpdateAsync(int Id, UpdateLearningTaskRequest updateLearningTask)
        {
            LearningTask? learningTask = await _appDbContext.LearningTasks.FindAsync(Id);
            if (learningTask==null)
            {
                _logger.LogInformation("LearningTask with id {} was not found", Id);
                throw new NotFoundException("Learning Task not found");
            }
            learningTask.Title = updateLearningTask.Title;
            learningTask.Description = updateLearningTask.Description;
            learningTask.ExpectedTechStack = updateLearningTask.ExpectedTechStack;
            learningTask.Status = updateLearningTask.Status;
            learningTask.UpdatedDate = DateTime.UtcNow;
            await _appDbContext.SaveChangesAsync();
            _logger.LogInformation("LearningTask with id {} updated successfully", Id);
            return new LearningTaskResponse(learningTask);
        }

        public async Task<bool> DeleteAsync(int Id)
        {
            LearningTask? learningTask = await _appDbContext.LearningTasks.FindAsync(Id);
            if (learningTask==null)
            {
                _logger.LogInformation("LearningTask with id {} was not found", Id);
                return false;
            }
            _appDbContext.LearningTasks.Remove(learningTask);
            await _appDbContext.SaveChangesAsync();
            _logger.LogInformation("LearningTask with id {} deleted successfully", Id);
            return true;
        }
    }
}
