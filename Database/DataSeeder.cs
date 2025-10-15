using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SanVicenteHospital.Models;
using SanVicenteHospital.Repositories;


namespace SanVicenteHospital.Database
{
    public class DataSeeder
    {
        public static void SeedAllData(
            PatientRepository patientRepository,
            DoctorRepository _doctorRepository,
            AppointmentRepository appointmentRepository
        )
        {
            // Create and register a sample customer

            Patient patient1 = new Patient("Joh", "Doe", "Passport", "A12345678", "maikol@gmail.com", 30, "Main St,555-1234", "165462343", new DateOnly(1993, 1, 1));
            patientRepository.RegisterPatient(patient1);

            Console.WriteLine(patient1.ToString());

            Doctor doctor1 = new Doctor(
            "John",
            "Smith",
            "CC",
            "10203040",
            "johnsmith@gmail.com",
            40,
            "3001234567",
            "Main St 123",
            new DateOnly(1985, 5, 12),
            "Cardiology"
            );

            _doctorRepository.RegisterDoctor(doctor1);

            Console.WriteLine(doctor1.ToString());

            // Create and register a sample appointment
            Appointment appointment1 = new Appointment(
                patient1.Document,
                doctor1.Document,
                DateTime.Now.AddDays(2),  // appointment scheduled 2 days from now
                "Scheduled",              // initial status
                "Initial appointment"     // note
            );
            appointmentRepository.RegisterAppointment(appointment1);
            Console.WriteLine(appointment1.ToString());
        }
    }
}