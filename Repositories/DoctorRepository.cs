using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SanVicenteHospital.Database;
using SanVicenteHospital.Interfaces;
using SanVicenteHospital.Models;

namespace SanVicenteHospital.Repositories;

public class DoctorRepository
{
    private readonly DatabaseContext _Context;

    public DoctorRepository(DatabaseContext context)
    {
        _Context = context;
    }

    public void RegisterDoctor(Doctor doctor)
    {
        _Context.Doctors.Add(doctor);
    }

    public List<Doctor> ShowAllDoctors()
    {
        return _Context.Doctors;
    }

    public Doctor GetDoctorByDocument(string document)
    {
        return _Context.Doctors.FirstOrDefault(p => p.Document == document);
    }
    public void UpdateDoctor(Doctor Updateddoctor)
    {
        var existingDoctor = GetDoctorByDocument(Updateddoctor.Document);
        if (existingDoctor != null)
        {
            existingDoctor.Name = Updateddoctor.Name;
            existingDoctor.LastName = Updateddoctor.LastName;
            existingDoctor.DocumentType = Updateddoctor.DocumentType;
            existingDoctor.Document = Updateddoctor.Document;
            existingDoctor.Email = Updateddoctor.Email;
            existingDoctor.Age = Updateddoctor.Age;
            existingDoctor.Phone = Updateddoctor.Phone;
            existingDoctor.Address = Updateddoctor.Address;
            existingDoctor.BirthDay = Updateddoctor.BirthDay;
            existingDoctor.Specialty = Updateddoctor.Specialty;
        }
    }

    public void DeleteDoctor(string document)
    {
        var doctor = GetDoctorByDocument(document);
        if (doctor != null)
        {
            _Context.Doctors.Remove(doctor);
        }
    }
}


