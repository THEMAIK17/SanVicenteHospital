using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SanVicenteHospital.Services;

namespace SanVicenteHospital.Utils
{
    public class ShowMenuDoctor
    {

        public readonly DoctorService _doctorService;

        public ShowMenuDoctor(DoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        bool running = true;
        public void ShowMenuDoctor1()
        {
            Console.Clear();
            Console.WriteLine("\n--- Menú Principal ---");
            Console.WriteLine("1. registrar doctor");
            Console.WriteLine("2. mostrar todos los doctores");
            Console.WriteLine("3. buscar doctor por documento");
            Console.WriteLine("4. filtrar por especialidad");
            Console.WriteLine("5. editar cliente");
            Console.WriteLine("6. eliminar cliente");
            Console.WriteLine("0. Salir");
            Console.Write("Seleccione una opción: ");
            string optionDoctor = Console.ReadLine() ?? "";
            switch (optionDoctor)
            {
                case "1":
                    _doctorService.RegisterDoctor();
                    break;
                case "2":
                    _doctorService.ShowAllDoctors();
                    break;
                case "3":
                    _doctorService.GetDoctorByDocument();
                    break;
                case "4":
                    _doctorService.FilterBySpecialty();
                    break;
                case "5":
                    _doctorService.UpdateDoctor();
                    break;
                case "6":
                    _doctorService.DeleteDoctor();
                    break;
                case "0":
                    running = false;
                    Console.WriteLine("Saliendo...");
                    break;
                default:
                    Console.WriteLine("Opción inválida. Intente de nuevo.");
                    break;
            }

        }


    }
}