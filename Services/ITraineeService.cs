using TraineeManagement.api.DTOs;

namespace TraineeManagement.api.Services
{
    public interface ITraineeService
    {
        List<TraineeResponse> GetAll();
        TraineeResponse? GetById(int Id);
        TraineeResponse Create(CreateTraineeRequest createTrainee);
        // Task<bool> UpdateAsync(int Id, Trainee trainee);
        // Task<bool> DeleteAsync(int Id);
    }
}
