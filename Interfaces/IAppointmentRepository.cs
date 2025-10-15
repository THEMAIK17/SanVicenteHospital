using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SanVicenteHospital.Models;

namespace SanVicenteHospital.Interfaces;

public interface IAppointmentRepository
{
    // Register a new medical appointment
    void RegisterAppointment(Appointment appointment);

    // Retrieve all appointments from the system
    List<Appointment> ShowAllAppointments();

    // Get a specific appointment by its unique identifier
    Appointment GetAppointmentById(Guid id);

    // List all appointments assigned to a specific patient
    List<Appointment> GetAppointmentsByPatient(string patientDocument);

    // List all appointments assigned to a specific doctor
    List<Appointment> GetAppointmentsByDoctor(string doctorDocument);

    // Cancel an appointment (change its status to "Canceled")
    void CancelAppointment(Guid id);

    // Mark an appointment as completed (status = "Completed")
    void CompleteAppointment(Guid id);

    // Update appointment details
    void UpdateAppointment(Appointment updatedAppointment);
}
