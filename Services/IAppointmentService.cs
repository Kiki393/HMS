// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAppointmentService.cs" company="VVU">
// Copyright (c) VVU. All rights reserved.
// </copyright>
// <summary>
//   Defines the IAppointmentService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using HMS.Models;
using HMS.Models.ViewModels;

namespace HMS.Services
{
    /// <summary>
    /// The AppointmentService interface.
    /// </summary>
    public interface IAppointmentService
    {
        /// <summary>
        /// The get doctor list.
        /// </summary>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public List<DoctorVm> GetDoctorList();

        /// <summary>
        /// The get patient list.
        /// </summary>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public List<Patients> GetPatientList();

        /// <summary>
        /// The add update.
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<int> AddUpdate(AppointmentVm model);

        /// <summary>
        /// The doctors events by id.
        /// </summary>
        /// <param name="doctorId">
        /// The doctor id.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public List<AppointmentVm> DoctorsEventsById(string doctorId);

        /// <summary>
        /// The patients events by id.
        /// </summary>
        /// <param name="patientId">
        /// The patient id.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public List<AppointmentVm> PatientsEventsById(string patientId);

        /// <summary>
        /// The get by id.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="AppointmentVm"/>.
        /// </returns>
        public AppointmentVm GetById(int id);

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<int> Delete(int id);

        /// <summary>
        /// The confirm event.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<int> ConfirmEvent(int id);
    }
}