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
        private readonly ApplicationDbContext _db;

        private readonly INotyfService _notifyService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReceptionistController"/> class.
        /// </summary>
        /// <param name="db">
        /// </param>
        /// <param name="notifyService"></param>
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

        [HttpPost]
        public IActionResult SendResult([FromBody] Test patientData)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _db.Tests.Add(patientData);
                    _db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return Json(patientData);
        }
    }
}