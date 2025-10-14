using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SanVicenteHospital.Models;

namespace SanVicenteHospital.Database
{
    public class DatabaseContext
    {
        private List<Patient> _Patients = new List<Patient>();
        public List<Patient> Patients => _Patients;
    }
}