using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SanVicenteHospital.Models;
using SanVicenteHospital.Repositories;
namespace SanVicenteHospital.Services;

public class DoctorService
{
    private readonly DoctorRepository _doctorRepository;

    public DoctorService(DoctorRepository doctorRepository)
    {
        _doctorRepository = doctorRepository;
    }

    public void RegisterDoctor()
    {
        try
        {
            Console.WriteLine("Enter Doctor Name:");
            string name = Console.ReadLine().Trim().ToLower();
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Name cannot be empty. Registration failed.");
                return;
            }

            Console.WriteLine("Enter Doctor Last Name:");
            string lastName = Console.ReadLine().Trim().ToLower();
            if (string.IsNullOrWhiteSpace(lastName))
            {
                Console.WriteLine("Last Name cannot be empty. Registration failed.");
                return;
            }

            Console.WriteLine("Enter Document Type:");
            string documentType = Console.ReadLine().Trim().ToLower();
            if (string.IsNullOrWhiteSpace(documentType))
            {
                Console.WriteLine("Document Type cannot be empty. Registration failed.");
                return;
            }

            Console.WriteLine("Enter Document Number:");
            string document = Console.ReadLine().Trim().ToLower();
            if (string.IsNullOrWhiteSpace(document))
            {
                Console.WriteLine("Document Number cannot be empty. Registration failed.");
                return;
            }

            if (_doctorRepository.ShowAllDoctors().Any(d => d.Document == document))
            {
                Console.WriteLine("Document Number already exists. Registration failed.");
                return;
            }

            Console.WriteLine("Enter Email:");
            string email = Console.ReadLine().Trim();
            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            {
                Console.WriteLine("Invalid email format. Registration failed.");
                return;
            }

            Console.WriteLine("Enter Age:");
            byte age = 0;
            try
            {
                age = byte.Parse(Console.ReadLine().Trim());
                if (age < 1 || age > 120)
                {
                    Console.WriteLine(" Age must be between 1 and 120.");
                    return;
                }
            }
            catch
            {
                Console.WriteLine(" Invalid age format. Enter numbers only.");
                return;
            }
            
            Console.WriteLine("Enter Phone Number:");
            string phone = Console.ReadLine().Trim();
            if (string.IsNullOrWhiteSpace(phone))
            {
                Console.WriteLine("Phone Number cannot be empty. Registration failed.");
                return;
            }

            Console.WriteLine("Enter Address:");
            string address = Console.ReadLine().Trim();
            if (string.IsNullOrWhiteSpace(address))
            {
                Console.WriteLine("Address cannot be empty. Registration failed.");
                return;
            }

            Console.WriteLine("Enter Birth Date (YYYY-MM-DD):");
            DateOnly birthDay;
            try
            {
                birthDay = DateOnly.Parse(Console.ReadLine().Trim());
                if (birthDay > DateOnly.FromDateTime(DateTime.Now))
                {
                    Console.WriteLine("Birth Date cannot be in the future. Registration failed.");
                    return;
                }
            }
            catch
            {
                Console.WriteLine("Invalid date format. Use YYYY-MM-DD.");
                return;
            }

            Console.WriteLine("Enter Specialty:");
            string specialty = Console.ReadLine().Trim().ToLower();
            if (string.IsNullOrWhiteSpace(specialty))
            {
                Console.WriteLine("Specialty cannot be empty. Registration failed.");
                return;
            }

            Doctor doctor = new Doctor(
                name,
                lastName,
                documentType,
                document,
                email,
                age,
                phone,
                address,
                birthDay,
                specialty
            );

            _doctorRepository.RegisterDoctor(doctor);
            Console.WriteLine("Doctor registered successfully.");
        }
        catch
        {
            Console.WriteLine("An error occurred during doctor registration. Please try again.");
        }
    }


    public void ShowAllDoctors()
    {
        var doctors = _doctorRepository.ShowAllDoctors();
        if (doctors.Count == 0)
        {
            Console.WriteLine("No doctors found.");
            return;
        }

        Console.WriteLine("\n Registered Doctors:\n");
        foreach (var doctor in doctors)
        {
            Console.WriteLine(doctor.ToString());
        }
    }

    
    public void FilterBySpecialty()
    {
        Console.WriteLine("Enter the specialty to filter by:");
        string specialty = Console.ReadLine().Trim().ToLower();

        var filtered = _doctorRepository.ShowAllDoctors()
            .Where(d => d.Specialty.ToLower() == specialty)
            .ToList();

        if (filtered.Count == 0)
        {
            Console.WriteLine($"No doctors found with specialty '{specialty}'.");
            return;
        }

        Console.WriteLine($"\nDoctors with specialty '{specialty}':\n");
        foreach (var doctor in filtered)
        {
            Console.WriteLine(doctor.ToString());
        }
    }


    public void GetDoctorByDocument()
    {
        Console.WriteLine("Enter the Document Number of the doctor to search:");
        string document = Console.ReadLine().Trim();

        if (string.IsNullOrWhiteSpace(document))
        {
            Console.WriteLine("Document cannot be empty.");
            return;
        }

        var doctor = _doctorRepository.GetDoctorByDocument(document);
        if (doctor != null)
        {
            Console.WriteLine("Doctor found:");
            Console.WriteLine(doctor.ToString());
        }
        else
        {
            Console.WriteLine("No doctor found with the provided document.");
        }
    }


    public void UpdateDoctor()
    {
        try
        {
            Console.WriteLine("Enter the document of the doctor to update:");
            string documentSearch = Console.ReadLine().Trim();

            var doctor = _doctorRepository.GetDoctorByDocument(documentSearch);
            if (doctor == null)
            {
                Console.WriteLine("No doctor found with the provided document.");
                return;
            }

            Console.WriteLine($"\nUpdating data for {doctor.Name} {doctor.LastName}:");

            Console.WriteLine("Enter new Name (or press Enter to keep current):");
            string name = Console.ReadLine().Trim().ToLower();
            if (!string.IsNullOrWhiteSpace(name)) doctor.Name = name;

            Console.WriteLine("Enter new Last Name (or press Enter to keep current):");
            string lastName = Console.ReadLine().Trim().ToLower();
            if (!string.IsNullOrWhiteSpace(lastName)) doctor.LastName = lastName;

            Console.WriteLine("Enter new Document Type (or press Enter to keep current):");
            string documentType = Console.ReadLine().Trim().ToLower();
            if (!string.IsNullOrWhiteSpace(documentType)) doctor.DocumentType = documentType;

            Console.WriteLine("Enter new Document Number (or press Enter to keep current):");
            string newDocument = Console.ReadLine().Trim().ToLower();
            if (!string.IsNullOrWhiteSpace(newDocument))
            {
                if (_doctorRepository.ShowAllDoctors().Any(d => d.Document == newDocument && d.Id != doctor.Id))
                {
                    Console.WriteLine("Document already exists. Update failed.");
                    return;
                }
                doctor.Document = newDocument;
            }

            Console.WriteLine("Enter new Email (or press Enter to keep current):");
            string email = Console.ReadLine().Trim();
            if (!string.IsNullOrWhiteSpace(email))
            {
                if (!email.Contains("@"))
                {
                    Console.WriteLine("Invalid email format. Update failed.");
                    return;
                }
                doctor.Email = email;
            }

            Console.WriteLine("Enter new Phone Number (or press Enter to keep current):");
            string phone = Console.ReadLine().Trim();
            if (!string.IsNullOrWhiteSpace(phone)) doctor.Phone = phone;

            Console.WriteLine("Enter new Address (or press Enter to keep current):");
            string address = Console.ReadLine().Trim().ToLower();
            if (!string.IsNullOrWhiteSpace(address)) doctor.Address = address;

            Console.WriteLine("Enter new Birth Date (YYYY-MM-DD) (or press Enter to keep current):");
            string birthInput = Console.ReadLine().Trim();
            if (!string.IsNullOrWhiteSpace(birthInput))
            {
                try
                {
                    doctor.BirthDay = DateOnly.Parse(birthInput);
                }
                catch
                {
                    Console.WriteLine("Invalid date format. Use YYYY-MM-DD.");
                    return;
                }
            }

            Console.WriteLine("Enter new Specialty (or press Enter to keep current):");
            string specialty = Console.ReadLine().Trim().ToLower();
            if (!string.IsNullOrWhiteSpace(specialty)) doctor.Specialty = specialty;

            _doctorRepository.UpdateDoctor(doctor);
            Console.WriteLine("Doctor updated successfully.");
        }
        catch
        {
            Console.WriteLine("An error occurred during doctor update. Please try again.");
        }
    }


    public void DeleteDoctor()
    {
        try
        {
            Console.WriteLine("Enter the document of the doctor to delete:");
            string document = Console.ReadLine().Trim();

            var doctor = _doctorRepository.GetDoctorByDocument(document);
            if (doctor == null)
            {
                Console.WriteLine("No doctor found with the provided document.");
                return;
            }

            _doctorRepository.DeleteDoctor(document);
            Console.WriteLine("Doctor deleted successfully.");
        }
        catch
        {
            Console.WriteLine("An error occurred during doctor deletion. Please try again.");
        }
    }
}
