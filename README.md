# MyShop

This is a .NET 8 Web API project named MyShop, developed following REST architecture principles.

## API Documentation

The API is documented using Swagger. You can access the Swagger documentation at the following link:  [`(https://localhost:7020/swagger/index.html)`]

## Project Structure

The project is divided into several layers to ensure a clean architecture:

1. **Entities**: Contains the domain models.
2. **DTO (Data Transfer Objects)**: Contains the data transfer objects used for communication between layers.
3. **Repositories**: Contains the repository classes responsible for data access.
4. **Services**: Contains the business logic of the application.
5. **Controllers**: Contains the API controllers that handle HTTP requests.

### Why Layered Architecture?

We use a layered architecture to separate concerns, making the codebase more maintainable and scalable. Each layer has a specific responsibility, and they communicate with each other through well-defined interfaces.

### AutoMapper

We use AutoMapper for mapping between domain models and DTOs. This helps in reducing boilerplate code and ensures a clean separation between layers.

### Dependency Injection (DI)

We use Dependency Injection to manage dependencies between layers. This makes the code more testable and modular.

### Scalability with Async/Await

To ensure scalability, we use async/await for asynchronous programming. This helps in handling more concurrent requests efficiently.

### Database

We use SQL as the database, following the DB First approach. You can create the database using the following commands:

```sh
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### Configuration Files

Configuration settings are managed using configuration files. This allows us to manage different settings for different environments easily.

### Error Handling

All errors are handled using middleware. Errors are logged using a logger, and critical errors are sent via email. This ensures that the application can handle unexpected issues gracefully.

### Request Logging

All requests to the system are logged for rating and analysis purposes. This helps in monitoring and improving the system.

### Clean Code

We follow clean code principles to ensure that the codebase is readable, maintainable, and scalable.

## Contact

For any questions or issues, please contact Yael Rosenberg:
- **GitHub**: [Yael380](https://github.com/Yael380/MyShop)
- **Phone**: 054-854-8380
- **Email**: yael054854@gmail.com



Happy Coding!
