using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SanVicenteHospital.Models;
using SanVicenteHospital.Services;

namespace SanVicenteHospital.Utils
;

public class ShowMenuPatient
{
    public readonly PatientService _patientService;

    public ShowMenuPatient(PatientService patientService)
    {
        _patientService = patientService;
    }

    bool running = true;
    public void ShowMenuPatient1()
    {
        Console.Clear();
        Console.WriteLine("\n--- Menú Principal ---");
        Console.WriteLine("1. registrar cliente");
        Console.WriteLine("2. mostrar todos los clientes");
        Console.WriteLine("3. buscar cliente por documento");
        Console.WriteLine("4. editar cliente");
        Console.WriteLine("5. eliminar cliente");
        Console.WriteLine("0. Salir");
        Console.Write("Seleccione una opción: ");
        string optionPatient = Console.ReadLine() ?? "";
        switch (optionPatient)
        {
            case "1":
                _patientService.RegisterPatient();
                break;
            case "2":
                _patientService.ShowAllPatients();
                break;
            case "3":
                _patientService.GetPatientByDocument();
                break;
            case "4":
                _patientService.UpdatePatient();
                break;
            case "5":
                _patientService.DeletePatient();
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
