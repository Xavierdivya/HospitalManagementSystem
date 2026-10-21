using HospitalManagementSystem.Models;
using HospitalManagementSystem.Services;

public class Program
{
    public static void Main(string[] args)
    {
        
        PatientService patientService = new PatientService();
        DoctorService doctorService = new DoctorService();
        
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

            string? choice = Console.ReadLine() ?? "";
            if (string.IsNullOrWhiteSpace(choice))
            {
                Console.WriteLine("Enter valid option(1-13");
                return;
            }

            switch (choice)
            {
                case "1":
                    patientService.RegisterPatient(new Patient("101", "Divya", 20, "987654321", "div@gmail.com"));
                    break;

                case "2":
                    patientService.DisplayPatients();
                    break;

                case "3":
                    patientService.DispalyById("101");
                    break;

                case "4":
                    patientService.SearchPatient("div");
                    break;

                case "5":
                    patientService.UpadatePatient(new Patient("102", "Mary", 23, "2345678", "mary@gmail.com"));
                    break;

                case "6":
                    patientService.DeletePatient("101");
                    break;

                case "7":
                    doctorService.RegisterDoctor(new Doctor("008","Leo","Cardiology",23.45m));
                    break;

                case "8":
                    doctorService.DisplayDoctor();
                    break;

                case "9":
                    doctorService.DispalyById("008");
                    break;

                case "10":
                    doctorService.SearchDoctor("leo");
                    break;

                case "11":
                    doctorService.UpadateDoctor(new Doctor("009","Ruban","Ortho",89.67m));
                    break;

                case "12":
                    doctorService.DeleteDoctor("008");
                    break;

                case "13":
                    Console.WriteLine("Thank you!Exiting.....");
                    res = false;
                    return;

                default:
                    Console.WriteLine("Enter valid option (1-13)");
                    break;
            }
        }

        //Patient patient = 
        //Patient patient2 = new Patient("102", "Rose", 21, "987655631", "rose@gmail.com");
        //Doctor doctor = new Doctor("do12","Maran","cardiology",23.78m);
        //doctor.DispalyDoctors();
        //patientService.RegisterPatient(patient2);
        //patientService.DisplayPatients();
        //doctorService.RegisterDoctor(new Doctor("009", "Leo", "Cardiology", 23.67m));


    }
}
