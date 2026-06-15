using TraineeManagement.api.DTOs;
using TraineeManagement.api.Models;

namespace TraineeManagement.api.Services
{
    public interface ILearningTaskService
    {
        Task<PagedResponse<LearningTaskResponse>> GetAllAsync(PaginationParams paginationParams);
        Task<LearningTaskResponse> GetByIdAsync(int Id);
        Task<LearningTaskResponse> CreateAsync(CreateLearningTaskRequest createLearningTask);
        Task<LearningTaskResponse> UpdateAsync(int Id, UpdateLearningTaskRequest updateLearningTask);
        Task<bool> DeleteAsync(int Id);
    }
}
