using HospitalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace HospitalManagementSystem.Services;

internal class PatientService
{
    public List<Patient> patients = new List<Patient>();

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
            throw new ArgumentNullException("Patient Id must not be Empty");
        }
        if (string.IsNullOrWhiteSpace(patient.PatientName))
        {
            throw new ArgumentNullException("Patient Name must not be Empty");
        }
        if (patient.Age <= 0 || patient.Age >= 100)
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
        Console.WriteLine("===Patient List===");
        foreach (var patient in patients)
        {
            Console.WriteLine($"Patient Name : {patient.PatientId}\nId : {patient.PatientId}\nPhone Number : {patient.Phonenumber}\nAge : {patient.Age}\nEmail : {patient.Email}");
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
            throw new ArgumentNullException("Patient Name must not be empty");
        }
        if (patient.Age <= 0 || patient.Age >= 100)
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

       string patientName = patient.PatientName;
       string phoneNumber = patient.Phonenumber;
       int age = patient.Age;
       string email = patient.Email;

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
        patients.Remove(p);
        Console.WriteLine("Patient removed Successfully");

    }
}

   

