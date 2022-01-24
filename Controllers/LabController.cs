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
    using System.Linq;

    using AspNetCoreHero.ToastNotification.Abstractions;

    using HMS.Areas.Identity.Data;
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
        /// The notification.
        /// </summary>
        private readonly INotyfService _notyf;

        /// <summary>
        /// Initializes a new instance of the <see cref="LabController"/> class.
        /// </summary>
        /// <param name="db">
        /// The db.
        /// </param>
        /// <param name="notyf">
        /// The notification.
        /// </param>
        public LabController(ApplicationDbContext db, INotyfService notyf)
        {
            this._db = db;
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
            var waiting = (from patientId in _db.LabWaiting
                           join pId in _db.Patients on patientId.PatientId equals pId.PatientId
                           select new AttendVm() { PatientId = patientId.PatientId, Name = pId.Name }).ToList();

            return View(waiting);
        }
    }
}