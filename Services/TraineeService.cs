using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TraineeManagement.api.DTOs;
using TraineeManagement.api.Models;

namespace TraineeManagement.api.Services
{
    public class TraineeService : ITraineeService
    {
        private readonly TraineeContext _traineeContext;

        public TraineeService(TraineeContext traineeContext)
        {
            _traineeContext = traineeContext;
        }

        public async Task<IEnumerable<TraineeResponse>> GetAllAsync(string? search)
        {
            // string search="smit";
            IQueryable<Trainee> trainees = from t in _traineeContext.Trainees select t;
            if (!string.IsNullOrWhiteSpace(search))
            {
                trainees = trainees.Where(t => t.Email.Contains(search)
                || t.FirstName.Contains(search,StringComparison.OrdinalIgnoreCase)
                || t.LastName.Contains(search,StringComparison.OrdinalIgnoreCase)
                || t.TechStack.Contains(search,StringComparison.OrdinalIgnoreCase));
            }
            List<TraineeResponse> traineeResponses = await trainees.Select(t => new TraineeResponse(t)).ToListAsync();
            return traineeResponses;
        }

        public async Task<TraineeResponse?> GetByIdAsync(int Id)
        {
            Trainee? trainee = await _traineeContext.Trainees.FindAsync(Id);
            if (trainee==null)
            {
                return null;
            }
            TraineeResponse traineeResponse = new TraineeResponse(trainee);
            return traineeResponse;
        }

        public async Task<TraineeResponse> CreateAsync(CreateTraineeRequest createTrainee)
        {
            Trainee trainee = new Trainee(createTrainee);
            _traineeContext.Trainees.Add(trainee);
            await _traineeContext.SaveChangesAsync();
            return new TraineeResponse(trainee);
        }

        public async Task<TraineeResponse?> UpdateAsync(int Id, UpdateTraineeRequest updateTrainee)
        {
            Trainee? trainee = await _traineeContext.Trainees.FindAsync(Id);
            if (trainee==null)
            {
                return null;
            }
            trainee.FirstName = updateTrainee.FirstName;
            trainee.LastName = updateTrainee.LastName;
            trainee.Email = updateTrainee.Email;
            trainee.TechStack = updateTrainee.TechStack;
            trainee.Status = updateTrainee.Status;
            await _traineeContext.SaveChangesAsync();
            return new TraineeResponse(trainee);
        }

        public async Task<bool> DeleteAsync(int Id)
        {
            Trainee? trainee = await _traineeContext.Trainees.FindAsync(Id);
            if (trainee==null)
            {
                return false;
            }
            _traineeContext.Trainees.Remove(trainee);
            await _traineeContext.SaveChangesAsync();
            return true;
        }
    }
}
