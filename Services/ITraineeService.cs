using TraineeManagement.api.DTOs;

namespace TraineeManagement.api.Services
{
    public interface ITraineeService
    {
        List<TraineeResponse> GetAll();
        TraineeResponse? GetById(int Id);
        TraineeResponse Create(CreateTraineeRequest createTrainee);
        bool Update(int Id, UpdateTraineeRequest updateTrainee);
        bool Delete(int Id);
    }
}
