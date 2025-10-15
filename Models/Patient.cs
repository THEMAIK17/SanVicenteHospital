using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthClinic.Models;

namespace SanVicenteHospital.Models;

public class Patient: Person
{
    public Patient(string name,
                    string lastname,
                    string documentType,
                    string document,
                    string email,
                    byte age,
                    string address,
                    string phone,
                    DateOnly birthDay) 
        : base(name, lastname, documentType, document, email, age, address, phone, birthDay)
    {
    }
}
