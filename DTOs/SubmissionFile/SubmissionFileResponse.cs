using TraineeManagement.api.Models;

namespace TraineeManagement.api.DTOs
{
    public class SubmissionFileResponse
    {
        public int Id { get; set; }

        public string OriginalFileName { get; set; } = null!;

        public string StorageFileName { get; set; } = null!;

        public string ContentType { get; set; } = null!;

        public long SizeBytes { get; set; }

        public string Checksum { get; set; } = null!;

        public int UploadedBy { get; set; }

        public DateTime TimeStamp { get; set; }

        public SubmissionFileResponse(SubmissionFile metadata)
        {
            Id = metadata.Id;
            OriginalFileName = metadata.OriginalFileName;
            StorageFileName = metadata.StorageFileName;
            ContentType = metadata.ContentType;
            SizeBytes = metadata.SizeBytes;
            Checksum = metadata.Checksum;
            UploadedBy = metadata.UploadedBy;
            TimeStamp = metadata.TimeStamp;
        }
    }
}
