using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using SanVicenteHospital.Models;
using SanVicenteHospital.Repositories;

namespace SanVicenteHospital.Services;

public class PatientService
{
    private readonly PatientRepository _PatientRepository;

    public PatientService(PatientRepository patientRepository)
    {
        _PatientRepository = patientRepository;
    }

    public void RegisterPatient()
    {
        try
        {

            Console.WriteLine("Enter Patient Name:");
            string name = Console.ReadLine().Trim().ToLower();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Name cannot be empty. Registration failed.");
                return;
            }

            Console.WriteLine("Enter Patient Last Name:");
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

            if (_PatientRepository.ShowAllPatients().Any(p => p.Document == document))
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

            Console.WriteLine("Enter Address:");
            string address = Console.ReadLine().Trim();
            if (string.IsNullOrWhiteSpace(address))
            {
                Console.WriteLine("Address cannot be empty. Registration failed.");
                return;
            }

            Console.WriteLine("Enter Phone Number:");
            string phone = Console.ReadLine().Trim();
            if (string.IsNullOrWhiteSpace(phone))
            {
                Console.WriteLine("Phone Number cannot be empty. Registration failed.");
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

            Patient patient = new Patient(name, lastName, documentType, document, email, age, address, phone, birthDay);
            _PatientRepository.RegisterPatient(patient);
            Console.WriteLine("Patient registered successfully.");
        }
        catch
        {
            Console.WriteLine("An error occurred during patient registration. Please try again.");
            return;
        }

    }

    public void ShowAllPatients()
    {
        var patients = _PatientRepository.ShowAllPatients();
        if (patients.Count == 0)
        {
            Console.WriteLine("No patients found.");
            return;
        }

        Console.WriteLine("\n Registered Patients:\n");

        foreach (var patient in patients)
        {
            Console.WriteLine(patient.ToString());
        }
    }

    public void GetPatientByDocument()
    {
        try
        {

            Console.WriteLine("Enter the Document Number of the patient to search:");
            string document = Console.ReadLine().Trim();

            if (string.IsNullOrWhiteSpace(document))
            {
                Console.WriteLine("Document cannot be empty.");
                return;
            }

            var patient = _PatientRepository.GetPatientByDocument(document);

            if (patient != null)
            {
                Console.WriteLine("Patient found:");
                Console.WriteLine(patient.ToString());
            }
            else
            {
                Console.WriteLine("No patient found with the provided Document Number.");
            }
        }
        catch
        {
            Console.WriteLine("An error occurred while searching for the patient.");
        }
    }

    public void UpdatePatient()
    {
        try
        {
            Console.WriteLine("Enter the document of the patient to update:");

            string documentSearch = Console.ReadLine().Trim();

            if (string.IsNullOrWhiteSpace(documentSearch))
            {
                Console.WriteLine("Document cannot be empty.");
                return;
            }

            var patient = _PatientRepository.GetPatientByDocument(documentSearch);
            if (patient == null)
            {
                Console.WriteLine("No patient found with the provided Id.");
                return;
            }

            Console.WriteLine($"\nUpdating data for {patient.Name} {patient.LastName}:");

            Console.WriteLine("Enter new Name (or press Enter to keep current):");
            string name = Console.ReadLine().Trim().ToLower();
            if (!string.IsNullOrWhiteSpace(name))
            {
                patient.Name = name.ToLower();
            }

            Console.WriteLine("Enter new Last Name (or press Enter to keep current):");
            string lastName = Console.ReadLine().Trim();
            if (!string.IsNullOrWhiteSpace(lastName))
            {
                patient.LastName = lastName.ToLower();
            }

            Console.WriteLine("Enter new Document Type (or press Enter to keep current):");
            string documentType = Console.ReadLine().Trim().ToLower();
            if (!string.IsNullOrWhiteSpace(documentType))
            {
                patient.DocumentType = documentType.ToLower();
            }
            Console.WriteLine("Enter new Document Number (or press Enter to keep current):");
            string document = Console.ReadLine().Trim().ToLower();
            if (!string.IsNullOrWhiteSpace(document))
            {
                if (_PatientRepository.ShowAllPatients().Any(p => p.Document == document && p.Id != patient.Id))
                {
                    Console.WriteLine("Document Number already exists. Update failed.");
                    return;
                }
                patient.Document = document.ToLower();
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
                patient.Email = email;
            }

            Console.WriteLine("Enter new Age (or press Enter to keep current):");
            string ageInput = Console.ReadLine().Trim();
            if (!string.IsNullOrWhiteSpace(ageInput))
            {
                try
                {
                    patient.Age = byte.Parse(ageInput);

                }
                catch
                {
                    Console.WriteLine(" Invalid age format. Enter numbers only.");
                    return;
                }
            }
            Console.WriteLine("Enter new Address (or press Enter to keep current):");
            string address = Console.ReadLine().Trim().ToLower();
            if (!string.IsNullOrWhiteSpace(address))
            {
                patient.Address = address;
            }
            Console.WriteLine("Enter new Phone Number (or press Enter to keep current):");
            string phone = Console.ReadLine().Trim();
            if (!string.IsNullOrWhiteSpace(phone))
            {
                patient.Phone = phone;
            }
            Console.WriteLine("Enter new Birth Date (YYYY-MM-DD) (or press Enter to keep current):");
            string birthDateInput = Console.ReadLine().Trim();
            if (!string.IsNullOrWhiteSpace(birthDateInput))
            {
                try
                {
                    patient.BirthDay = DateOnly.Parse(birthDateInput);
                }
                catch
                {
                    Console.WriteLine("Invalid date format. Use YYYY-MM-DD.");
                    return;
                }
            }

            _PatientRepository.UpdatePatient(patient);
            Console.WriteLine("Patient updated successfully.");

        }
        catch
        {

            Console.WriteLine("An error occurred during patient update. Please try again.");
            return;
        }
    }

    public void DeletePatient()
    {
        try
        {
            Console.WriteLine("Enter the Id of the patient to delete:");
            string documentSearch = Console.ReadLine().Trim();

            if (string.IsNullOrWhiteSpace(documentSearch))
            {
                Console.WriteLine("Id cannot be empty.");
                return;
            }

            var patient = _PatientRepository.GetPatientByDocument(documentSearch);
            if (patient != null)
            {
                _PatientRepository.DeletePatient(documentSearch);
                Console.WriteLine("Patient deleted successfully.");
            }
            else
            {
                Console.WriteLine("No patient found with the provided Id.");
            }

        }
        catch
        {
            Console.WriteLine("An error occurred during patient deletion. Please try again.");
            return;
        }
    }



}
