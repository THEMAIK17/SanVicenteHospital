using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthClinic.Models;

namespace SanVicenteHospital.Models;

public class Doctor : Person
{
    public string Specialty { get; set; }


    public Doctor(string name,
                string lastName,
                string documentType,
                string document,
                string email,
                byte age,
                string phone,
                string address,
                DateOnly birthDay,
                string specialty)
      : base(name, lastName, documentType, document, email, age, address, phone, birthDay)
    {
        Specialty = specialty;
    }

    public override string ToString()
    {
        return $@"
        ----------------------------------------
                    Id: {Id}
                    Name: {Name}
                    LastName: {LastName}
                    DocumentType: {DocumentType}
                    Document: {Document}
                    Email: {Email}
                    Phone: {Phone}
                    Address: {Address}
                    Specialty: {Specialty}
        ----------------------------------------            
        ";
                    
    }
}


