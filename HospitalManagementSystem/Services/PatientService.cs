using HospitalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace HospitalManagementSystem.Services;

internal class PatientService
{
    private readonly List<Patient> patients;
    private readonly List<Appointment> appointments;

    public PatientService(List<Patient> patients, List<Appointment> appointments)
    {
        this.patients = patients;
        this.appointments = appointments;
    }

    //Register a Patient
    public void RegisterPatient(Patient patient)
    {
        Console.WriteLine("===Register a Patient===");
        if(patient == null)
        {
            throw new ArgumentNullException("Patient value must not be empty");
        }
        if (string.IsNullOrWhiteSpace(patient.PatientId))
        {
            throw new ArgumentException("Patient Id must not be Empty");
        }
        if (string.IsNullOrWhiteSpace(patient.PatientName))
        {
            throw new ArgumentNullException("Patient Name must not be Empty");
        }
        if (patient.Age <= 0 || patient.Age >= 120)
        {
            throw new ArgumentException(
                "Patient age must be between 1 and 119");
        }
        if (string.IsNullOrWhiteSpace(patient.Phonenumber))
        {
            throw new ArgumentException("Phone number must be valid");
        }
        if (string.IsNullOrWhiteSpace(patient.Email))
        {
            throw new ArgumentException("Email must be valid");
        }

        var existingPatient = patients.FirstOrDefault(p => p.PatientId == patient.PatientId);
        if (existingPatient != null)
        {
            throw new InvalidOperationException("Patient already exists");
        }

        patients.Add(patient);
        Console.WriteLine("Patient added Successfully");

    }

    //Display All Patients
    public void DisplayPatients()
    {
        if (patients.Count == 0)
        {
            Console.WriteLine("No patients found.");
            return;
        }
        Console.WriteLine("===Patient List===");
        foreach (var patient in patients)
        {
            Console.WriteLine($"Patient Name : {patient.PatientName}\nId : {patient.PatientId}\nPhone Number : {patient.Phonenumber}\nAge : {patient.Age}\nEmail : {patient.Email}");
        }

    }

    //Display by Id
    public void DispalyById(string patientId)
    {
        Console.WriteLine("=====Patient by Id====");
        var p = patients.FirstOrDefault(p => p.PatientId == patientId);
        if (p != null)
        {
            Console.WriteLine($"Patient Name : {p.PatientName}\nPatient Id : {p.PatientId}");
            return;
        }
        else
        {
            Console.WriteLine("Patient Id not found");
        }
    }

    public void SearchPatient(string searchtext)
    {
        
        Console.WriteLine("===Searching...===");
        var results = patients
            .Where(p => p.PatientName != null && p.PatientName.Contains(searchtext, StringComparison.OrdinalIgnoreCase))
            .ToList();
        if (results.Count == 0)
        {
            Console.WriteLine("No matching patients found.");
            return;
        }

        foreach (var patient in results)
        {
            Console.WriteLine(patient.PatientName);
        }
    }

    public void UpadatePatient(Patient patient)
    {
        Console.WriteLine("===Upadate Patient Deatils====");
        var p = patients.FirstOrDefault(p => p.PatientId == patient.PatientId);
        if (p == null)
        {
            Console.WriteLine("Patient does not exist");
            return;
        }
        if (string.IsNullOrWhiteSpace(patient.PatientName))
        {
            throw new ArgumentException("Patient Name must not be empty");
        }
        if (patient.Age <= 0 || patient.Age >= 120)
        {
            throw new ArgumentException("Patient Age must be valid");
        }
        if (string.IsNullOrWhiteSpace(patient.Phonenumber))
        {
            throw new ArgumentException("Phone number must be valid");
        }
        if (string.IsNullOrWhiteSpace(patient.Email))
        {
            throw new ArgumentException("Email must be valid");
        }

       p.PatientName = patient.PatientName;
       p.Phonenumber = patient.Phonenumber;
       p.Age = patient.Age;
       p.Email = patient.Email;

        Console.WriteLine("Patient details Updated Successfully");

    }

    public void DeletePatient(string patientId)
    {
        Console.WriteLine("===Delete Patient===");
        var p = patients.FirstOrDefault(p => p.PatientId == patientId);
        if (p == null)
        {
            Console.WriteLine("Patient does not exist");
            return;
        }
        bool hasAppointments = appointments.Any(
         a => a.PatientId == patientId);

        if (hasAppointments)
        {
            Console.WriteLine(
                "Cannot delete patient because appointments are linked.");
            return;
        }
        patients.Remove(p);
        Console.WriteLine("Patient removed Successfully");

    }
}

   

