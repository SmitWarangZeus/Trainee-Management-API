using TraineeManagement.api.DTOs;
using TraineeManagement.api.Models;

namespace TraineeManagement.api.Services
{
    public interface ITraineeService
    {
        Task<PagedResponse<TraineeResponse>> GetAllAsync(PaginationParams paginationParams);
        Task<TraineeResponse?> GetByIdAsync(int Id);
        Task<TraineeResponse> CreateAsync(CreateTraineeRequest createTrainee);
        Task<TraineeResponse?> UpdateAsync(int Id, UpdateTraineeRequest updateTrainee);
        Task<bool> DeleteAsync(int Id);
    }
}
