using System.Security.Cryptography;
using TraineeManagement.api.Data;
using TraineeManagement.api.DTOs;
using TraineeManagement.api.Models;
using TraineeManagement.api.Exceptions;

namespace TraineeManagement.api.Services
{
    public class LocalFileStorageService : IFileStorageService
    {
        private readonly IConfiguration _config;

        private readonly string _baseDirectory;

        private readonly AppDbContext _appDbContext;

        private readonly string[] validExtensions = ["pdf", "xlsx", "docx", "txt"];

        private readonly long maxFileSize = 10*1024*1024;

        public LocalFileStorageService(AppDbContext appDbContext, IConfiguration config)
        {
            _appDbContext = appDbContext;
            _config = config;
            var baseDirectory = _config.GetSection("StorageRoot");
            _baseDirectory = Path.GetFullPath(baseDirectory["StoragePath"]!);
            Directory.CreateDirectory(_baseDirectory);
        }

        public async Task<SubmissionFileResponse> SaveAsync(int SubmissionId, int userId, CreateSubmissionFileRequest createSubmissionFileRequest)
        {
            Submission? submission = await _appDbContext.Submissions.FindAsync(SubmissionId);
            if (submission==null)
            {
                throw new NotFoundException($"Submission with id {SubmissionId} was not found");
            }
            if (!validExtensions.Contains(Path.GetExtension(createSubmissionFileRequest.File.FileName)))
            {
                throw new BadRequestException("Invalid file type");
            }
            if (createSubmissionFileRequest.File.Length > maxFileSize)
            {
                throw new ContentTooLargeException("File size exceeds 10mb");
            }

            var storageFileName = Guid.NewGuid().ToString("N") + Path.GetExtension(createSubmissionFileRequest.File.FileName);

            using var sha256 = SHA256.Create();
            using var stream = createSubmissionFileRequest.File.OpenReadStream();
            var checksum = BitConverter.ToString(sha256.ComputeHash(stream)).Replace("-", "").ToLower();
            stream.Position = 0;

            var metadata = new SubmissionFile
            {
                SubmissionId = SubmissionId,
                OriginalFileName = createSubmissionFileRequest.File.FileName,
                StorageFileName = storageFileName,
                ContentType = createSubmissionFileRequest.File.ContentType,
                SizeBytes = createSubmissionFileRequest.File.Length,
                Checksum = checksum,
                UploadedBy = userId,
                TimeStamp = DateTime.UtcNow
            };

            await _appDbContext.SubmissionFiles.AddAsync(metadata);
            await _appDbContext.SaveChangesAsync();

            string fullPath = GetFullPath(storageFileName);
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath)!);

            await using FileStream fileStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize : 4096, useAsync : true);
            await stream.CopyToAsync(fileStream);

            return new SubmissionFileResponse(metadata);
        }

        public async Task<FileStream> OpenReadAsync(int Id)
        {
            SubmissionFile submissionFile = await _appDbContext.SubmissionFiles.FindAsync(Id) ?? throw new NotFoundException($"Submission file with id {Id} was not found");
            if (!await ExistsAsync(submissionFile.StorageFileName))
            {
                throw new FileNotFoundException("File not found", submissionFile.StorageFileName);
            }

            string fullPath = GetFullPath(submissionFile.StorageFileName);
            return new FileStream(fullPath, FileMode.Open, FileAccess.Read, FileShare.Read);
        }

        public async Task<bool> ExistsAsync(string path)
        {
            string fullPath = GetFullPath(path);
            return File.Exists(fullPath);
        }

        public async Task<bool> DeleteAsync(int Id)
        {
            SubmissionFile submissionFile = await _appDbContext.SubmissionFiles.FindAsync(Id) ?? throw new NotFoundException($"Submission file with id {Id} was not found");
            string fullPath = GetFullPath(submissionFile.StorageFileName);
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
            return true;
        }

        private string GetFullPath(string relativePath)
        {
            string combined = Path.Combine(_baseDirectory, relativePath);
            string fullPath = Path.GetFullPath(combined);

            if (!fullPath.StartsWith(_baseDirectory, StringComparison.OrdinalIgnoreCase))
            {
                throw new UnauthorizedAccessException("Access outside base directory is not allowed.");
            }

            return fullPath;
        }
    }
}
