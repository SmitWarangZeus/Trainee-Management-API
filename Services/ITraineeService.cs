using TraineeManagement.api.DTOs;

namespace TraineeManagement.api.Services
{
    public interface ITraineeService
    {
        Task<IEnumerable<TraineeResponse>> GetAllAsync(string? search);
        Task<TraineeResponse?> GetByIdAsync(int Id);
        Task<TraineeResponse> CreateAsync(CreateTraineeRequest createTrainee);
        Task<TraineeResponse?> UpdateAsync(int Id, UpdateTraineeRequest updateTrainee);
        Task<bool> DeleteAsync(int Id);
    }
}
