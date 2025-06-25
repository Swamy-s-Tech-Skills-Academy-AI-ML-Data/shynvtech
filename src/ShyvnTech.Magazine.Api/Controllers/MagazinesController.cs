using Microsoft.AspNetCore.Mvc;

namespace ShyvnTech.Magazine.Api.Controllers;

/// <summary>
/// Controller for managing ShyvnTech Magazine content and PDF downloads using hierarchical structure (year/month).
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class MagazinesController : ControllerBase
{
    private readonly IWebHostEnvironment _environment;
    private readonly ILogger<MagazinesController> _logger;

    public MagazinesController(IWebHostEnvironment environment, ILogger<MagazinesController> logger)
    {
        _environment = environment;
        _logger = logger;
    }

    /// <summary>
    /// Gets a list of all available magazines.
    /// </summary>
    /// <returns>A list of magazines with basic information and download links.</returns>
    /// <response code="200">Returns the list of available magazines</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
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
                PdfUrl = "/api/magazines/2025/July/pdf"
            },
            new {
                Id = 2,
                Title = "Career Guidance Special",
                IssueDate = "August 2025",
                Description = "Complete guide for career planning and job preparation",
                CoverImageUrl = "/images/career-guide-aug.jpg",
                PdfUrl = "/api/magazines/2025/Aug/pdf"
            }
        };

        return Ok(magazines);
    }    /// <summary>
         /// Gets detailed information about a specific magazine by ID.
         /// </summary>
         /// <param name="id">The magazine ID</param>
         /// <returns>Detailed magazine information including articles</returns>
         /// <response code="200">Returns the magazine details</response>
         /// <response code="404">If the magazine is not found</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetMagazine(int id)
    {
        // Mock data - in real app, this would come from database
        if (id == 1)
        {
            var magazine = new
            {
                Id = 1,
                Title = "Tech Innovations 2025",
                IssueDate = "July 2025",
                Description = "Latest technology trends and innovations for college students",
                CoverImageUrl = "/images/tech-2025-jul.jpg",
                PdfUrl = "/api/magazines/2025/July/pdf",
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
    }    /// <summary>
         /// Gets information about the latest magazine issue.
         /// </summary>
         /// <returns>Latest magazine information with download links</returns>
         /// <response code="200">Returns the latest magazine details</response>
    [HttpGet("latest")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetLatestMagazine()
    {
        var latestMagazine = new
        {
            Id = 999,
            Title = "ShyvnTech Magazine - August 2025",
            IssueDate = "August 2025",
            Description = "🚀 Discover the future of entrepreneurship! This special startup edition features breakthrough innovations, successful founder stories, and essential business strategies for the next generation of tech entrepreneurs.",
            CoverImageUrl = "/images/shyvntech-aug-2025.jpg",
            PdfUrl = "/api/magazines/2025/Aug/pdf",
            Year = 2025,
            Month = "Aug",
            Category = "Startup"
        };

        return Ok(latestMagazine);
    }    // PDF Streaming Endpoints - Hierarchical Structure Only

    /// <summary>
    /// Downloads a PDF magazine for the specified year and month.
    /// </summary>
    /// <param name="year">The publication year (e.g., 2025)</param>
    /// <param name="month">The publication month (e.g., "July", "Aug")</param>
    /// <returns>PDF file download</returns>
    /// <response code="200">Returns the PDF file for download</response>
    /// <response code="404">If the PDF file is not found for the specified year/month</response>
    /// <response code="500">If an internal server error occurs</response>
    [HttpGet("{year}/{month}/pdf")]
    [Produces("application/pdf")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DownloadPdfByDate(int year, string month)
    {
        try
        {
            _logger.LogInformation("PDF download requested for {Year}/{Month}", year, month);

            // Build file path for date-based structure
            var filePath = Path.Combine(_environment.WebRootPath, "pdfs", year.ToString(), month, "Shyvntech_Magazine.pdf");

            if (!System.IO.File.Exists(filePath))
            {
                _logger.LogWarning("PDF file not found at path {FilePath}", filePath);
                return NotFound($"PDF file not available for {month} {year}");
            }

            // Read and serve file
            var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
            var fileName = $"Shyvntech_Magazine_{month}_{year}.pdf";

            _logger.LogInformation("Successfully served PDF download for {Year}/{Month}", year, month);

            return File(fileBytes, "application/pdf", fileName);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error serving PDF download for {Year}/{Month}", year, month);
            return StatusCode(500, "An error occurred while processing your request");
        }
    }    /// <summary>
         /// Views a PDF magazine inline in the browser for the specified year and month.
         /// </summary>
         /// <param name="year">The publication year (e.g., 2025)</param>
         /// <param name="month">The publication month (e.g., "July", "Aug")</param>
         /// <returns>PDF file for inline viewing</returns>
         /// <response code="200">Returns the PDF file for inline viewing</response>
         /// <response code="404">If the PDF file is not found for the specified year/month</response>
         /// <response code="500">If an internal server error occurs</response>
    [HttpGet("{year}/{month}/pdf/view")]
    [Produces("application/pdf")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult ViewPdfByDate(int year, string month)
    {
        try
        {
            _logger.LogInformation("PDF view requested for {Year}/{Month}", year, month);

            // Build file path for date-based structure
            var filePath = Path.Combine(_environment.WebRootPath, "pdfs", year.ToString(), month, "Shyvntech_Magazine.pdf");

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
    }    /// <summary>
         /// Checks if a PDF magazine exists for the specified year and month (HEAD request).
         /// </summary>
         /// <param name="year">The publication year (e.g., 2025)</param>
         /// <param name="month">The publication month (e.g., "July", "Aug")</param>
         /// <returns>HTTP status indicating PDF availability</returns>
         /// <response code="200">PDF file exists</response>
         /// <response code="404">PDF file not found</response>
         /// <response code="500">Internal server error</response>
    [HttpHead("{year}/{month}/pdf")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult CheckPdfExistsByDate(int year, string month)
    {
        try
        {            // Check if PDF file exists in date structure
            var filePath = Path.Combine(_environment.WebRootPath, "pdfs", year.ToString(), month, "Shyvntech_Magazine.pdf");
            var pdfExists = System.IO.File.Exists(filePath);

            return pdfExists ? Ok() : NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking PDF existence for {Year}/{Month}", year, month);
            return StatusCode(500);
        }
    }    /// <summary>
         /// Gets the complete archive of all available magazine issues organized by year and month.
         /// </summary>
         /// <returns>A list of all available magazine issues with download and view links</returns>
         /// <response code="200">Returns the complete magazine archive</response>
         /// <response code="500">If an internal server error occurs</response>
    [HttpGet("archive")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GetMagazineArchive()
    {
        try
        {
            var archive = new List<object>();
            var pdfsPath = Path.Combine(_environment.WebRootPath, "pdfs");

            // Check for year/month structure only
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
                        var pdfPath = Path.Combine(monthDir, "Shyvntech_Magazine.pdf");

                        if (System.IO.File.Exists(pdfPath))
                        {
                            var id = archive.Count + 1;
                            var issueDate = $"{month} {year}";
                            var category = GetCategoryByMonth(month);
                            
                            archive.Add(new
                            {
                                Id = id,
                                Title = $"ShyvnTech Magazine - {month} {year}",
                                IssueDate = issueDate,
                                Description = GetDescriptionByMonth(month, year),
                                CoverImageUrl = $"/images/shyvntech-{month.ToLower()}-{year}.jpg",
                                PdfUrl = $"/api/magazines/{year}/{month}/pdf",
                                Year = int.Parse(year),
                                Month = month,
                                Category = category
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

    /// <summary>
    /// Gets a category based on the month for variety in magazine content.
    /// </summary>
    private static string GetCategoryByMonth(string month)
    {
        return month.ToLowerInvariant() switch
        {
            "january" or "jan" => "AI",
            "february" or "feb" => "Blockchain",
            "march" or "mar" => "Cloud",
            "april" or "apr" => "Cybersecurity",
            "may" => "Startup",
            "june" or "jun" => "AI",
            "july" or "jul" => "Cloud",
            "august" or "aug" => "Startup",
            "september" or "sep" => "Blockchain",
            "october" or "oct" => "Cybersecurity",
            "november" or "nov" => "AI",
            "december" or "dec" => "Cloud",
            _ => "Technology"
        };
    }

    /// <summary>
    /// Gets a description based on the month and year for magazine content.
    /// </summary>
    private static string GetDescriptionByMonth(string month, string year)
    {
        var category = GetCategoryByMonth(month);
        return category switch
        {
            "AI" => $"Explore the latest in Artificial Intelligence, machine learning trends, and AI applications in {month} {year}. Perfect for students and professionals looking to stay ahead in the AI revolution.",
            "Blockchain" => $"Discover blockchain technology, cryptocurrency insights, and decentralized solutions in this comprehensive {month} {year} issue. Learn how blockchain is transforming industries.",
            "Cloud" => $"Master cloud computing essentials, AWS, Azure, and modern DevOps practices in our {month} {year} edition. Essential reading for cloud enthusiasts and IT professionals.",
            "Cybersecurity" => $"Stay protected with the latest cybersecurity trends, threat analysis, and security best practices in {month} {year}. Your guide to digital safety and security.",
            "Startup" => $"Entrepreneurship insights, startup stories, and business innovation strategies in our {month} {year} startup special. From idea to IPO - your startup journey guide.",
            _ => $"Comprehensive technology insights and innovations covering multiple domains in {month} {year}. Stay updated with the latest tech trends and industry developments."
        };
    }
}
