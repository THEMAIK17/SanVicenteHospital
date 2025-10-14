using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SanVicenteHospital.Models;
using SanVicenteHospital.Repositories;


namespace SanVicenteHospital.Database
{
    public class DataSeeder
    {
        public static void SeedAllData(
          PatientRepository patientRepository
        )
        {
            // Create and register a sample customer

            Patient patient1 = new Patient ( "Joh","Doe","Passport","A12345678","maikol@gmail.com",30,"Main St,555-1234","165462343", new DateOnly(1993, 1, 1));
            patientRepository.RegisterPatient(patient1);

            Console.WriteLine(patient1.ToString());
        }
    }
}