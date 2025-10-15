# SanVicenteHospital

SanVicenteHospital is a .NET 8 console application designed to manage a hospital’s medical appointments efficiently.  
The hospital currently manages appointments using physical agendas and spreadsheets, which has caused several problems:

- Duplicate appointments for the same doctor or patient.
- Difficulty finding patient information.
- Lack of control over doctors and their specialties.
- Loss of information when agendas are damaged or misplaced.

The management decided to develop an internal system in C# to efficiently organize **appointments, patients, and doctors**.

## Features

- Register, edit, delete, and search:
  - Patients
  - Doctors
  - Appointments
- Prevent duplicate documents, emails, or licenses
- Validate data (emails, age, dates, etc.)
- Send email confirmations for appointments
- Clear and interactive console menus
- Seed data for testing

## Technologies

- .NET 8
- C#
- Object-oriented programming
- Repository pattern for data access
- Service layer for business logic
- Console application with interactive menus
- `System.Net.Mail` for sending emails

## Requirements

- .NET 8.0 SDK or later installed
- Supported operating system for .NET (Windows, macOS, Linux)

## Installation and Running

### Clone the repository:

git clone https://github.com/THEMAIK17/SanVicenteHospital.git

### Navigate to the project folder:

cd SanVicenteHospital

### Restore dependencies and build the project:

dotnet restore
dotnet build

### Install the library for sending emails:

dotnet add package System.Net.Mail

Note: If using Gmail, generate an App Password and use it instead of your normal password.

## Run the application:

dotnet run

## Usage

Upon starting, you will see a main menu with options to manage:

Patients

Doctors

Appointments

Each section has its own submenu to register, list, search by document, update, and delete records.
Appointments trigger email confirmations sent to patients.

## Project Structure

```
SanVicenteHospital/
│
├── SanVicenteHospital.csproj  # Project file
├── Program.cs                 # Entry point (Main menu loop)
│
├── Core/
│   ├── AppInitializer.cs
│
├── Database/
│   ├── DatabaseContext.cs
│   ├── DataSeeder.cs
│
├── Models/                     # Domain entities (data models)
│   ├── Patient.cs
│   ├── Doctor.cs
│   └── Appointment.cs
│
├── Interfaces/                 # Interface definitions for repositories and services
│   ├── IPatientRepository.cs
│   ├── IDoctorRepository.cs
│   ├── IAppointmentRepository.cs
│   └── ... (additional service interfaces if needed)
│
├── Repositories/               # Data access layer (implements interfaces)
│   ├── PatientRepository.cs
│   ├── DoctorRepository.cs
│   ├── AppointmentRepository.cs
│
├── Services/                   
│   ├── PatientService.cs
│   ├── DoctorService.cs
│   └── AppointmentService.cs
│
├── Utils/                      # Console UI (menus and helpers)
│   ├── ShowMenuPatient.cs
│   ├── ShowMenuDoctor.cs
│   ├── ShowMenuAppointment.cs
```

## Layer Responsibilities

Core : Business logic, domain rules, and contracts.

Database : In-memory context and sample data seeding.

Models : Core entities and properties.

Interfaces : Contracts for repositories and services.

Repositories : Data access implementation using DatabaseContext.

Services : Application logic, validation, and operations.

Utils : Console menus, helpers like email sending.

Program.cs : Top-level application navigation.

## Future Improvements

Real database persistence (MySQL, PostgreSQL, or SQL Server).

Web or graphical interface for better UX.

Reports and statistics.

User and role management.



## License

This project is licensed under the MIT License. See the LICENSE file for details.

## Name

ARMANDO MAIKOL DURAN SALAS
 
## Clan
CAIMAN

## Email
maikold774@gmail.com

## Document Type

1002184305