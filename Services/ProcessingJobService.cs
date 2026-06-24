using Microsoft.EntityFrameworkCore;
using TraineeManagement.api.DTOs;
using TraineeManagement.api.Models;
using TraineeManagement.api.Data;
using TraineeManagement.api.Exceptions;

namespace TraineeManagement.api.Services
{
    public class ProcessingJobService
    {
        private readonly AppDbContext _appDbContext;

        private readonly ILogger<ProcessingJobService> _logger;

        public ProcessingJobService(AppDbContext AppDbContext, ILogger<ProcessingJobService> logger)
        {
            _appDbContext = AppDbContext;
            _logger = logger;
        }

        public async Task<ProcessingJobResponse> GetByIdAsync(int Id)
        {
            ProcessingJob? processingJob = await _appDbContext.ProcessingJobs.FindAsync(Id);
            if (processingJob==null)
            {
                _logger.LogInformation("ProcessingJob with id {} was not found", Id);
                throw new NotFoundException("ProcessingJob not found");
            }
            ProcessingJobResponse processingJobResponse = new ProcessingJobResponse(processingJob);
            _logger.LogInformation("ProcessingJob with id {} found", Id);
            return processingJobResponse;
        }
    }
}
