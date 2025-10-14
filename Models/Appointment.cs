using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SanVicenteHospital.Models;

public class Appointment
{
    // Unique identifier for each appointment
    public Guid Id { get; set; }

    // Document of the patient linked to the appointment
    public string PatientDocument { get; set; }

    // Document of the doctor assigned to the appointment
    public string DoctorDocument { get; set; }

    // Appointment date and time
    public DateTime AppointmentDate { get; set; }

    // Current appointment status (Scheduled, Completed, Canceled)
    public string Status { get; set; }

    // Optional notes about the appointment
    public string Notes { get; set; }

    // Constructor
   public Appointment(string patientDocument, string doctorDocument, DateTime appointmentDate, string status, string notes)
    {
        Id = Guid.NewGuid();
        PatientDocument = patientDocument;
        DoctorDocument = doctorDocument;
        AppointmentDate = appointmentDate;
        Status = status;
        Notes = notes;
    }

    // Empty constructor (required for deserialization or repository operations)
    public Appointment() { }

    // String representation for displaying appointment details
    public override string ToString()
    {
        return $@"
        ----------------------------------------
            ID: {Id}
            Patient Document: {PatientDocument}
            Doctor Document: {DoctorDocument}
            Date: {AppointmentDate}
            Status: {Status}
            Notes: {Notes}
            
        ----------------------------------------";
    }
}
