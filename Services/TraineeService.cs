using Microsoft.EntityFrameworkCore;
using TraineeManagement.api.DTOs;
using TraineeManagement.api.Models;
using TraineeManagement.api.Data;

namespace TraineeManagement.api.Services
{
    public class TraineeService : ITraineeService
    {
        private readonly AppDbContext _appDbContext;

        public TraineeService(AppDbContext AppDbContext)
        {
            _appDbContext = AppDbContext;
        }

        public async Task<IEnumerable<TraineeResponse>> GetAllAsync(string? search)
        {
            IQueryable<Trainee> trainees = _appDbContext.Trainees.AsQueryable();
            if (!string.IsNullOrWhiteSpace(search))
            {
                string searchLower = search.ToLower();
                trainees = trainees.Where(t => t.Email.Contains(search)
                || t.FirstName.ToLower().Contains(searchLower)
                || t.LastName.ToLower().Contains(searchLower)
                || t.TechStack.ToLower().Contains(searchLower));
            }
            List<TraineeResponse> traineeResponses = await trainees.Select(t => new TraineeResponse(t)).AsNoTracking().ToListAsync();
            return traineeResponses;
        }

        public async Task<TraineeResponse?> GetByIdAsync(int Id)
        {
            Trainee? trainee = await _appDbContext.Trainees.FindAsync(Id);
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
            _appDbContext.Trainees.Add(trainee);
            await _appDbContext.SaveChangesAsync();
            return new TraineeResponse(trainee);
        }

        public async Task<TraineeResponse?> UpdateAsync(int Id, UpdateTraineeRequest updateTrainee)
        {
            Trainee? trainee = await _appDbContext.Trainees.FindAsync(Id);
            if (trainee==null)
            {
                return null;
            }
            trainee.FirstName = updateTrainee.FirstName;
            trainee.LastName = updateTrainee.LastName;
            trainee.Email = updateTrainee.Email;
            trainee.TechStack = updateTrainee.TechStack;
            trainee.Status = updateTrainee.Status;
            await _appDbContext.SaveChangesAsync();
            return new TraineeResponse(trainee);
        }

        public async Task<bool> DeleteAsync(int Id)
        {
            Trainee? trainee = await _appDbContext.Trainees.FindAsync(Id);
            if (trainee==null)
            {
                return false;
            }
            _appDbContext.Trainees.Remove(trainee);
            await _appDbContext.SaveChangesAsync();
            return true;
        }
    }
}
