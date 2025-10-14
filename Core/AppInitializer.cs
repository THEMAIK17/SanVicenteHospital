using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using SanVicenteHospital.Database;
using SanVicenteHospital.Models;
using SanVicenteHospital.Repositories;
using SanVicenteHospital.Services;
using SanVicenteHospital.Utils;

namespace SanVicenteHospital.Core;

public class AppInitializer
{
    public void Run()
    {
        var dbContext = new DatabaseContext();
        var patientRepository = new PatientRepository(dbContext);
        var doctorRepository = new DoctorRepository(dbContext);
        var appointmentRepository = new AppointmentRepository(dbContext);

        var patientService = new PatientService(patientRepository);
        var doctorService = new DoctorService(doctorRepository);
        var appointmentService = new AppointmentService(appointmentRepository, doctorRepository, patientRepository);

        var showMenuPatient = new ShowMenuPatient(patientService);
        var showMenuDoctor = new ShowMenuDoctor(doctorService);
        var showMenuAppointment = new ShowMenuAppointment(appointmentService);

        DataSeeder.SeedAllData(patientRepository, doctorRepository,appointmentRepository);

        RunMainMenu(showMenuPatient, showMenuDoctor, showMenuAppointment);
    }

    private void RunMainMenu(ShowMenuPatient showMenuPatient,
                                ShowMenuDoctor showMenuDoctor,
                                ShowMenuAppointment showMenuAppointment)
    {
        bool running = true;

        while (running)
        {
            Console.Clear();
            Console.WriteLine("\n--- Menú Principal ---");
            Console.WriteLine("1. Trámites Pacientes");
            Console.WriteLine("2. Trámites Doctores");
            Console.WriteLine("3. Trámites Citas Médicas");
            Console.WriteLine("0. Salir");
            Console.Write("Seleccione una opción: ");
            string option = Console.ReadLine() ?? "";

            switch (option)
            {
                case "1":
                    showMenuPatient.ShowMenuPatient1();
                    break;
                case "2":
                    showMenuDoctor.ShowMenuDoctor1();
                    break;
                case "3":
                    showMenuAppointment.ShowMenuAppointment1();
                    break;
                case "0":
                    running = false;
                    Console.WriteLine("Saliendo...");
                    break;
                default:
                    Console.WriteLine("Opción inválida. Intente de nuevo.");
                    break;
            }

            if (running)
            {
                Console.WriteLine("\nPresione una tecla para continuar...");
                Console.ReadKey();
            }
        }
    }
}
        
      


    



    


