using HospitalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Services;

    internal class DoctorService{
    public List<Doctor> doctors = new List<Doctor>();

    //Register a Patient
    public void RegisterDoctor(Doctor doctor)
    {
        Console.WriteLine("===Register a Doctor===");
        if (doctor == null)
        {
            throw new ArgumentNullException("Doctor value must not be empty");
        }
        if (string.IsNullOrWhiteSpace(doctor.Id))
        {
            throw new ArgumentNullException("DOctor Id must not be Empty");
        }
        if (string.IsNullOrWhiteSpace(doctor.Name))
        {
            throw new ArgumentNullException("Doctor Name must not be Empty");
        }
        if (string.IsNullOrWhiteSpace(doctor.Specialization))
        {
            throw new ArgumentNullException("Specialization must not be empty");
        }
        if (doctor.ConsultationFee <= 0)
        {
            throw new ArgumentNullException("Consultation fee not non-nagative");
        }

        var existingDoctor = doctors.FirstOrDefault(p => p.Id == doctor.Id);
        if (existingDoctor != null)
        {
            throw new InvalidOperationException("Doctor already exists");
        }

        doctors.Add(doctor);
        Console.WriteLine("Patient added Successfully");

    }

    //Display All Doctors
    public void DisplayDoctor()
    {
        Console.WriteLine("===Dispaly Doctor List===");
        foreach (var doctor in doctors)
        {
            Console.WriteLine($"Doctor Name : {doctor.Name}\nId : {doctor.Id}\nSpecialization : {doctor.Specialization}\nConsultation Fee : {doctor.ConsultationFee}");
        }

    }

    //Display by Id
    public void DispalyById(string doctorId)
    {
        Console.WriteLine("====Doctor by Id====");
        var p = doctors.FirstOrDefault(p => p.Id == doctorId);
        if (p != null)
        {
            Console.WriteLine($"Doctor Name : {p.Name}\nDoctor Id : {p.Id}");
            return;
        }
        else
        {
            Console.WriteLine("Doctor Id not found");
        }
    }

    //Search Doctor
    public void SearchDoctor(string searchtext)
    {
        Console.WriteLine("====Doctor by Search====");
        var results = doctors
            .Where(p => p.Name != null && ((p.Name.Contains(searchtext, StringComparison.OrdinalIgnoreCase))
            || (p.Specialization.Contains(searchtext, StringComparison.OrdinalIgnoreCase)))).ToList();
          

        foreach (var doctors in results)
        {
            Console.WriteLine($"Doctor : {doctors.Name}Specialization : {doctors.Specialization}");
        }
    }

    //Upadate Doctor details
    public void UpadateDoctor(Doctor doctor)
    {
        Console.WriteLine("===Update Doctor details===");
        var p = doctors.FirstOrDefault(p => p.Id == doctor.Id);
        if (p == null)
        {
            Console.WriteLine("Doctor does not exist");
            return;
        }
        if (string.IsNullOrWhiteSpace(doctor.Specialization))
        {
            throw new ArgumentNullException("Specialization must not be empty");
        }
        if(doctor.ConsultationFee <= 0)
        {
            throw new ArgumentNullException("Consultation fee not non-nagative");
        }
        string specialization = doctor.Specialization;
        decimal consultationFee = doctor.ConsultationFee;

        Console.WriteLine("Doctors details Updated Successfully");

    }

    //Delete doctor
    public void DeleteDoctor(string Id)
    {
        Console.WriteLine("===Delete Doctor===");
        var p = doctors.FirstOrDefault(p => p.Id == Id);
        if (p == null)
        {
            Console.WriteLine("Doctor does not exist");
            return;
        }
        doctors.Remove(p);
        Console.WriteLine("Doctor removed Successfully");

    }
}

