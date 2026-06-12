using Microsoft.EntityFrameworkCore;
using TraineeManagement.api.DTOs;
using TraineeManagement.api.Models;
using TraineeManagement.api.Data;

namespace TraineeManagement.api.Services
{
    public class MentorService : IMentorService
    {
        private readonly AppDbContext _appDbContext;

        private readonly ILogger<MentorService> _logger;

        public MentorService(AppDbContext AppDbContext, ILogger<MentorService> logger)
        {
            _appDbContext = AppDbContext;
            _logger = logger;
        }

        public async Task<PagedResponse<MentorResponse>> GetAllAsync(PaginationParams paginationParams)
        {
            IQueryable<Mentor> query = _appDbContext.Mentors.AsQueryable();
            if (!string.IsNullOrEmpty(paginationParams.SearchTerm))
            {
                string searchLower = paginationParams.SearchTerm.ToLower();
                query = query.Where(t => t.Email.Contains(paginationParams.SearchTerm)
                || t.FirstName.ToLower().Contains(searchLower)
                || t.LastName.ToLower().Contains(searchLower)
                || t.Expertise.ToLower().Contains(searchLower));
            }
            if (!string.IsNullOrEmpty(paginationParams.Status))
            {
                query = query.Where(t => t.Status == paginationParams.Status);
            }
            int totalRecords = await query.CountAsync();
            List<MentorResponse> mentors = await query.Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize).Take(paginationParams.PageSize).Select(t => new MentorResponse(t)).AsNoTracking().ToListAsync();
            PagedResponse<MentorResponse> pagedResponse = new PagedResponse<MentorResponse>(mentors, paginationParams.PageNumber, paginationParams.PageSize, totalRecords);
            return pagedResponse;
        }

        public async Task<MentorResponse?> GetByIdAsync(int Id)
        {
            Mentor? mentor = await _appDbContext.Mentors.FindAsync(Id);
            if (mentor==null)
            {
                _logger.LogInformation("Mentor with id {} was not found", Id);
                return null;
            }
            MentorResponse mentorResponse = new MentorResponse(mentor);
            _logger.LogInformation("Mentor with id {} found", Id);
            return mentorResponse;
        }

        public async Task<MentorResponse> CreateAsync(CreateMentorRequest createMentor)
        {
            Mentor mentor = new Mentor(createMentor);
            _appDbContext.Mentors.Add(mentor);
            await _appDbContext.SaveChangesAsync();
            _logger.LogInformation("Mentor with id {} created successfully", mentor.Id);
            return new MentorResponse(mentor);
        }

        public async Task<MentorResponse?> UpdateAsync(int Id, UpdateMentorRequest updateMentor)
        {
            Mentor? mentor = await _appDbContext.Mentors.FindAsync(Id);
            if (mentor==null)
            {
                _logger.LogInformation("Mentor with id {} was not found", Id);
                return null;
            }
            mentor.FirstName = updateMentor.FirstName;
            mentor.LastName = updateMentor.LastName;
            mentor.Email = updateMentor.Email;
            mentor.Expertise = updateMentor.Expertise;
            mentor.Status = updateMentor.Status;
            mentor.UpdatedDate = DateTime.UtcNow;
            await _appDbContext.SaveChangesAsync();
            _logger.LogInformation("Mentor with id {} updated successfully", Id);
            return new MentorResponse(mentor);
        }

        public async Task<bool> DeleteAsync(int Id)
        {
            Mentor? mentor = await _appDbContext.Mentors.FindAsync(Id);
            if (mentor==null)
            {
                _logger.LogInformation("Mentor with id {} was not found", Id);
                return false;
            }
            _appDbContext.Mentors.Remove(mentor);
            await _appDbContext.SaveChangesAsync();
            _logger.LogInformation("Mentor with id {} deleted successfully", Id);
            return true;
        }
    }
}
