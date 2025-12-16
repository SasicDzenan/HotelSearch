# Hotel Search API

## Overview
This project is a proof-of-concept (PoC) JSON REST API for hotel search, developed as part of a technical assignment.
The solution is built using **.NET 8 (LTS)** and demonstrates clean architecture, domain-driven design principles,
and a strong focus on testable business logic.

The application provides:
- CRUD operations for hotel management
- A search endpoint that ranks hotels by price and distance from a given location
- Paging support for search results


## Architecture Overview

The solution follows **Clean Architecture** principles and is organized into clear layers:

- **Domain**
  - Core business models (`Hotel`, `GeoLocation`)
  - Domain invariants and validation logic
- **Application**
  - Use cases and business logic (`SearchService`, `HotelService`)
  - Interfaces for infrastructure dependencies
  - Ranking logic encapsulated in a dedicated service
- **Infrastructure**
  - In-memory repository implementation
  - Easily replaceable with a persistent storage layer
- **API**
  - ASP.NET Core Web API controllers
  - Input validation and HTTP concerns
- **Tests**
  - Unit tests targeting application-layer business logic

This structure ensures low coupling, high cohesion, and easy extensibility.


## Search and Ranking Logic

The hotel search functionality applies a **multi-criteria ranking strategy**:

1. **Price (ascending)** – cheaper hotels are preferred
2. **Distance from user location (ascending)** – closer hotels are preferred when prices are equal

Distance is calculated using the **Haversine formula**.

Paging is applied **after ranking**, ensuring consistent and predictable results.


## API Endpoints

### Hotels (CRUD)
- `POST /api/hotels` – Create a hotel
- `GET /api/hotels` – Retrieve all hotels
- `GET /api/hotels/{id}` – Retrieve a hotel by ID
- `PUT /api/hotels/{id}` – Update a hotel
- `DELETE /api/hotels/{id}` – Delete a hotel

### Search
- `GET /api/search`
  - Query parameters:
    - `latitude`
    - `longitude`
    - `page`
    - `pageSize`

### Health Check
- `GET /health`


## Testing Strategy

Unit tests focus on **core business rules** at the application layer:

- Correct hotel ranking by price and distance
- Paging behavior applied after ranking

Infrastructure dependencies are mocked to keep tests isolated and deterministic.

Invalid input validation is handled at the API boundary (controllers) and therefore is not tested at the service level.

Tests are executed using:

```bash
dotnet test
```


## Performance Considerations

- Distance calculation: **O(n)**
- Sorting: **O(n log n)**
- Paging: **O(1)** after sorting

This approach is suitable for an in-memory PoC.
In a production environment, distance calculations and sorting would likely be delegated to the persistence layer
with proper indexing and query optimization.


## Security Considerations

- Input validation via DataAnnotations
- Domain-level defensive programming
- HTTPS redirection enabled

Authentication and authorization are intentionally omitted due to the scope of the assignment,
but the API pipeline is prepared for adding them.


## AI Assistance

AI tools such as **ChatGPT** and **GitHub Copilot** were used to:
- Accelerate boilerplate generation
- Validate architectural decisions
- Improve code clarity and consistency

All generated code was reviewed, adapted, and tested manually.


## How to Run

### Prerequisites
- .NET 8 SDK

### Run the application
```bash
dotnet restore
dotnet build
dotnet run --project HotelSearch/HotelSearch.csproj
```

### Run tests
```bash
dotnet test
```

Swagger UI is available in the development environment.


## Future Improvements

- Persistent storage (e.g. SQL database with EF Core)
- Authentication and authorization
- Centralized exception handling
- Structured logging
- CI/CD pipeline


## Summary

This solution prioritizes correctness, clean design, and clarity of intent.
It demonstrates how core business logic can be isolated, tested, and extended,
while keeping the overall implementation aligned with real-world production practices.
