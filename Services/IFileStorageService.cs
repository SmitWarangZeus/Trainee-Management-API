using TraineeManagement.api.DTOs;

namespace TraineeManagement.api.Services
{
    public interface IFileStorageService
    {
        Task<SubmissionFileResponse> SaveAsync(int SubmissionId, int userId, CreateSubmissionFileRequest createSubmissionFileRequest);
        Task<FileStream> OpenReadAsync(int Id);
        Task<bool> ExistsAsync(string path);
        Task<bool> DeleteAsync(int Id);
    }
}
