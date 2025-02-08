# Patient-API
---

# PatientApi - Blazor Application  

## Project Description  
The PatientApi project is a Blazor-based web application designed to manage patient records. It allows users to:  

✅ Create, read, update, and delete patient information.  
✅ Manage medical records associated with each patient.  
✅ Use Entity Framework Core for seamless data access.  
✅ Ensure code reliability through xUnit unit tests.  

The project is built with modern web development practices and provides a scalable and maintainable solution for healthcare data management.  

---

## Technology Stack  
- .NET 9 (Blazor WebAssembly & Blazor Server)  
- Entity Framework Core (for database interactions)  
- SQLite / SQL Server (switchable database backends)  
- xUnit (for unit testing)  
- Dependency Injection (for better code maintainability)  
- Docker (optional for containerization)  

---

## Thought Process Behind Implementation  

### 1. Architecture  
The project follows a layered architecture:  

🔹 Data Layer: Handles database interactions via PatientApiDbContext using Entity Framework Core.  
🔹 Model Layer: Defines core data models like Patient and PatientRecord.  
🔹 Service Layer: Implements business logic via PatientRecordService.  
🔹 Presentation Layer: Uses Blazor components to render UI and manage user interactions.  

---

## Key Design Decisions  

### 1. Use of Entity Framework Core  
✔ Why? Simplifies database operations and migrations.  
✔ How? The `PatientApiDbContext` class is configured for:  
   - In-memory database (for testing).  
   - SQL Server or SQLite (for production).  

### 2. In-Memory Database for Testing  
✔ Why? Enables fast, isolated unit tests without needing a real database.  
✔ How? The `PatientRecordServiceTests` class uses DbContextOptions to configure an in-memory database for testing.  

### 3. Blazor for UI  
✔ Why? Provides a modern, component-based UI framework using C#.  
✔ How? The project includes Razor components and Blazor pages to display and manage patient records interactively.  

### 4. Separation of Concerns (SoC)  
✔ Why? Improves maintainability and testability.  
✔ How? The project is structured into separate layers:  
   - Models for data structures.  
   - Services for business logic.  
   - DbContext for database interactions.  

### 5. Unit Testing with xUnit  
✔ Why? Ensures reliability of core functionalities.  
✔ How? The `PatientRecordServiceTests` class includes tests for:  
   - Creating, updating, and retrieving patient records.  

---

## Setup & Installation  

### 1. Clone the Repository  
```sh
git clone https://github.com/yourusername/PatientApi.git
cd PatientApi
```

### 2. Build and Run the Application  
#### Without Docker  
1. Install Dependencies:  
   ```sh
   dotnet restore
   ```
2. Run the Application:  
   ```sh
   dotnet run
   ```
3. Open the application in the browser:  
   ```
   http://localhost:5000
   ```

#### With Docker  
1. Build the Docker Image:  
   ```sh
   docker build -t patientapi .
   ```
2. Run the Container:  
   ```sh
   docker run -p 8080:8080 patientapi
   ```
3. Access the API at:  
   ```
   http://localhost:8080
   ```

---

## API Endpoints  
| Method | Endpoint               | Description                          |
|--------|------------------------|--------------------------------------|
| GET    | `/api/patients`        | Get all patients                    |
| GET    | `/api/patients/{id}`   | Get a single patient by ID          |
| POST   | `/api/patients`        | Create a new patient                |
| PUT    | `/api/patients/{id}`   | Update patient details              |
| DELETE | `/api/patients/{id}`   | Soft-delete a patient               |
| GET    | `/api/records/{id}`    | Get a patient’s medical record      |
| POST   | `/api/records`         | Create a new patient record         |

---

## Docker Configuration  
Here is the Dockerfile used for running the project in a containerized environment:

```dockerfile
# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
# For more information, please see https://aka.ms/containercompat

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0-nanoserver-1809 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0-nanoserver-1809 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Patient-API.csproj", "."]
RUN dotnet restore "./Patient-API.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./Patient-API.csproj" -c %BUILD_CONFIGURATION% -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Patient-API.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Patient-API.dll"]
```

---

## Future Enhancements  
🔹 Implement JWT authentication for secure access.  
🔹 Add role-based access control (RBAC).  
🔹 Enhance the user interface with more Blazor components.  
🔹 Support file uploads for patient medical reports.  
🔹 Migrate to PostgreSQL for better scalability.  

---

## License  
This project is open-source and free to use. Contributions are welcome!  

---
