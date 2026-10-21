using System.Collections.Generic;
using HospitalManagementSystem.Models;
using HospitalManagementSystem.Services;

public class Program
{
    public static void Main(string[] args)
    {


        JsonDataService jsonService = new JsonDataService();

        List<Patient> patients =
            jsonService.Load<Patient>("patients.json");

        List<Doctor> doctors =
            jsonService.Load<Doctor>("doctors.json");

        List<Appointment> appointments =
            jsonService.Load<Appointment>("appointments.json");

        PatientService patientService = new PatientService(patients,appointments);

        DoctorService doctorService =
            new DoctorService(doctors, appointments);

        AppointmentService appointmentService =
            new AppointmentService(patients, doctors, appointments);

        bool res = true;
        while (res)
        {
            Console.WriteLine("1.Register Patient");
            Console.WriteLine("2.Display Patient");
            Console.WriteLine("3.Display Patient by Id");
            Console.WriteLine("4.Search Patient");
            Console.WriteLine("5.Upadate Patient");
            Console.WriteLine("6.Delete Patient");
            Console.WriteLine("7.Register Doctor");
            Console.WriteLine("8.Display Doctor");
            Console.WriteLine("9.Display Doctor by Id");
            Console.WriteLine("10.Search Doctor");
            Console.WriteLine("11.Upadate Doctor");
            Console.WriteLine("12.Delete Doctor");
            Console.WriteLine("13. Book Appointment");
            Console.WriteLine("14. Display All Appointments");
            Console.WriteLine("15. Display Appointment by ID");
            Console.WriteLine("16. Reschedule Appointment");
            Console.WriteLine("17. Cancel Appointment");
            Console.WriteLine("18. Complete Appointment");
            Console.WriteLine("19. Display Appointments by Patient");
            Console.WriteLine("20. Display Appointments by Doctor");
            Console.WriteLine("21. Display Appointments by Date");
            Console.WriteLine("22. Display Appointments by Status");
            Console.WriteLine("23. Exit");
            Console.Write("Enter your choice: ");

            string? choice = Console.ReadLine() ?? "";
            if (string.IsNullOrWhiteSpace(choice))
            {
                Console.WriteLine("Enter valid option(1-13");
                return;
            }

            switch (choice)
            {
                case "1":

                    Console.Write("Enter Patient ID: ");
                    string id = Console.ReadLine() ?? "";

                    Console.Write("Enter Patient Name: ");
                    string name = Console.ReadLine() ?? "";

                    Console.Write("Enter Patient Age: ");
                    int age = Convert.ToInt32(Console.ReadLine());

                    Console.Write("Enter Phone Number: ");
                    string phone = Console.ReadLine() ?? "";

                    Console.Write("Enter Email: ");
                    string email = Console.ReadLine() ?? "";

                    Patient patient = new Patient(id, name, age, phone, email);
                    try
                    {
                        patientService.RegisterPatient(patient);
                        jsonService.Save("patients.json", patients);
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"Registration failed: {ex.Message}");
                    }
                    catch (InvalidOperationException ex)
                    {
                        Console.WriteLine($"Registration failed: {ex.Message}");
                    }

                    break;

                case "2":
                    patientService.DisplayPatients();
                    break;

                case "3":
                    Console.Write("Enter Patient ID: ");
                    string id1 = Console.ReadLine() ?? "";

                    patientService.DispalyById(id1);
                    break;

                case "4":
                    Console.Write("Enter Search text: ");
                    string text = Console.ReadLine() ?? "";
                    patientService.SearchPatient(text);
                    break;

                case "5":
                    Console.Write("Enter Patient ID: ");
                    string id2 = Console.ReadLine() ?? "";

                    Console.Write("Enter Patient Name: ");
                    string name1 = Console.ReadLine() ?? "";

                    Console.Write("Enter Patient Age: ");
                    if (!int.TryParse(Console.ReadLine(), out int age1))
                    {
                        Console.WriteLine("Invalid age. Enter a number.");
                        break;
                    }

                    Console.Write("Enter Phone Number: ");
                    string phone1 = Console.ReadLine() ?? "";

                    Console.Write("Enter Email: ");
                    string email1 = Console.ReadLine() ?? "";

                    Patient patient1 = new Patient(id2, name1, age1, phone1, email1);
                    try
                    {
                        patientService.UpadatePatient(patient1);
                        jsonService.Save("patients.json", patients);
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"Update failed: {ex.Message}");
                    }
                    break;

                case "6":
                    Console.Write("Enter Patient ID: ");
                    string id3 = Console.ReadLine() ?? "";

                    patientService.DeletePatient(id3);
                    jsonService.Save("patients.json", patients);
                    break;

                case "7":
                    Console.Write("Enter Doctor ID: ");
                    string id4 = Console.ReadLine() ?? "";

                    Console.Write("Enter Doctor Name: ");
                    string name4 = Console.ReadLine() ?? "";

                    Console.Write("Enter Specialization: ");
                    string specialization = Console.ReadLine() ?? "";

                    Console.Write("Enter Consultation Fee: ");
                    decimal fee = decimal.Parse(Console.ReadLine());

                    Doctor doctor = new Doctor(id4, name4, specialization, fee);
                    try
                    {
                        doctorService.RegisterDoctor(doctor);
                        jsonService.Save("doctors.json", doctors);
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"Registration failed: {ex.Message}");
                    }
                    catch (InvalidOperationException ex)
                    {
                        Console.WriteLine($"Registration failed: {ex.Message}");
                    }

