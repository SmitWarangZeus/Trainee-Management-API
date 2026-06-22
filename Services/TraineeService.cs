using Microsoft.EntityFrameworkCore;
using TraineeManagement.api.DTOs;
using TraineeManagement.api.Models;
using TraineeManagement.api.Data;
using TraineeManagement.api.Exceptions;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace TraineeManagement.api.Services
{
    public class TraineeService : ITraineeService
    {
        private readonly AppDbContext _appDbContext;

        private readonly ILogger<TraineeService> _logger;

        private readonly RedisCacheService _cache;

        public TraineeService(AppDbContext AppDbContext, ILogger<TraineeService> logger, RedisCacheService cache)
        {
            _appDbContext = AppDbContext;
            _logger = logger;
            _cache = cache;
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
            List<TraineeResponse> trainees = await query.Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
            .Take(paginationParams.PageSize).Select(t => new TraineeResponse(t)).AsNoTracking().ToListAsync();
            PagedResponse<TraineeResponse> pagedResponse = new PagedResponse<TraineeResponse>(trainees, paginationParams.PageNumber, paginationParams.PageSize, totalRecords);
            return pagedResponse;
        }

        public async Task<TraineeResponse> GetByIdAsync(int Id)
        {
            string cacheKey = $"trainee:{Id}";
            TraineeResponse? cachedData = await _cache.GetKeyAsync<TraineeResponse>(cacheKey);
            if (cachedData!=null)
            {
                _logger.LogInformation("Cached trainee with id {} found", Id);
                return cachedData;
            }
            Trainee? trainee = await _appDbContext.Trainees.FindAsync(Id);
            if (trainee==null)
            {
                _logger.LogInformation("Trainee with id {} was not found", Id);
                throw new NotFoundException("Trainee not found");
            }
            TraineeResponse traineeResponse = new TraineeResponse(trainee);
            await _cache.SetKeyAsync(cacheKey, traineeResponse);
            _logger.LogInformation("Trainee with id {} found", Id);
            return traineeResponse;
        }

        public async Task<TraineeResponse> CreateAsync(CreateTraineeRequest createTrainee)
        {
            Trainee trainee = new Trainee(createTrainee);
            _appDbContext.Trainees.Add(trainee);
            await _appDbContext.SaveChangesAsync();
            _logger.LogInformation("Trainee with id {} created successfully", trainee.Id);
            return new TraineeResponse(trainee);
        }

        public async Task<TraineeResponse> UpdateAsync(int Id, UpdateTraineeRequest updateTrainee)
        {
            Trainee? trainee = await _appDbContext.Trainees.FindAsync(Id);
            if (trainee==null)
            {
                _logger.LogInformation("Trainee with id {} was not found", Id);
                throw new NotFoundException("Trainee not found");
            }
            trainee.FirstName = updateTrainee.FirstName;
            trainee.LastName = updateTrainee.LastName;
            trainee.Email = updateTrainee.Email;
            trainee.TechStack = updateTrainee.TechStack;
            trainee.Status = updateTrainee.Status;
            trainee.UpdatedDate = DateTime.UtcNow;
            await _appDbContext.SaveChangesAsync();

            string cacheKey = $"trainee:{Id}";
            TraineeResponse? cachedData = await _cache.GetKeyAsync<TraineeResponse>(cacheKey);
            if (cachedData!=null)
            {
                _logger.LogInformation("Cached trainee with id {} found", Id);
                TraineeResponse traineeResponse = new TraineeResponse(trainee);
                string serialized = JsonSerializer.Serialize(traineeResponse);
                await _cache.SetKeyAsync(cacheKey, serialized);
            }

            _logger.LogInformation("Trainee with id {} updated successfully", Id);
            return new TraineeResponse(trainee);
        }

        public async Task<bool> DeleteAsync(int Id)
        {
            Trainee? trainee = await _appDbContext.Trainees.FindAsync(Id);
            if (trainee==null)
            {
                _logger.LogInformation("Trainee with id {} was not found", Id);
                throw new NotFoundException("Trainee not found");
            }
            _appDbContext.Trainees.Remove(trainee);
            await _appDbContext.SaveChangesAsync();
            string cacheKey = $"trainee:{Id}";
            await _cache.DeleteKeyAsync(cacheKey);
            _logger.LogInformation("Trainee with id {} deleted successfully", Id);
            return true;
        }
    }
}
