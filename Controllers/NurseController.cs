﻿// --------------------------------------------------------------------------------------------------------------------
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
        public NurseController(ApplicationDbContext db, INotyfService notifyService)
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
    }
}