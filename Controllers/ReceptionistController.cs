// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReceptionistController.cs" company="VVU">
// Copyright (c) VVU. All rights reserved.
// </copyright>
// <summary>
//   Defines the ReceptionistController type.
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
    /// <summary>
    /// The receptionist controller.
    /// </summary>
    [Authorize(Roles = "Receptionist")]
    public class ReceptionistController : Controller
    {
        /// <summary>
        /// The _db.
        /// </summary>
        private readonly ApplicationDbContext _db;

        /// <summary>
        /// The _notify service.
        /// </summary>
        private readonly INotyfService _notifyService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReceptionistController"/> class.
        /// </summary>
        /// <param name="db">
        /// The database.
        /// </param>
        /// <param name="notifyService">
        /// The notification service.
        /// </param>
        public ReceptionistController(ApplicationDbContext db, INotyfService notifyService)
        {
            _db = db;
            _notifyService = notifyService;
        }

        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        public IActionResult Index()
        {
            IEnumerable<Patients> obj = _db.Patients;
            return View(obj);
        }

        /// <summary>
        /// The send result.
        /// </summary>
        /// <param name="patientData">
        /// The patient data.
        /// </param>
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        [HttpPost]
        public IActionResult SendResult([FromBody] PatientVitals patientData)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _db.Vitals.Add(patientData);
                    _db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                _notifyService.Error(e.ToString());
            }

            return Json(patientData);
        }
    }
}