using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SanVicenteHospital.Models;

namespace SanVicenteHospital.Interfaces;

public interface IPatientRepository
{
    void RegisterPatient(Patient patient);

    List<Patient> ShowAllPatients();

    Patient GetPatientByDocument(string document);

    void UpdatePatient(Patient Updatedpatient);
    void DeletePatient(string document);
}

