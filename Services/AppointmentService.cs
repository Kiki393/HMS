// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppointmentService.cs" company="VVU">
// Copyright (c) VVU. All rights reserved.
// </copyright>
// <summary>
//   Defines the AppointmentService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HMS.Areas.Identity.Data;
using HMS.Models;
using HMS.Models.ViewModels;
using HMS.Utilities;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace HMS.Services
{
    /// <summary>
    /// The appointment service.
    /// </summary>
    public class AppointmentService : IAppointmentService
    {
        /// <summary>
        /// The _database.
        /// </summary>
        private readonly ApplicationDbContext _db;

        /// <summary>
        /// The _email sender.
        /// </summary>
        private readonly IEmailSender _emailSender;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppointmentService"/> class.
        /// The appointment service.
        /// </summary>
        /// <param name="db">
        /// The database.
        /// </param>
        /// <param name="emailSender">
        /// The email Sender.
        /// </param>
        public AppointmentService(ApplicationDbContext db, IEmailSender emailSender)
        {
            _db = db;
            _emailSender = emailSender;
        }

        /// <summary>
        /// The add update.
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<int> AddUpdate(AppointmentVm model)
        {
            var startDate = DateTime.Parse(model.StartDate);
            var endDate = DateTime.Parse(model.StartDate).AddMinutes(Convert.ToDouble(model.Duration));

            var patient = _db.Users.FirstOrDefault(u => u.Id == model.PatientId);
            var doctor = _db.Users.FirstOrDefault(u => u.Id == model.DoctorId);

            if (model is { Id: > 0 })
            {
                // update
                var appointment = _db.Appointments.FirstOrDefault(x => x.Id == model.Id);
                if (appointment != null)
                {
                    appointment.Title = model.Title;
                    appointment.Description = model.Description;
                    appointment.StartDate = startDate;
                    appointment.EndDate = endDate;
                    appointment.Duration = model.Duration;
                    appointment.DoctorId = model.DoctorId;
                    appointment.PatientId = model.PatientId;
                    appointment.IsDoctorApproved = false;
                    appointment.AdminId = model.AdminId;
                }

                await _db.SaveChangesAsync();

                return 1;
            }
            else
            {
                // create
                var appointment = new Appointment
                {
                    Title = model.Title,
                    Description = model.Description,
                    StartDate = startDate,
                    EndDate = endDate,
                    Duration = model.Duration,
                    DoctorId = model.DoctorId,
                    PatientId = model.PatientId,
                    IsDoctorApproved = false,
                    AdminId = model.AdminId,
                };

                // Sends and email to both doctor and patient on the status of an appointment
                // await this._emailSender.SendEmailAsync(
                //    doctor.Email,
                //    "Appointment Created",
                //    $"Your appointment with {patient.Name} has been created and is pending confirmation.");
                // await this._emailSender.SendEmailAsync(
                //    patient.Email,
                //    "Appointment Created",
                //    $"Your appointment with {doctor.Name} has been created and is pending confirmation.");

                _db.Appointments.Add(appointment);
                await _db.SaveChangesAsync();

                return 2;
            }
        }

        /// <summary>
        /// The confirm event.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<int> ConfirmEvent(int id)
        {
            var appointment = _db.Appointments.FirstOrDefault(x => x.Id == id);
            if (appointment != null)
            {
                appointment.IsDoctorApproved = true;
                return await _db.SaveChangesAsync();
            }

            return 0;
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<int> Delete(int id)
        {
            var appointment = _db.Appointments.FirstOrDefault(x => x.Id == id);
            if (appointment != null)
            {
                _db.Appointments.Remove(appointment);
                return await _db.SaveChangesAsync();
            }

            return 0;
        }

        /// <summary>
        /// The doctors events by id.
        /// </summary>
        /// <param name="doctorId">
        /// The doctor id.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public List<AppointmentVm> DoctorsEventsById(string doctorId)
        {
            return _db.Appointments.Where(x => x.DoctorId == doctorId).ToList().Select(c => new AppointmentVm
            {
                Id = c.Id,
                Description = c.Description,
                StartDate = c.StartDate.ToString("yyyy-MM-dd HH:mm:ss"),
                EndDate = c.EndDate.ToString("yyyy-MM-dd HH:mm:ss"),
                Title = c.Title,
                Duration = c.Duration,
                IsDoctorApproved = c.IsDoctorApproved,
            }).ToList();
        }

        /// <summary>
        /// The get by id.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="AppointmentVm"/>.
        /// </returns>
        public AppointmentVm GetById(int id)
        {
            return _db.Appointments.Where(x => x.Id == id).ToList().Select(c => new AppointmentVm
            {
                Id = c.Id,
                Description = c.Description,
                StartDate = c.StartDate.ToString("yyyy-MM-dd HH:mm:ss"),
                EndDate = c.EndDate.ToString("yyyy-MM-dd HH:mm:ss"),
                Title = c.Title,
                Duration = c.Duration,
                IsDoctorApproved = c.IsDoctorApproved,
                PatientId = c.PatientId,
                DoctorId = c.DoctorId,
                PatientName = _db.Users.Where(x => x.Id == c.PatientId).Select(x => x.Name).FirstOrDefault(),
                DoctorName = _db.Users.Where(x => x.Id == c.DoctorId).Select(x => x.Name).FirstOrDefault(),
            }).SingleOrDefault();
        }

        /// <summary>
        /// The get doctor list.
        /// </summary>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public List<DoctorVm> GetDoctorList()
        {
            var doctors = (from user in _db.Users
                           join userRoles in _db.UserRoles on user.Id equals userRoles.UserId
                           join roles in _db.Roles.Where(x => x.Name == UserRoles.Doctor) on userRoles.RoleId equals roles.Id
                           select new DoctorVm
                           {
                               Id = user.Id,
                               Name = user.Name,
                           }).ToList();

            return doctors;
        }

        /// <summary>
        /// The get patient list.
        /// </summary>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public List<Patients> GetPatientList()
        {
            var patients = (from patient in _db.Patients
                            orderby patient.Name
                            select new Patients
                            {
                                Id = patient.Id,
                                Name = patient.Name,
                            }).ToList();

            return patients;
        }

        /// <summary>
        /// The patients events by id.
        /// </summary>
        /// <param name="patientId">
        /// The patient id.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public List<AppointmentVm> PatientsEventsById(string patientId)
        {
            return _db.Appointments.Where(x => x.PatientId == patientId).ToList().Select(c => new AppointmentVm
            {
                Id = c.Id,
                Description = c.Description,
                StartDate = c.StartDate.ToString("yyyy-MM-dd HH:mm:ss"),
                EndDate = c.EndDate.ToString("yyyy-MM-dd HH:mm:ss"),
                Title = c.Title,
                Duration = c.Duration,
                IsDoctorApproved = c.IsDoctorApproved,
            }).ToList();
        }
    }
}