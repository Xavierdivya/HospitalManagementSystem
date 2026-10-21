using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Models;

    internal class Doctor
    {
    public string Id { get; set; }
    public string Name { get; set; }
    public string Specialization { get; set; }
    public decimal ConsultationFee { get; set; }

    public Doctor(string id,string name,string specialization,decimal consultationFee)
    {
        Id = id;
        Name = name;
        Specialization = specialization;
        ConsultationFee = consultationFee;
    }

    public Doctor() { }

    public void DispalyDoctors()
    {
        Console.WriteLine($"Doctor Name : {Name}\nDoctor Id : {Id}\nSpecialization : {Specialization}\nConsultation Fee : {ConsultationFee}");
    }
}

