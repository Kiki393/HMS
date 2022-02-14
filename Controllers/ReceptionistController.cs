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
    using System.Linq;

    using HMS.Models.ViewModels;

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
        private readonly INotyfService _notyf;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReceptionistController"/> class.
        /// </summary>
        /// <param name="db">
        /// The database.
        /// </param>
        /// <param name="notyf">
        /// The notification service.
        /// </param>
        public ReceptionistController(ApplicationDbContext db, INotyfService notyf)
        {
            _db = db;
            this._notyf = notyf;
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

            ViewData["announcements"] = (from announcement in this._db.Announcements
                                         select new AnnouncementsVm()
                                         {
                                             Announcement = announcement.Announcement,
                                             For = announcement.For,
                                         }).ToList();

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
        public IActionResult SendResult([FromBody] VitalsWaiting patientData)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _db.Waiting.Add(patientData);
                    _db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                this._notyf.Error(e.ToString());
            }

            return Json(patientData);
        }

        /// <summary>
        /// The referrals history.
        /// </summary>
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        public IActionResult ReferralsHistory()
        {
            var referrals = (from refer in _db.Referrals
                             join patients in _db.Patients on refer.PatientId equals patients.PatientId
                             join docName in this._db.Users on refer.DoctorId equals docName.Id
                             where patients.PatientId == refer.PatientId
                             select new ReferralsVm()
                             {
                                 Hospital = refer.Hospital,
                                 Severity = refer.Severity,
                                 Comments = refer.Comments,
                                 Condition = refer.Condition,
                                 Date = refer.Date,
                                 Gender = patients.Gender,
                                 PatientId = refer.PatientId,
                                 PatientName = patients.Name,
                                 DoctorName = docName.Name,
                                 Contact = patients.Contact,
                             }).ToList();

            return this.View(referrals);
        }
    }
}