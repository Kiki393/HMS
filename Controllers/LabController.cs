// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LabController.cs" company="VVU">
// Copyright (c) VVU. All rights reserved.
// </copyright>
// <summary>
//   The lab controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HMS.Controllers
{
    using System.Collections.Generic;
    using System.Linq;

    using HMS.Areas.Identity.Data;
    using HMS.Models;
    using HMS.Models.ViewModels;

    /// <summary>
    /// The lab controller.
    /// </summary>
    [Authorize(Roles = "Lab Technician")]
    public class LabController : Controller
    {
        /// <summary>
        /// The _db.
        /// </summary>
        private readonly ApplicationDbContext _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="LabController"/> class.
        /// </summary>
        /// <param name="db">
        /// The db.
        /// </param>
        public LabController(ApplicationDbContext db)
        {
            this._db = db;
        }

        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        public IActionResult Index()
        {
            var waiting = (from patientId in _db.LabWaiting
                           join pId in _db.Patients on patientId.PatientId equals pId.PatientId
                           select new AttendVm() { PatientId = patientId.PatientId, Name = pId.Name }).ToList();

            ViewData["announcements"] = (from announcement in this._db.Announcements
                                         select new AnnouncementsVm()
                                         {
                                             Announcement = announcement.Announcement,
                                             For = announcement.For,
                                         }).ToList();

            return View(waiting);
        }

        /// <summary>
        /// The lab history.
        /// </summary>
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        public IActionResult LabHistory()
        {
            IEnumerable<LabResult> obj = _db.LabResults;
            return this.View(obj);
        }
    }
}