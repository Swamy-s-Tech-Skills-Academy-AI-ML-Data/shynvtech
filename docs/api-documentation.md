# API Documentation

This document provides comprehensive documentation for all ShynvTech platform APIs.

## Base URLs

| Service | Local Development | Production |
|---------|------------------|------------|
| Magazine API | `https://localhost:7xxx` | TBD |
| Events API | `https://localhost:7xxx` | TBD |
| LMS API | `https://localhost:7xxx` | TBD |
| Content API | `https://localhost:7xxx` | TBD |
| General API | `https://localhost:7xxx` | TBD |

*Note: Actual ports are assigned dynamically by .NET Aspire. Check the Aspire dashboard for current URLs.*

## Authentication

All APIs will support the following authentication methods (implementation pending):

- **JWT Bearer Tokens** - For API access
- **Cookie Authentication** - For web application
- **API Keys** - For external integrations

### Authentication Headers

```http
Authorization: Bearer <jwt_token>
X-API-Key: <api_key>
```

## Common Response Formats

### Success Response

```json
{
  "success": true,
  "data": { ... },
  "message": "Operation completed successfully"
}
```

### Error Response

```json
{
  "success": false,
  "error": {
    "code": "ERROR_CODE",
    "message": "Human readable error message",
    "details": "Additional error details"
  }
}
```

### Pagination Response

```json
{
  "success": true,
  "data": {
    "items": [...],
    "pagination": {
      "page": 1,
      "pageSize": 20,
      "totalItems": 100,
      "totalPages": 5,
      "hasNextPage": true,
      "hasPreviousPage": false
    }
  }
}
```

## Magazine API

### Base URL: `/api/magazines`

The Magazine API manages monthly college magazines, articles, and publications.

#### Get All Magazines

```http
GET /api/magazines
```

**Query Parameters:**
- `page` (integer, optional): Page number (default: 1)
- `pageSize` (integer, optional): Items per page (default: 20, max: 100)
- `search` (string, optional): Search term for magazine title or description
- `category` (string, optional): Filter by category
- `status` (string, optional): Filter by status (draft, published, archived)

**Response:**
```json
{
  "success": true,
  "data": {
    "items": [
      {
        "id": 1,
        "title": "Tech Innovations 2025",
        "description": "Latest technology trends for students",
        "category": "Technology",
        "status": "published",
        "publishDate": "2025-01-15T00:00:00Z",
        "coverImageUrl": "https://example.com/cover.jpg",
        "downloadUrl": "https://example.com/magazine.pdf",
        "articleCount": 15,
        "downloadCount": 1205,
        "createdAt": "2025-01-01T10:00:00Z",
        "updatedAt": "2025-01-15T14:30:00Z"
      }
    ],
    "pagination": { ... }
  }
}
```

#### Get Magazine by ID

```http
GET /api/magazines/{id}
```

**Path Parameters:**
- `id` (integer, required): Magazine ID

**Response:**
```json
{
  "success": true,
  "data": {
    "id": 1,
    "title": "Tech Innovations 2025",
    "description": "Latest technology trends for students",
    "content": "Full magazine content in HTML format...",
    "category": "Technology",
    "status": "published",
    "publishDate": "2025-01-15T00:00:00Z",
    "coverImageUrl": "https://example.com/cover.jpg",
    "downloadUrl": "https://example.com/magazine.pdf",
    "tags": ["technology", "innovation", "students"],
    "articles": [
      {
        "id": 1,
        "title": "AI in Education",
        "author": "John Doe",
        "excerpt": "How AI is transforming education...",
        "readingTime": 5
      }
    ],
    "downloadCount": 1205,
    "createdAt": "2025-01-01T10:00:00Z",
    "updatedAt": "2025-01-15T14:30:00Z"
  }
}
```

#### Create Magazine

```http
POST /api/magazines
```

**Request Body:**
```json
{
  "title": "New Magazine Title",
  "description": "Magazine description",
  "content": "Full magazine content",
  "category": "Technology",
  "tags": ["tech", "innovation"],
  "publishDate": "2025-02-01T00:00:00Z",
  "coverImage": "base64_encoded_image_data"
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "id": 2,
    "title": "New Magazine Title",
    // ... other fields
  },
  "message": "Magazine created successfully"
}
```

#### Update Magazine

```http
PUT /api/magazines/{id}
```

**Path Parameters:**
- `id` (integer, required): Magazine ID

**Request Body:** Same as Create Magazine

#### Delete Magazine

```http
DELETE /api/magazines/{id}
```

**Path Parameters:**
- `id` (integer, required): Magazine ID

**Response:**
```json
{
  "success": true,
  "message": "Magazine deleted successfully"
}
```

#### Download Magazine

```http
GET /api/magazines/{id}/download
```

**Response:** PDF file download

## Events API

### Base URL: `/api/events`

