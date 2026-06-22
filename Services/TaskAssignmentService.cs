using Microsoft.EntityFrameworkCore;
using TraineeManagement.api.DTOs;
using TraineeManagement.api.Models;
using TraineeManagement.api.Data;
using TraineeManagement.api.Exceptions;
using System.Text.Json;

namespace TraineeManagement.api.Services
{
    public class TaskAssignmentService
    {
        private readonly AppDbContext _appDbContext;

        private readonly ILogger<TaskAssignmentService> _logger;

        private readonly RedisCacheService _cache;

        public TaskAssignmentService(AppDbContext AppDbContext, ILogger<TaskAssignmentService> logger, RedisCacheService cache)
        {
            _appDbContext = AppDbContext;
            _logger = logger;
            _cache = cache;
        }

        public async Task<List<TaskAssignmentResponse>> GetAllAsync()
        {
            return await _appDbContext.TaskAssignments.Select(t => new TaskAssignmentResponse(t)).AsNoTracking().ToListAsync();
        }

        public async Task<TaskAssignmentResponse> GetByIdAsync(int Id)
        {
            string cacheKey = $"taskAssignment:{Id}";
            TaskAssignmentResponse? cachedData = await _cache.GetKeyAsync<TaskAssignmentResponse>(cacheKey);
            if (cachedData!=null)
            {
                _logger.LogInformation("Cached trainee with id {} was not found", Id);
                return cachedData;
            }
            TaskAssignment? taskAssignment = await _appDbContext.TaskAssignments.FindAsync(Id);
            if (taskAssignment==null)
            {
                _logger.LogInformation("TaskAssignment with id {} was not found", Id);
                throw new NotFoundException("Task assignment not found");
            }
            TaskAssignmentResponse taskAssignmentResponse = new TaskAssignmentResponse(taskAssignment);
            await _cache.SetKeyAsync(cacheKey, taskAssignmentResponse);
            _logger.LogInformation("TaskAssignment with id {} found", Id);
            return taskAssignmentResponse;
        }

        public async Task<TaskAssignmentResponse?> CreateAsync(CreateTaskAssignmentRequest createTaskAssignment)
        {
            Trainee? trainee = await _appDbContext.Trainees.FindAsync(createTaskAssignment.TraineeId);
            if (trainee==null)
            {
                _logger.LogInformation("Trainee with id {} was not found", createTaskAssignment.TraineeId);
                throw new NotFoundException("Trainee not found");
            }
            Mentor? mentor = await _appDbContext.Mentors.FindAsync(createTaskAssignment.MentorId);
            if (mentor==null)
            {
                _logger.LogInformation("Mentor with id {} was not found", createTaskAssignment.TraineeId);
                throw new NotFoundException("Mentor not found");
            }
            LearningTask? learningTask = await _appDbContext.LearningTasks.FindAsync(createTaskAssignment.LearningTaskId);
            if (learningTask==null)
            {
                _logger.LogInformation("LearningTask with id {} was not found", createTaskAssignment.TraineeId);
                throw new NotFoundException("Task assignment not found");
            }
            if (createTaskAssignment.DueDate<createTaskAssignment.AssignedDate)
            {
                _logger.LogInformation("Due date should be greater than Assigned date");
                return null;
            }
            TaskAssignment taskAssignment = new TaskAssignment(createTaskAssignment);
            _appDbContext.TaskAssignments.Add(taskAssignment);
            await _appDbContext.SaveChangesAsync();
            _logger.LogInformation("TaskAssignment with id {} created successfully", taskAssignment.Id);
            return new TaskAssignmentResponse(taskAssignment);
        }

        public async Task<TaskAssignmentResponse> UpdateAsync(int Id, UpdateTaskAssignmentRequest updateTaskAssignment)
        {
            TaskAssignment? taskAssignment = await _appDbContext.TaskAssignments.FindAsync(Id);
            if (taskAssignment==null)
            {
                _logger.LogInformation("TaskAssignment with id {} was not found", Id);
                throw new NotFoundException("Task assignment not found");
            }
            taskAssignment.Status = updateTaskAssignment.Status;
            await _appDbContext.SaveChangesAsync();

            string cacheKey = $"taskAssignment:{Id}";
            TaskAssignmentResponse? cachedData = await _cache.GetKeyAsync<TaskAssignmentResponse>(cacheKey);
            if (cachedData!=null)
            {
                _logger.LogInformation("Cached trainee with id {} found", Id);
                TaskAssignmentResponse taskAssignmentResponse = new TaskAssignmentResponse(taskAssignment);
                string serialized = JsonSerializer.Serialize(taskAssignmentResponse);
                await _cache.SetKeyAsync(cacheKey, serialized);
            }
            
            _logger.LogInformation("TaskAssignment with id {} updated successfully", Id);
            return new TaskAssignmentResponse(taskAssignment);
        }
    }
}
