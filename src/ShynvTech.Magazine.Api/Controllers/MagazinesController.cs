using Microsoft.AspNetCore.Mvc;

namespace ShynvTech.Magazine.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MagazinesController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetMagazines()
    {
        var magazines = new[]
        {
            new {
                Id = 1,
                Title = "Tech Innovations 2025",
                IssueDate = "January 2025",
                Description = "Latest technology trends and innovations for college students",
                CoverImageUrl = "/images/tech-2025-jan.jpg",
                PdfUrl = "/downloads/tech-2025-jan.pdf"
            },
            new {
                Id = 2,
                Title = "Career Guidance Special",
                IssueDate = "December 2024",
                Description = "Complete guide for career planning and job preparation",
                CoverImageUrl = "/images/career-guide-dec.jpg",
                PdfUrl = "/downloads/career-guide-dec.pdf"
            },
            new {
                Id = 3,
                Title = "Student Life & Skills",
                IssueDate = "November 2024",
                Description = "Essential life skills and study techniques for students",
                CoverImageUrl = "/images/student-life-nov.jpg",
                PdfUrl = "/downloads/student-life-nov.pdf"
            }
        };

        return Ok(magazines);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMagazine(int id)
    {
        // Mock data - in real app, this would come from database
        if (id == 1)
        {
            var magazine = new
            {
                Id = 1,
                Title = "Tech Innovations 2025",
                IssueDate = "January 2025",
                Description = "Latest technology trends and innovations for college students",
                CoverImageUrl = "/images/tech-2025-jan.jpg",
                PdfUrl = "/downloads/tech-2025-jan.pdf",
                Articles = new[]
                {
                    new { Title = "AI in Education", Author = "Dr. Sarah Johnson", Pages = "4-12" },
                    new { Title = "Future of Web Development", Author = "Mike Chen", Pages = "13-20" },
                    new { Title = "Cybersecurity Essentials", Author = "Emily Rodriguez", Pages = "21-28" }
                }
            };
            return Ok(magazine);
        }

        return NotFound();
    }

    [HttpGet("latest")]
    public async Task<IActionResult> GetLatestMagazine()
    {
        var latestMagazine = new
        {
            Id = 1,
            Title = "Tech Innovations 2025",
            IssueDate = "January 2025",
            Description = "Latest technology trends and innovations for college students",
            CoverImageUrl = "/images/tech-2025-jan.jpg",
            PdfUrl = "/downloads/tech-2025-jan.pdf"
        };

        return Ok(latestMagazine);
    }
}