The Events API manages college events, registrations, and event planning.

#### Get All Events

```http
GET /api/events
```

**Query Parameters:**
- `page` (integer, optional): Page number
- `pageSize` (integer, optional): Items per page
- `category` (string, optional): Event category
- `status` (string, optional): Event status (upcoming, ongoing, completed, cancelled)
- `startDate` (string, optional): Filter events from date (ISO 8601)
- `endDate` (string, optional): Filter events to date (ISO 8601)
- `search` (string, optional): Search term

**Response:**
```json
{
  "success": true,
  "data": {
    "items": [
      {
        "id": 1,
        "title": "Tech Symposium 2025",
        "description": "Annual technology symposium",
        "category": "Academic",
        "status": "upcoming",
        "startDateTime": "2025-03-15T09:00:00Z",
        "endDateTime": "2025-03-15T17:00:00Z",
        "location": "Main Auditorium",
        "maxAttendees": 500,
        "registeredCount": 245,
        "registrationDeadline": "2025-03-10T23:59:59Z",
        "isRegistrationOpen": true,
        "imageUrl": "https://example.com/event.jpg",
        "organizer": "Tech Club",
        "createdAt": "2025-01-15T10:00:00Z"
      }
    ],
    "pagination": { ... }
  }
}
```

#### Get Event by ID

```http
GET /api/events/{id}
```

#### Create Event

```http
POST /api/events
```

**Request Body:**
```json
{
  "title": "New Event",
  "description": "Event description",
  "category": "Academic",
  "startDateTime": "2025-04-01T10:00:00Z",
  "endDateTime": "2025-04-01T15:00:00Z",
  "location": "Conference Hall",
  "maxAttendees": 200,
  "registrationDeadline": "2025-03-28T23:59:59Z",
  "organizer": "Student Council",
  "agenda": [
    {
      "time": "10:00",
      "title": "Opening Ceremony",
      "speaker": "Dean Smith"
    }
  ]
}
```

#### Register for Event

```http
POST /api/events/{id}/register
```

**Request Body:**
```json
{
  "attendeeName": "John Doe",
  "attendeeEmail": "john@example.com",
  "attendeePhone": "+1234567890",
  "department": "Computer Science",
  "year": "3rd Year"
}
```

#### Get Event Registrations

```http
GET /api/events/{id}/registrations
```

## LMS API

### Base URL: `/api/lms`

The Learning Management System API handles courses, assignments, and student progress.

#### Get All Courses

```http
GET /api/lms/courses
```

**Response:**
```json
{
  "success": true,
  "data": {
    "items": [
      {
        "id": 1,
        "title": "Introduction to Programming",
        "description": "Learn programming fundamentals",
        "instructor": "Prof. Jane Smith",
        "department": "Computer Science",
        "credits": 3,
        "duration": "16 weeks",
        "level": "Beginner",
        "enrolledStudents": 45,
        "maxStudents": 50,
        "startDate": "2025-02-01T00:00:00Z",
        "endDate": "2025-05-15T00:00:00Z",
        "status": "active",
        "imageUrl": "https://example.com/course.jpg",
        "modules": [
          {
            "id": 1,
            "title": "Variables and Data Types",
            "order": 1,
            "duration": "2 weeks"
          }
        ]
      }
    ],
    "pagination": { ... }
  }
}
```

#### Enroll in Course

```http
POST /api/lms/courses/{id}/enroll
```

#### Get Course Materials

```http
GET /api/lms/courses/{id}/materials
```

#### Submit Assignment

```http
POST /api/lms/assignments/{id}/submit
```

## Content API

### Base URL: `/api/content`

The Content API manages static content like About Us, Contact Us, and other informational pages.

#### Get Page Content

```http
GET /api/content/pages/{slug}
```

**Path Parameters:**
- `slug` (string, required): Page slug (e.g., "about-us", "contact-us")

**Response:**
```json
{
  "success": true,
  "data": {
    "id": 1,
    "slug": "about-us",
    "title": "About ShynvTech",
    "content": "<h1>About Us</h1><p>Content in HTML format...</p>",
    "metaTitle": "About ShynvTech - Student Platform",
    "metaDescription": "Learn about ShynvTech platform...",
    "lastUpdated": "2025-01-15T10:00:00Z",
    "author": "Admin"
  }
}
```

#### Update Page Content

```http
PUT /api/content/pages/{slug}
```

#### Get Contact Information

```http
GET /api/content/contact
```

**Response:**
```json
{
  "success": true,
  "data": {
    "email": "contact@shynvtech.com",
    "phone": "+1-555-0123",
    "address": {
      "street": "123 College Street",
      "city": "Education City",
      "state": "State",
      "zipCode": "12345",
      "country": "Country"
    },
    "socialMedia": {
      "facebook": "https://facebook.com/shynvtech",
      "twitter": "https://twitter.com/shynvtech",
      "instagram": "https://instagram.com/shynvtech",
      "linkedin": "https://linkedin.com/company/shynvtech"
    },
    "officeHours": "Monday - Friday: 9:00 AM - 5:00 PM"
  }
}
```

