using TraineeManagement.api.DTOs;
using TraineeManagement.api.Models;

namespace TraineeManagement.api.Services
{
    public interface IMentorService
    {
        Task<PagedResponse<MentorResponse>> GetAllAsync(PaginationParams paginationParams);
        Task<MentorResponse?> GetByIdAsync(int Id);
        Task<MentorResponse> CreateAsync(CreateMentorRequest createMentor);
        Task<MentorResponse?> UpdateAsync(int Id, UpdateMentorRequest updateMentor);
        Task<bool> DeleteAsync(int Id);
    }
}
