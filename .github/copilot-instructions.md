# GitHub Copilot Custom Instructions

## Project Overview
This repository contains examples of various architectural styles and patterns implemented in .NET Core, including:
- **Hexagonal Architecture (Ports & Adapters)**
- **CQRS (Command Query Responsibility Segregation)**
- **Event-Driven Architecture**
- **Asynchronous Command Queue**
- **Vertical Slice Architecture**

## Code Style and Conventions

### General Guidelines
- Follow C# coding conventions and .NET best practices
- Use meaningful variable and method names that clearly express intent
- Keep methods focused and single-purpose (SOLID principles)

### Architecture-Specific Guidelines

#### Hexagonal Architecture (Ports & Adapters)
- **Ports** define interfaces for application boundaries
- **Adapters** implement ports for specific technologies
- Domain logic must remain independent of infrastructure concerns
- Dependencies should point inward toward the domain

#### CQRS Pattern
- Separate read models from write models
- Commands should not return data (except confirmation/ID)
- Queries should not modify state
- Use appropriate naming: `*Command`, `*Query`, `*CommandHandler`, `*QueryHandler`

#### Event-Driven Architecture
- Events should be immutable and named in past tense (e.g., `AlbumCreated`)
- Event handlers should be idempotent when possible
- Domain events belong in `*.Domain.Events` namespace
- Event handlers belong in `*.Application.EventHandlers` namespace

#### Asynchronous Command Queue
- Commands are queued and processed asynchronously
- Use background processors for command execution
- Implement proper error handling and retry logic
- Consider eventual consistency implications

#### Vertical Slice Architecture
- Each feature is self-contained within its own vertical slice
- Minimize sharing code between slices
- Co-locate related functionality (controllers, services, models)
- Prefer duplication over wrong abstractions

## Project Structure

### Naming Conventions
- **API Projects**: `*.Api`
- **Domain Projects**: `*.Domain`
- **Application Projects**: `*.Application.*`
- **Infrastructure Projects**: `*.Infrastructure.*`
- **Test Projects**: `*.Tests`

### File Organization
- Controllers in `/Controllers`
- Domain entities in `/Domain` or root of domain project
- Application services in `/Services`
- Queries in `/Queries`
- Commands in `/Commands`
- Event handlers in `/EventHandlers`

## Testing Guidelines
- Use NUnit for unit and integration tests
- Follow Given-When-Then pattern in tests without comments
- Name tests clearly: `MethodName_Scenario_ExpectedBehavior`
- Test files should mirror the structure of source files
- Mock external dependencies appropriately

## Dependencies and Libraries
- Target .NET Core framework
- Use dependency injection via built-in IoC container
- Prefer async/await for I/O operations
- Use appropriate NuGet packages for each architectural style

## API Design
- Follow REST principles for HTTP APIs
- Use appropriate HTTP verbs and status codes
- Implement proper error handling and validation
- Return consistent response structures
- Use `appsettings.json` for configuration

## When Generating Code
1. Respect the existing architectural pattern of the project being modified
2. Maintain separation of concerns appropriate to the architecture
3. Follow existing namespace and folder structures
4. Include appropriate error handling and logging
5. Consider testability when designing new features
6. Use async/await patterns for I/O operations
7. Implement proper dependency injection
8. Avoid comments ussage; write self-explanatory code instead

## What to Avoid
- Don't mix architectural patterns within a single project
- Don't add business logic to controllers or adapters
- Don't create circular dependencies
- Don't use static classes for state management
- Don't bypass the established architectural boundaries
- Don't ignore existing error handling patterns

## Additional Context
- Each architecture example is a complete, standalone solution
- Solutions demonstrate the same domain (MyMusic) using different patterns
- Changes should be made within the appropriate architectural context
- Consider the trade-offs of each architectural style when suggesting improvements
