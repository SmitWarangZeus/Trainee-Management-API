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

        public async Task<PagedResponse<TraineeResponse>> GetAllAsync(PaginationParams paginationParams)
        {
            IQueryable<Trainee> query = _appDbContext.Trainees.AsQueryable();
            if (!string.IsNullOrEmpty(paginationParams.SearchTerm))
            {
                string searchLower = paginationParams.SearchTerm.ToLower();
                query = query.Where(t => t.Email.Contains(paginationParams.SearchTerm)
                || t.FirstName.ToLower().Contains(searchLower)
                || t.LastName.ToLower().Contains(searchLower)
                || t.TechStack.ToLower().Contains(searchLower));
            }
            if (!string.IsNullOrEmpty(paginationParams.Status))
            {
                query = query.Where(t => t.Status == paginationParams.Status);
            }
            int totalRecords = await query.CountAsync();
            List<TraineeResponse> trainees = await query.Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize).Take(paginationParams.PageSize).Select(t => new TraineeResponse(t)).AsNoTracking().ToListAsync();
            PagedResponse<TraineeResponse> pagedResponse = new PagedResponse<TraineeResponse>(trainees, paginationParams.PageNumber, paginationParams.PageSize, totalRecords);
            return pagedResponse;
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
