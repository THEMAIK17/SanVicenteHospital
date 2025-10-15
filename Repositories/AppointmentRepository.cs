using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SanVicenteHospital.Database;
using SanVicenteHospital.Interfaces;
using SanVicenteHospital.Models;

namespace SanVicenteHospital.Repositories
{
   public class AppointmentRepository : IAppointmentRepository
    {
        private readonly DatabaseContext _dbContext;

        // Constructor that receives the database context
        public AppointmentRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Register a new medical appointment
        public void RegisterAppointment(Appointment appointment)
        {
            _dbContext.Appointments.Add(appointment);
        }

        // Retrieve all appointments
        public List<Appointment> ShowAllAppointments()
        {
            return _dbContext.Appointments;
        }

        // Get an appointment by its unique ID
        public Appointment GetAppointmentById(Guid id)
        {
            return _dbContext.Appointments.FirstOrDefault(a => a.Id == id);
        }

        // Get all appointments for a specific patient
        public List<Appointment> GetAppointmentsByPatient(string patientDocument)
        {
            return _dbContext.Appointments
                .Where(a => a.PatientDocument == patientDocument)
                .ToList();
        }

        // Get all appointments for a specific doctor
        public List<Appointment> GetAppointmentsByDoctor(string doctorDocument)
        {
            return _dbContext.Appointments
                .Where(a => a.DoctorDocument == doctorDocument)
                .ToList();
        }

        // Cancel an appointment (change its status to "Canceled")
        public void CancelAppointment(Guid id)
        {
            var appointment = GetAppointmentById(id);
            if (appointment != null)
            {
                appointment.Status = "Canceled";
            }
        }

        // Mark an appointment as completed (status = "Completed")
        public void CompleteAppointment(Guid id)
        {
            var appointment = GetAppointmentById(id);
            if (appointment != null)
            {
                appointment.Status = "Completed";
            }
        }

        // Update appointment details
        public void UpdateAppointment(Appointment updatedAppointment)
        {
            var existingAppointment = GetAppointmentById(updatedAppointment.Id);
            if (existingAppointment != null)
            {
                existingAppointment.PatientDocument = updatedAppointment.PatientDocument;
                existingAppointment.DoctorDocument = updatedAppointment.DoctorDocument;
                existingAppointment.AppointmentDate = updatedAppointment.AppointmentDate;
                existingAppointment.Status = updatedAppointment.Status;
                existingAppointment.Notes = updatedAppointment.Notes;
            }
        }
    }
}