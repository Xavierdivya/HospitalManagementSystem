
using HospitalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HospitalManagementSystem.Services
{
    internal class AppointmentService
    {
        public List<Appointment> appointments;
        public List<Patient> patients;
        public List<Doctor> doctors;

        public AppointmentService(
            List<Patient> patients,
            List<Doctor> doctors,
            List<Appointment> appointments)
        {
            this.patients = patients;
            this.doctors = doctors;
            this.appointments = appointments;
        }

        public string GenerateAppointmentId()
        {
            int number = 1;
            string id;
            do
            {
                id = $"A{number:D3}";
                number++;
            }
            while (appointments.Any(a => a.AppointmentId == id));
            return id;
        }

        // Book an appointment
        public void BookAppointment(
            string patientId,
            string doctorId,
            DateTime appointmentDateTime,
            string reason)
        {
            if (string.IsNullOrWhiteSpace(patientId))
                throw new ArgumentException("Patient ID is required.");

            if (string.IsNullOrWhiteSpace(doctorId))
                throw new ArgumentException("Doctor ID is required.");

            Patient? patient = patients.FirstOrDefault(
                p => p.PatientId == patientId);

            if (patient == null)
                throw new InvalidOperationException(
                    "Patient does not exist.");

            Doctor? doctor = doctors.FirstOrDefault(
                d => d.Id == doctorId);

            if (doctor == null)
                throw new InvalidOperationException(
                    "Doctor does not exist.");

            if (appointmentDateTime <= DateTime.Now)
                throw new ArgumentException(
                    "Appointment must be scheduled for a future date and time.");

            if (string.IsNullOrWhiteSpace(reason))
                throw new ArgumentException(
                    "Appointment reason is required.");

            bool isBooked = appointments.Any(a =>
                a.DoctorId == doctorId &&
                a.AppointmentDateTime == appointmentDateTime &&
                a.Status == AppointmentStatus.Scheduled);

            if (isBooked)
                throw new InvalidOperationException(
                    "Doctor already has a scheduled appointment at this time.");

            string appointmentId = GenerateAppointmentId();

            Appointment appointment = new Appointment(
                appointmentId,
                patientId,
                doctorId,
                appointmentDateTime,
                reason,
                AppointmentStatus.Scheduled);

            appointments.Add(appointment);

            Console.WriteLine(
                $"Appointment booked successfully! ID: {appointmentId}");
        }

        // Display all appointments
        public void DisplayAllAppointments()
        {
            Console.WriteLine("\n=== All Appointments ===");

            if (appointments.Count == 0)
            {
                Console.WriteLine("No appointments found.");
                return;
            }

            foreach (var appointment in appointments
                         .OrderBy(a => a.AppointmentDateTime))
            {
                DisplayAppointmentDetails(appointment);
            }
        }

        // Retrieve appointment by ID
        public void DisplayAppointmentById(string appointmentId)
        {
            Appointment? appointment = appointments.FirstOrDefault(
                a => a.AppointmentId == appointmentId);

            if (appointment == null)
            {
                Console.WriteLine("Appointment not found.");
                return;
            }

            DisplayAppointmentDetails(appointment);
        }

        // Display appointment details
        private void DisplayAppointmentDetails(Appointment appointment)
        {
            Patient? patient = patients.FirstOrDefault(
                p => p.PatientId == appointment.PatientId);

            Doctor? doctor = doctors.FirstOrDefault(
                d => d.Id == appointment.DoctorId);

            Console.WriteLine("--------------------------------");
            Console.WriteLine($"Appointment ID: {appointment.AppointmentId}");
            Console.WriteLine($"Patient: {patient?.PatientName ?? "Unknown"}");
            Console.WriteLine($"Patient ID: {appointment.PatientId}");
            Console.WriteLine($"Doctor: {doctor?.Name ?? "Unknown"}");
            Console.WriteLine($"Doctor ID: {appointment.DoctorId}");
            Console.WriteLine($"Date and Time: {appointment.AppointmentDateTime}");
            Console.WriteLine($"Reason: {appointment.Reason}");
            Console.WriteLine($"Status: {appointment.Status}");
        }

        // Reschedule an appointment
        public void RescheduleAppointment(
            string appointmentId,
            DateTime newDateTime)
        {
            Appointment? appointment = appointments.FirstOrDefault(
                a => a.AppointmentId == appointmentId);

            if (appointment == null)
                throw new InvalidOperationException(
                    "Appointment not found.");

            if (appointment.Status != AppointmentStatus.Scheduled)
                throw new InvalidOperationException(
                    "Only scheduled appointments can be rescheduled.");

            if (newDateTime <= DateTime.Now)
                throw new ArgumentException(
                    "New appointment date and time must be in the future.");

            bool conflict = appointments.Any(a =>
                a.AppointmentId != appointmentId &&
                a.DoctorId == appointment.DoctorId &&
                a.AppointmentDateTime == newDateTime &&
                a.Status == AppointmentStatus.Scheduled);

            if (conflict)
                throw new InvalidOperationException(
                    "Doctor already has an appointment at this time.");

            appointment.AppointmentDateTime = newDateTime;

            Console.WriteLine("Appointment rescheduled successfully.");
        }

        // Cancel an appointment
        public void CancelAppointment(string appointmentId)
        {
            Appointment? appointment = appointments.FirstOrDefault(
                a => a.AppointmentId == appointmentId);

            if (appointment == null)
                throw new InvalidOperationException(
                    "Appointment not found.");

            if (appointment.Status != AppointmentStatus.Scheduled)
                throw new InvalidOperationException(
                    "Only scheduled appointments can be cancelled.");

            appointment.Status = AppointmentStatus.Cancelled;

            Console.WriteLine("Appointment cancelled successfully.");
        }

        // Mark appointment as completed
        public void CompleteAppointment(string appointmentId)
        {
            Appointment? appointment = appointments.FirstOrDefault(
                a => a.AppointmentId == appointmentId);

            if (appointment == null)
                throw new InvalidOperationException(
                    "Appointment not found.");

            if (appointment.Status != AppointmentStatus.Scheduled)
                throw new InvalidOperationException(
                    "Only scheduled appointments can be completed.");

            if (appointment.AppointmentDateTime > DateTime.Now)
                throw new InvalidOperationException(
                    "A future appointment cannot be marked completed.");

            appointment.Status = AppointmentStatus.Completed;

            Console.WriteLine("Appointment marked as completed.");
        }

        // List appointments for a selected patient
        public void DisplayAppointmentsByPatient(string patientId)
        {
            var results = appointments
                .Where(a => a.PatientId == patientId)
                .OrderBy(a => a.AppointmentDateTime)
                .ToList();

            if (results.Count == 0)
            {
                Console.WriteLine("No appointments found for this patient.");
                return;
            }

            foreach (var appointment in results)
                DisplayAppointmentDetails(appointment);
        }

        // List appointments for a selected doctor
        public void DisplayAppointmentsByDoctor(string doctorId)
        {
            var results = appointments
                .Where(a => a.DoctorId == doctorId)
                .OrderBy(a => a.AppointmentDateTime)
                .ToList();

            if (results.Count == 0)
            {
                Console.WriteLine("No appointments found for this doctor.");
                return;
            }

            foreach (var appointment in results)
                DisplayAppointmentDetails(appointment);
        }

        // List appointments on a specified date
        public void DisplayAppointmentsByDate(DateTime date)
        {
            var results = appointments
                .Where(a => a.AppointmentDateTime.Date == date.Date)
                .OrderBy(a => a.AppointmentDateTime)
                .ToList();

            if (results.Count == 0)
            {
                Console.WriteLine("No appointments found on this date.");
                return;
            }

            foreach (var appointment in results)
                DisplayAppointmentDetails(appointment);
        }

        // Filter appointments by status
        public void DisplayAppointmentsByStatus(
            AppointmentStatus status)
        {
            var results = appointments
                .Where(a => a.Status == status)
                .OrderBy(a => a.AppointmentDateTime)
                .ToList();

            if (results.Count == 0)
            {
                Console.WriteLine($"No {status} appointments found.");
                return;
            }

            foreach (var appointment in results)
                DisplayAppointmentDetails(appointment);
        }
    }
}
