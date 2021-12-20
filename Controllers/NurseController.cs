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
        private readonly INotyfService _notifyService;

        /// <summary>
        /// Initializes a new instance of the <see cref="NurseController"/> class.
        /// </summary>
        /// <param name="db">
        /// The database.
        /// </param>
        /// <param name="notifyService">
        /// The notification service.
        /// </param>
        /// <param name="appointmentService">
        /// The appointment Service.
        /// </param>
        public NurseController(ApplicationDbContext db, INotyfService notifyService, IAppointmentService appointmentService)
        {
            _db = db;
            _notifyService = notifyService;
            this._appointmentService = appointmentService;
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
            IEnumerable<PatientVitals> vitals = _db.Vitals;
            return View(vitals);
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
                    _db.Vitals.Update(vitals);
                    _db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                _notifyService.Error(e.ToString());
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
                    this._db.AssignDoctors.Add(doctor);
                    this._db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                _notifyService.Error(e.ToString());
            }

            return Json(doctor);
        }
    }
}