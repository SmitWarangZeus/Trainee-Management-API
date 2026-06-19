using Microsoft.AspNetCore.Mvc;
using TraineeManagement.api.Services;
using Microsoft.AspNetCore.Authorization;

namespace TraineeManagement.api.Controllers;

[ApiController]
[Authorize]
[Route("/api/submission-files")]
public class SubmissionFileController : ControllerBase
{
    private readonly IFileStorageService _service;

    public SubmissionFileController(IFileStorageService service)
    {
        _service = service;
    }

    [HttpGet("{Id:int}/download")]
    public async Task<IActionResult> DownloadFileAsync([FromRoute] int Id)
    {
        FileStream stream = await _service.OpenReadAsync(Id);
        string fileName = Path.GetFileName(stream.Name);
        string extension = Path.GetExtension(fileName);
        return File(stream, $"application/{extension[1..]}", fileName);
    }

    [HttpDelete("{Id:int}")]
    public async Task<IActionResult> DeleteAsync(int Id)
    {
        bool response = await _service.DeleteAsync(Id);
        return response ? NoContent() : NotFound();
    }
}
