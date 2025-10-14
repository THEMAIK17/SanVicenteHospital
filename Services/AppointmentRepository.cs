using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SanVicenteHospital.Interfaces;
using SanVicenteHospital.Models;
using SanVicenteHospital.Database;
using SanVicenteHospital.Repositories;
using System.Net;
using System.Net.Mail;

namespace SanVicenteHospital.Services
{
    public class AppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly DoctorRepository _doctorRepository;
        private readonly PatientRepository _patientRepository;

        public AppointmentService(
            IAppointmentRepository appointmentRepository,
            DoctorRepository doctorRepository,
            PatientRepository patientRepository)
        {
            _appointmentRepository = appointmentRepository;
            _doctorRepository = doctorRepository;
            _patientRepository = patientRepository;
        }

        public void RegisterAppointment()
        {
            try
            {
                Console.WriteLine("Enter Patient Document:");
                string patientDocument = Console.ReadLine().Trim();
                var patient = _patientRepository.GetPatientByDocument(patientDocument);
                if (patient == null)
                {
                    Console.WriteLine("No patient found with that document.");
                    return;
                }

                Console.WriteLine("Enter Doctor Document:");
                string doctorDocument = Console.ReadLine().Trim();
                var doctor = _doctorRepository.GetDoctorByDocument(doctorDocument);
                if (doctor == null)
                {
                    Console.WriteLine("No doctor found with that document.");
                    return;
                }

                Console.WriteLine("Enter Appointment Date (YYYY-MM-DD HH:MM):");
                DateTime appointmentDate;
                if (!DateTime.TryParse(Console.ReadLine().Trim(), out appointmentDate))
                {
                    Console.WriteLine("Invalid date format. Use YYYY-MM-DD HH:MM.");
                    return;
                }
                if (appointmentDate < DateTime.Now)
                {
                    Console.WriteLine("The appointment date cannot be in the past.");
                    return;
                }

                // Check doctor availability
                bool doctorBusy = _appointmentRepository
                    .ShowAllAppointments()
                    .Any(a => a.DoctorDocument == doctorDocument &&
                              a.AppointmentDate == appointmentDate &&
                              a.Status != "Canceled");
                if (doctorBusy)
                {
                    Console.WriteLine("The doctor already has an appointment at that time.");
                    return;
                }

                // Check patient availability
                bool patientBusy = _appointmentRepository
                    .ShowAllAppointments()
                    .Any(a => a.PatientDocument == patientDocument &&
                              a.AppointmentDate == appointmentDate &&
                              a.Status != "Canceled");
                if (patientBusy)
                {
                    Console.WriteLine("The patient already has an appointment at that time.");
                    return;
                }

                // Create and register the appointment with initial status and note
                Appointment appointment = new Appointment(
                    patientDocument,
                    doctorDocument,
                    appointmentDate,
                    "Scheduled",          // initial status
                    "Initial note"        // initial note
                );
                _appointmentRepository.RegisterAppointment(appointment);

                Console.WriteLine("Appointment registered successfully.");

                // Send confirmation email
                bool emailSent = SendAppointmentEmail(patient.Email, appointment);
                appointment.Notes = emailSent ? "Email sent successfully" : "Email not sent";
                Console.WriteLine(emailSent ? "Confirmation email sent." : "Failed to send confirmation email.");
            }
            catch
            {
                Console.WriteLine("An error occurred during appointment registration. Please try again.");
            }
    
        }

        public void CancelAppointment()
        {
            Console.WriteLine("Enter Appointment ID to cancel:");
            if (!Guid.TryParse(Console.ReadLine().Trim(), out Guid id))
            {
                Console.WriteLine("Invalid ID format.");
                return;
            }

            var appointment = _appointmentRepository.GetAppointmentById(id);
            if (appointment == null)
            {
                Console.WriteLine("No appointment found with that ID.");
                return;
            }

            _appointmentRepository.CancelAppointment(id);
            Console.WriteLine("Appointment canceled successfully.");
        }

        public void CompleteAppointment()
        {
            Console.WriteLine("Enter Appointment ID to mark as completed:");
            if (!Guid.TryParse(Console.ReadLine().Trim(), out Guid id))
            {
                Console.WriteLine("Invalid ID format.");
                return;
            }

            var appointment = _appointmentRepository.GetAppointmentById(id);
            if (appointment == null)
            {
                Console.WriteLine("No appointment found with that ID.");
                return;
            }

            _appointmentRepository.CompleteAppointment(id);
            Console.WriteLine("Appointment marked as completed.");
        }

        public void ShowAllAppointments()
        {
            var appointments = _appointmentRepository.ShowAllAppointments();
            if (!appointments.Any())
            {
                Console.WriteLine("No appointments found.");
                return;
            }

            Console.WriteLine("\nRegistered Appointments:\n");
            foreach (var appointment in appointments)
            {
                Console.WriteLine(appointment.ToString());
            }
        }

        public void GetAppointmentsByPatient()
        {
            Console.WriteLine("Enter Patient Document:");
            string document = Console.ReadLine().Trim();

            var appointments = _appointmentRepository.GetAppointmentsByPatient(document);
            if (!appointments.Any())
            {
                Console.WriteLine("No appointments found for that patient.");
                return;
            }

            Console.WriteLine($"\nAppointments for patient '{document}':\n");
            foreach (var appointment in appointments)
            {
                Console.WriteLine(appointment.ToString());
            }
        }

        public void GetAppointmentsByDoctor()
        {
            Console.WriteLine("Enter Doctor Document:");
            string document = Console.ReadLine().Trim();

            var appointments = _appointmentRepository.GetAppointmentsByDoctor(document);
            if (!appointments.Any())
            {
                Console.WriteLine("No appointments found for that doctor.");
                return;
            }

            Console.WriteLine($"\nAppointments for doctor '{document}':\n");
            foreach (var appointment in appointments)
            {
                Console.WriteLine(appointment.ToString());
            }
        }

        private bool SendAppointmentEmail(string patientEmail, Appointment appointment)
        {
            try
            {
                using (var smtpClient = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtpClient.Credentials = new NetworkCredential("tu_correo@gmail.com", "tu_contraseña_o_token");
                    smtpClient.EnableSsl = true;

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress("tu_correo@gmail.com", "San Vicente Hospital"),
                        Subject = "Appointment Confirmation",
                        Body = $@"
                    
                        Dear Patient,

                        Your appointment has been successfully scheduled.

                        Date: {appointment.AppointmentDate:dd/MM/yyyy HH:mm}
                        Status: {appointment.Status}
                        Thank you for choosing San Vicente Hospital!",
                        IsBodyHtml = false
                    };

                    mailMessage.To.Add(patientEmail);
                    smtpClient.Send(mailMessage);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}