// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NurseController.cs" company="VVU">
// Copyright (c) VVU. All rights reserved.
// </copyright>
// <summary>
//   The nurse controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using AspNetCoreHero.ToastNotification.Abstractions;
using HMS.Areas.Identity.Data;
using HMS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HMS.Controllers
{
    using System.Linq;

    using HMS.Services;

    /// <summary>
    /// The nurse controller.
    /// </summary>
    [Authorize(Roles = "Nurse")]
    public class NurseController : Controller
    {
        /// <summary>
        /// The _db.
        /// </summary>
        private readonly ApplicationDbContext _db;

        /// <summary>
        /// The appointment service.
        /// </summary>
        private readonly IAppointmentService _appointmentService;

        /// <summary>
        /// The _notify service.
        /// </summary>
        private readonly INotyfService _notyf;

        /// <summary>
        /// Initializes a new instance of the <see cref="NurseController"/> class.
        /// </summary>
        /// <param name="db">
        /// The database.
        /// </param>
        /// <param name="notyf">
        /// The notification service.
        /// </param>
        /// <param name="appointmentService">
        /// The appointment Service.
        /// </param>
        public NurseController(ApplicationDbContext db, INotyfService notyf, IAppointmentService appointmentService)
        {
            _db = db;
            this._notyf = notyf;
            _appointmentService = appointmentService;
        }

        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        public IActionResult Index()
        {
            ViewBag.DocList = _appointmentService.GetDoctorList();
            var waiting = (from patientId in _db.Waiting
                           join pId in _db.Patients on patientId.PatientId equals pId.PatientId
                           orderby DateTime.Now descending
                           select new VitalsWaiting { PatientId = patientId.PatientId, Name = pId.Name }).ToList();
            return View(waiting);
        }

        /// <summary>
        /// Save vitals.
        /// </summary>
        /// <param name="vitals">
        /// The vitals.
        /// </param>
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        [HttpPost]
        public IActionResult SaveVitals([FromBody] PatientVitals vitals)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _db.Vitals.Add(vitals);
                    _db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                this._notyf.Error(e.ToString());
            }

            return Json(vitals);
        }

        /// <summary>
        /// The assign doctor.
        /// </summary>
        /// <param name="doctor">
        ///     The doctor.
        /// </param>
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        [HttpPost]
        public IActionResult AssignDoc([FromBody] AssignDoctor doctor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var obj = _db.Waiting.First(e => e.PatientId == doctor.PId);
                    if (obj is null)
                    {
                        return NotFound();
                    }

                    _db.Waiting.Remove(obj);

                    _db.AssignDoctors.Add(doctor);
                    _db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                this._notyf.Error(e.ToString());
            }

            return Json(doctor);
        }

        /// <summary>
        /// The vitals.
        /// </summary>
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        public IActionResult Vitals()
        {
            IEnumerable<PatientVitals> vitals = _db.Vitals;
            return View(vitals);
        }
    }
}