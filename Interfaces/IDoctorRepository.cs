using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SanVicenteHospital.Models;
namespace SanVicenteHospital.Interfaces;

public interface IDoctorRepository
{

    void RegisterDoctor(Doctor doctor);

    List<Doctor> ShowAllDoctors();

    Doctor GetDoctorByDocument(string document);

    void UpdateDoctor(Doctor Updateddoctor);
    void DeleteDoctor(string document);

}