                    break;

                case "8":
                    doctorService.DisplayDoctor();
                    break;

                case "9":
                    Console.Write("Enter Doctor ID: ");
                    string id5 = Console.ReadLine() ?? "";

                    doctorService.DispalyById(id5);
                    break;

                case "10":
                    Console.Write("Enter Doctor Name or specialization: ");
                    string text1 = Console.ReadLine() ?? "";

                    doctorService.SearchDoctor(text1);
                    break;

                case "11":
                    Console.Write("Enter Doctor ID: ");
                    string id6 = Console.ReadLine() ?? "";

                    Console.Write("Enter Doctor Name: ");
                    string name6 = Console.ReadLine() ?? "";

                    Console.Write("Enter Specialization: ");
                    string specialization1 = Console.ReadLine() ?? "";

                    Console.Write("Enter Consultation Fee: ");
                    decimal fee1 = decimal.Parse(Console.ReadLine());

                    Doctor doctor2 = new Doctor(id6, name6, specialization1, fee1);
                    try
                    {
                        doctorService.UpadateDoctor(doctor2);
                        jsonService.Save("doctors.json", doctors);
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"Update failed: {ex.Message}");
                    }
                    break;

                case "12":
                    Console.Write("Enter Doctor ID: ");
                    string id7 = Console.ReadLine() ?? "";

                    doctorService.DeleteDoctor(id7);
                    jsonService.Save("doctors.json", doctors);
                    break;

                case "13":
                    Console.WriteLine("=== Book Appointment ===");

                    Console.Write("Enter Patient ID: ");
                    string patientId = Console.ReadLine() ?? "";

                    Console.Write("Enter Doctor ID: ");
                    string doctorId = Console.ReadLine() ?? "";

                    Console.Write("Enter Appointment Date and Time (dd-MM-yyyy HH:mm): ");
                    string dateInput = Console.ReadLine() ?? "";

                    if (!DateTime.TryParseExact(
                        dateInput,
                        "dd-MM-yyyy HH:mm",
                        System.Globalization.CultureInfo.InvariantCulture,
                        System.Globalization.DateTimeStyles.None,
                        out DateTime appointmentDateTime))
                    {
                        Console.WriteLine("Invalid date format.");
                        break;
                    }

                    Console.Write("Enter Appointment Reason: ");
                    string reason = Console.ReadLine() ?? "";

                    try
                    {
                        appointmentService.BookAppointment(patientId, doctorId, appointmentDateTime, reason);
                        jsonService.Save("appointments.json", appointments);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    break;

                case "14":
                    appointmentService.DisplayAllAppointments();
                    break;

                case "15":
                    Console.Write("Enter Appointment ID: ");
                    string appointmentId = Console.ReadLine() ?? "";

                    appointmentService.DisplayAppointmentById(appointmentId);
                    break;

                case "16":
                    Console.Write("Enter Appointment ID: ");
                    string rescheduleId = Console.ReadLine() ?? "";

                    Console.Write("Enter New Date and Time (dd-MM-yyyy HH:mm): ");
                    string newDateInput = Console.ReadLine() ?? "";

                    if (!DateTime.TryParseExact(
                        newDateInput,
                        "dd-MM-yyyy HH:mm",
                        System.Globalization.CultureInfo.InvariantCulture,
                        System.Globalization.DateTimeStyles.None,
                        out DateTime newDateTime))
                    {
                        Console.WriteLine("Invalid date format.");
                        break;
                    }

                    try
                    {
                        appointmentService.RescheduleAppointment(rescheduleId, newDateTime);
                        jsonService.Save("appointments.json", appointments);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    break;

                case "17":
                    Console.Write("Enter Appointment ID to cancel: ");
                    string cancelId = Console.ReadLine() ?? "";

                    try
                    {
                        appointmentService.CancelAppointment(cancelId);
                        jsonService.Save("appointments.json", appointments);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    break;

                case "18":
                    Console.Write("Enter Appointment ID to complete: ");
                    string completeId = Console.ReadLine() ?? "";

                    try
                    {
                        appointmentService.CompleteAppointment(completeId);
                        jsonService.Save("appointments.json", appointments);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    break;

                case "19":
                    Console.Write("Enter Patient ID: ");
                    string searchPatientId = Console.ReadLine() ?? "";

                    appointmentService.DisplayAppointmentsByPatient(searchPatientId);
                    break;

                case "20":
                    Console.Write("Enter Doctor ID: ");
                    string searchDoctorId = Console.ReadLine() ?? "";

                    appointmentService.DisplayAppointmentsByDoctor(searchDoctorId);
                    break;

                case "21":
                    Console.Write("Enter Date (dd-MM-yyyy): ");
                    string searchDateInput = Console.ReadLine() ?? "";

                    if (DateTime.TryParseExact(
                        searchDateInput,
                        "dd-MM-yyyy",
                        System.Globalization.CultureInfo.InvariantCulture,
                        System.Globalization.DateTimeStyles.None,
                        out DateTime searchDate))
                    {
                        appointmentService.DisplayAppointmentsByDate(searchDate);
                    }
                    else
                    {
                        Console.WriteLine("Invalid date format.");
                    }

                    break;


                case "22":
                    Console.WriteLine("1. Scheduled");
                    Console.WriteLine("2. Cancelled");
                    Console.WriteLine("3. Completed");
                    Console.Write("Select appointment status: ");

                    string statusChoice = Console.ReadLine() ?? "";

                    AppointmentStatus status;

                    switch (statusChoice)
                    {
                        case "1":
                            status = AppointmentStatus.Scheduled;
                            break;

                        case "2":
                            status = AppointmentStatus.Cancelled;
                            break;

                        case "3":
                            status = AppointmentStatus.Completed;
                            break;

                        default:
                            Console.WriteLine("Invalid status.");
                            continue;
                    }

                    appointmentService.DisplayAppointmentsByStatus(status);
                    break;

                case "23":
                    Console.WriteLine("Thank you! Exiting...");
                    res = false;
                    break;

                default:
                    Console.WriteLine("Enter valid option (1-23)");
                    break;
            }
        }

    }
}
