using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SanVicenteHospital.Services;

namespace SanVicenteHospital.Utils;

public class ShowMenuAppointment
{


    public readonly AppointmentService _appointmentService;

    public ShowMenuAppointment(AppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }

    bool running = true;
    public void ShowMenuAppointment1()
    {
        Console.Clear();
        Console.WriteLine("\n--- Menú de Citas Médicas ---");
        Console.WriteLine("1. Registrar cita");
        Console.WriteLine("2. Mostrar todas las citas");
        Console.WriteLine("3. Buscar citas por paciente");
        Console.WriteLine("4. Buscar citas por doctor");
        Console.WriteLine("5. Cancelar cita");
        Console.WriteLine("6. Marcar cita como atendida");
        Console.WriteLine("0. Salir");
        Console.Write("Seleccione una opción: ");
        string optionAppointment = Console.ReadLine() ?? "";

        switch (optionAppointment)
        {
            case "1":
                _appointmentService.RegisterAppointment();
                break;
            case "2":
                _appointmentService.ShowAllAppointments();
                break;
            case "3":
                _appointmentService.GetAppointmentsByPatient();
                break;
            case "4":
                _appointmentService.GetAppointmentsByDoctor();
                break;
            case "5":
                _appointmentService.CancelAppointment();
                break;
            case "6":
                _appointmentService.CompleteAppointment();
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

