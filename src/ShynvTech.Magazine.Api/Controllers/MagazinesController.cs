using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace ShynvTech.Magazine.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MagazinesController : ControllerBase
{
    private readonly IWebHostEnvironment _environment;
    private readonly ILogger<MagazinesController> _logger;

    public MagazinesController(IWebHostEnvironment environment, ILogger<MagazinesController> logger)
    {
        _environment = environment;
        _logger = logger;
    }
    [HttpGet]
    public IActionResult GetMagazines()
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
    public IActionResult GetMagazine(int id)
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
    public IActionResult GetLatestMagazine()
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

    // PDF Streaming Endpoints

    [HttpGet("{id}/pdf")]
    public async Task<IActionResult> DownloadPdf(int id)
    {
        try
        {
            _logger.LogInformation("PDF download requested for magazine ID {MagazineId}", id);

            // Validate magazine exists (using existing logic)
            var magazineExists = await ValidateMagazineExists(id);
            if (!magazineExists)
            {
                _logger.LogWarning("Magazine with ID {MagazineId} not found", id);
                return NotFound($"Magazine with ID {id} not found");
            }

            // Build file path
            var filePath = Path.Combine(_environment.WebRootPath, "pdfs", $"magazine-{id}.pdf");

            if (!System.IO.File.Exists(filePath))
            {
                _logger.LogWarning("PDF file not found for magazine ID {MagazineId} at path {FilePath}", id, filePath);
                return NotFound("PDF file not available");
            }

            // Read and serve file
            var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
            var fileName = GetMagazineTitle(id) + ".pdf";

            _logger.LogInformation("Successfully served PDF download for magazine ID {MagazineId}", id);

            return File(fileBytes, "application/pdf", fileName);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error serving PDF download for magazine ID {MagazineId}", id);
            return StatusCode(500, "An error occurred while processing your request");
        }
    }

    [HttpGet("{id}/pdf/view")]
    public async Task<IActionResult> ViewPdf(int id)
    {
        try
        {
            _logger.LogInformation("PDF view requested for magazine ID {MagazineId}", id);

            // Validate magazine exists
            var magazineExists = await ValidateMagazineExists(id);
            if (!magazineExists)
            {
                return NotFound($"Magazine with ID {id} not found");
            }

            // Build file path
            var filePath = Path.Combine(_environment.WebRootPath, "pdfs", $"magazine-{id}.pdf");

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("PDF file not available");
            }

            // Stream file for inline viewing
            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            _logger.LogInformation("Successfully served PDF view for magazine ID {MagazineId}", id);
            // Set headers for inline viewing
            Response.Headers["Content-Disposition"] = "inline";
            return File(fileStream, "application/pdf");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error serving PDF view for magazine ID {MagazineId}", id);
            return StatusCode(500, "An error occurred while processing your request");
        }
    }

    [HttpHead("{id}/pdf")]
    public async Task<IActionResult> CheckPdfExists(int id)
    {
        try
        {
            // Validate magazine exists
            var magazineExists = await ValidateMagazineExists(id);
            if (!magazineExists) return NotFound();

            // Check if PDF file exists
            var filePath = Path.Combine(_environment.WebRootPath, "pdfs", $"magazine-{id}.pdf");
            var pdfExists = System.IO.File.Exists(filePath);

            return pdfExists ? Ok() : NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking PDF existence for magazine ID {MagazineId}", id);
            return StatusCode(500);
        }
    }

    // Enhanced PDF endpoints for year/month structure

    [HttpGet("{year}/{month}/pdf")]
    public async Task<IActionResult> DownloadPdfByDate(int year, string month)
    {
        try
        {
            _logger.LogInformation("PDF download requested for {Year}/{Month}", year, month);

            // Build file path for date-based structure
            var filePath = Path.Combine(_environment.WebRootPath, "pdfs", year.ToString(), month, "Shynvtech_Magazine.pdf");

            if (!System.IO.File.Exists(filePath))
            {
                _logger.LogWarning("PDF file not found at path {FilePath}", filePath);
                return NotFound($"PDF file not available for {month} {year}");
            }

            // Read and serve file
            var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
            var fileName = $"Shynvtech_Magazine_{month}_{year}.pdf";

            _logger.LogInformation("Successfully served PDF download for {Year}/{Month}", year, month);

            return File(fileBytes, "application/pdf", fileName);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error serving PDF download for {Year}/{Month}", year, month);
            return StatusCode(500, "An error occurred while processing your request");
        }
    }
    [HttpGet("{year}/{month}/pdf/view")]
    public IActionResult ViewPdfByDate(int year, string month)
    {
        try
        {
            _logger.LogInformation("PDF view requested for {Year}/{Month}", year, month);

            // Build file path for date-based structure
            var filePath = Path.Combine(_environment.WebRootPath, "pdfs", year.ToString(), month, "Shynvtech_Magazine.pdf");

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound($"PDF file not available for {month} {year}");
            }

            // Stream file for inline viewing
            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            _logger.LogInformation("Successfully served PDF view for {Year}/{Month}", year, month);

            // Set headers for inline viewing
            Response.Headers["Content-Disposition"] = "inline";
            return File(fileStream, "application/pdf");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error serving PDF view for {Year}/{Month}", year, month);
            return StatusCode(500, "An error occurred while processing your request");
        }
    }
    [HttpHead("{year}/{month}/pdf")]
    public IActionResult CheckPdfExistsByDate(int year, string month)
    {
        try
        {
            // Check if PDF file exists in date structure
            var filePath = Path.Combine(_environment.WebRootPath, "pdfs", year.ToString(), month, "Shynvtech_Magazine.pdf");
            var pdfExists = System.IO.File.Exists(filePath);

            return pdfExists ? Ok() : NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking PDF existence for {Year}/{Month}", year, month);
            return StatusCode(500);
        }
    }
    [HttpGet("archive")]
    public IActionResult GetMagazineArchive()
    {
        try
        {
            var archive = new List<object>();
            var pdfsPath = Path.Combine(_environment.WebRootPath, "pdfs");

            // Check for legacy flat structure
            for (int id = 1; id <= 3; id++)
            {
                var legacyPath = Path.Combine(pdfsPath, $"magazine-{id}.pdf");
                if (System.IO.File.Exists(legacyPath))
                {
                    archive.Add(new
                    {
                        Id = id,
                        Title = GetMagazineTitle(id),
                        Type = "legacy",
                        DownloadUrl = $"/api/magazines/{id}/pdf",
                        ViewUrl = $"/api/magazines/{id}/pdf/view"
                    });
                }
            }

            // Check for year/month structure
            if (Directory.Exists(pdfsPath))
            {
                var yearDirs = Directory.GetDirectories(pdfsPath)
                    .Where(d => int.TryParse(Path.GetFileName(d), out _))
                    .OrderByDescending(d => d);

                foreach (var yearDir in yearDirs)
                {
                    var year = Path.GetFileName(yearDir);
                    var monthDirs = Directory.GetDirectories(yearDir).OrderByDescending(d => d);

                    foreach (var monthDir in monthDirs)
                    {
                        var month = Path.GetFileName(monthDir);
                        var pdfPath = Path.Combine(monthDir, "Shynvtech_Magazine.pdf");

                        if (System.IO.File.Exists(pdfPath))
                        {
                            archive.Add(new
                            {
                                Year = int.Parse(year),
                                Month = month,
                                Title = $"ShynvTech Magazine - {month} {year}",
                                Type = "archived",
                                DownloadUrl = $"/api/magazines/{year}/{month}/pdf",
                                ViewUrl = $"/api/magazines/{year}/{month}/pdf/view"
                            });
                        }
                    }
                }
            }

            return Ok(archive);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving magazine archive");
            return StatusCode(500, "An error occurred while retrieving the archive");
        }
    }

    // Helper Methods

    private async Task<bool> ValidateMagazineExists(int id)
    {
        // Use existing magazine validation logic
        // For now, check against the hardcoded magazine IDs (1, 2, 3)
        return await Task.FromResult(id >= 1 && id <= 3);
    }

    private string GetMagazineTitle(int id)
    {
        // Get magazine title for filename
        return id switch
        {
            1 => "Tech_Innovations_2025",
            2 => "Career_Guidance_Special",
            3 => "Student_Life_Skills",
            _ => $"Magazine_{id}"
        };
    }
}
