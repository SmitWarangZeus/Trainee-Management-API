using Microsoft.AspNetCore.Mvc;
using TraineeManagement.api.DTOs;
using TraineeManagement.api.Services;
using TraineeManagement.api.Models;
using Microsoft.AspNetCore.Authorization;

namespace TraineeManagement.api.Controllers;

[ApiController]
[Authorize]
[Route("/api/mentors")]
public class MentorController : ControllerBase
{
    private readonly IMentorService _service;

    public MentorController(IMentorService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams paginationParams)
    {
        PagedResponse<MentorResponse> mentorResponses = await _service.GetAllAsync(paginationParams);
        return Ok(mentorResponses);
    }

    [HttpGet("{Id:int}")]
    public async Task<IActionResult> GetByIdAsync(int Id)
    {
        return Ok(await _service.GetByIdAsync(Id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateMentorRequest createMentor)
    {
        MentorResponse mentorResponse = await _service.CreateAsync(createMentor);
        return Created($"api/mentors/{mentorResponse.Id}", mentorResponse);
    }

    [HttpPut("{Id:int}")]
    public async Task<IActionResult> UpdateAsync(int Id, UpdateMentorRequest updateMentor)
    {
        return Ok(await _service.UpdateAsync(Id, updateMentor));
    }

    [HttpDelete("{Id:int}")]
    public async Task<IActionResult> DeleteAsync(int Id)
    {
        bool response = await _service.DeleteAsync(Id);
        return response ? NoContent() : NotFound();
    }
}
