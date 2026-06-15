using Microsoft.AspNetCore.Mvc;
using TraineeManagement.api.DTOs;
using TraineeManagement.api.Services;
using Microsoft.AspNetCore.Authorization;

namespace TraineeManagement.api.Controllers;

[ApiController]
[Authorize]
[Route("/api/reviews")]
public class ReviewController : ControllerBase
{
    private readonly ReviewService _service;

    public ReviewController(ReviewService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        List<ReviewResponse> reviewResponses = await _service.GetAllAsync();
        return Ok(reviewResponses);
    }

    [HttpGet("{Id:int}")]
    public async Task<IActionResult> GetByIdAsync(int Id)
    {
        return Ok(await _service.GetByIdAsync(Id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateReviewRequest createReview)
    {
        ReviewResponse reviewResponse = await _service.CreateAsync(createReview);
        return Created($"/api/Reviews/{reviewResponse.Id}", reviewResponse);
    }
}