#### Submit Contact Form

```http
POST /api/content/contact/submit
```

**Request Body:**
```json
{
  "name": "John Doe",
  "email": "john@example.com",
  "subject": "Inquiry about services",
  "message": "I would like to know more about...",
  "department": "General Inquiry"
}
```

## General API Service

### Base URL: `/api/general`

General utility endpoints and shared functionality.

#### Health Check

```http
GET /api/health
```

**Response:**
```json
{
  "status": "healthy",
  "timestamp": "2025-01-15T14:30:00Z",
  "version": "1.0.0",
  "services": {
    "database": "healthy",
    "cache": "healthy",
    "storage": "healthy"
  }
}
```

#### Get System Statistics

```http
GET /api/general/stats
```

**Response:**
```json
{
  "success": true,
  "data": {
    "totalUsers": 1250,
    "totalMagazines": 24,
    "totalEvents": 45,
    "totalCourses": 18,
    "monthlyDownloads": 3450,
    "activeUsers": 890
  }
}
```

#### Search Across Platform

```http
GET /api/general/search
```

**Query Parameters:**
- `q` (string, required): Search query
- `type` (string, optional): Search type (all, magazines, events, courses)
- `page` (integer, optional): Page number
- `pageSize` (integer, optional): Items per page

**Response:**
```json
{
  "success": true,
  "data": {
    "query": "technology",
    "results": {
      "magazines": [
        {
          "id": 1,
          "title": "Tech Innovations 2025",
          "type": "magazine",
          "excerpt": "Latest technology trends...",
          "url": "/magazines/1"
        }
      ],
      "events": [...],
      "courses": [...]
    },
    "totalResults": 15
  }
}
```

## Error Codes

| Code | Description |
|------|-------------|
| `VALIDATION_ERROR` | Request data validation failed |
| `NOT_FOUND` | Requested resource not found |
| `UNAUTHORIZED` | Authentication required |
| `FORBIDDEN` | Insufficient permissions |
| `DUPLICATE_ENTRY` | Resource already exists |
| `SERVER_ERROR` | Internal server error |
| `SERVICE_UNAVAILABLE` | Service temporarily unavailable |
| `RATE_LIMIT_EXCEEDED` | Too many requests |

## Rate Limiting

All APIs implement rate limiting:

- **Anonymous users**: 100 requests per hour
- **Authenticated users**: 1000 requests per hour
- **API key users**: 5000 requests per hour

Rate limit headers:
```http
X-RateLimit-Limit: 1000
X-RateLimit-Remaining: 999
X-RateLimit-Reset: 1640995200
```

## Webhooks (Future)

The platform will support webhooks for real-time notifications:

### Available Events
- `magazine.published`
- `event.created`
- `event.registration.new`
- `course.enrollment.new`
- `assignment.submitted`

### Webhook Format
```json
{
  "event": "magazine.published",
  "timestamp": "2025-01-15T14:30:00Z",
  "data": {
    "magazineId": 1,
    "title": "Tech Innovations 2025"
  }
}
```

## SDKs and Client Libraries

Planned client libraries:

- **JavaScript/TypeScript SDK** - For web applications
- **C# SDK** - For .NET applications
- **Python SDK** - For data analysis and automation
- **Mobile SDK** - For Flutter/React Native apps

## Testing the APIs

### Using curl

```bash
# Get all magazines
curl -X GET "https://localhost:7xxx/api/magazines" \
  -H "Accept: application/json"

# Create a new event
curl -X POST "https://localhost:7xxx/api/events" \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer <token>" \
  -d '{
    "title": "Test Event",
    "description": "Test event description",
    "startDateTime": "2025-04-01T10:00:00Z",
    "endDateTime": "2025-04-01T15:00:00Z"
  }'
```

### Using Postman

1. Import the OpenAPI/Swagger definitions from each service
2. Set up environment variables for base URLs
3. Configure authentication tokens
4. Create test collections for each API

### Using HTTP files

Each API service includes `.http` files for testing:

- `ShynvTech.Magazine.Api.http`
- `ShynvTech.Events.Api.http`
- `ShynvTech.Lms.Api.http`
- `ShynvTech.Content.Api.http`

## OpenAPI/Swagger Documentation

Each service exposes interactive API documentation at:
- `/swagger` - Swagger UI
- `/swagger/v1/swagger.json` - OpenAPI specification

Access these endpoints when the services are running to explore the APIs interactively.

---

*This documentation is automatically updated as the APIs evolve. For the most current information, refer to the Swagger documentation of each running service.*
