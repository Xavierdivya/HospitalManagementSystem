using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Models;

    internal class Patient
{
    public string PatientId { get; set; }
    public string PatientName { get; set; }
    public int Age { get; set; }
    public string Phonenumber { get; set; }
    public string Email { get; set; }

    public Patient(string patientId,string patientName,int age,string phoneNumber,string email)
    {
        PatientId = patientId;
        PatientName = patientName;
        Age = age;
        Phonenumber = phoneNumber;
        Email = email;

    }
    public Patient()
    { }
    //public void DispalyPatient()
    //{
    //    Console.WriteLine($"Patient Id : {PatientId}\nPatient Name  : {PatientName}\nAge : {Age}\nPhone Number : {Phonenumber}\nEmail : {Email}");
    //}
    
}
    

