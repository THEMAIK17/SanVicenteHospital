using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using SanVicenteHospital.Database;
using SanVicenteHospital.Interfaces;
using SanVicenteHospital.Models;
namespace SanVicenteHospital.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly DatabaseContext _Context;

        public PatientRepository(DatabaseContext context)
        {
            _Context = context;
        }

        public void RegisterPatient(Patient patient)
        {
            _Context.Patients.Add(patient);
        }

        public List<Patient> ShowAllPatients()
        {
            return _Context.Patients;
        }

        public Patient GetPatientByDocument(string document)
        {
            return _Context.Patients.FirstOrDefault(p => p.Document == document);
        }
        public void UpdatePatient(Patient Updatedpatient)
        {
            var existingPatient = GetPatientByDocument(Updatedpatient.Document);
            if (existingPatient != null)
            {
                existingPatient.Name = Updatedpatient.Name;
                existingPatient.LastName = Updatedpatient.LastName;
                existingPatient.DocumentType = Updatedpatient.DocumentType;
                existingPatient.Document = Updatedpatient.Document;
                existingPatient.Email = Updatedpatient.Email;
                existingPatient.Age = Updatedpatient.Age;
                existingPatient.Address = Updatedpatient.Address;
                existingPatient.Phone = Updatedpatient.Phone;
                existingPatient.BirthDay = Updatedpatient.BirthDay;
            }
        }

        public void DeletePatient(string document)
        {
            var patient = GetPatientByDocument(document);
            if (patient != null)
            {
                _Context.Patients.Remove(patient);
            }
        }
    }
}