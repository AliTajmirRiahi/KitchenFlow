# KitchenFlow
KitchenFlow is a clean‑architecture–based .NET 10 application designed to manage restaurant operations with a scalable, maintainable, and testable structure.

The project follows Domain‑Driven Design (DDD), CQRS principles, and a layered architecture separating Domain, Application, Infrastructure, and Presentation concerns.

The solution includes dedicated layers for contracts, services, and integration/unit testing to ensure high code quality and long‑term maintainability.

Key Features

    Clean Architecture with strict project boundaries
    Domain‑Driven Design (Entities, Value Objects, Domain Services, Contracts)
    Application layer with CQRS‑style request handling
    Infrastructure layer for database access and external services
    Presentation layer for API/UI endpoints
    Unit and Integration test projects
    Organized folder structure for scalability

Project Structure

    src/Framework
        Base – Shared base classes and building blocks
        Domain – Core domain logic
        Application – Application services, DTOs, use cases
    src/Service
        Feature‑based services including Domain, Application, Infrastructure, and Presentation projects
    tests
        Unit and Integration test suites

This project is currently under active development and will evolve as features are added.
