// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DoctorController.cs" company="VVU">
// Copyright (c) VVU. All rights reserved.
// </copyright>
// <summary>
//   Defines the DoctorController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HMS.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;

    using HMS.Areas.Identity.Data;
    using HMS.Models;
    using HMS.Models.ViewModels;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;

    /// <summary>
    /// The doctor controller.
    /// </summary>
    [Authorize(Roles = "Doctor")]
    public class DoctorController : Controller
    {
        /// <summary>
        /// The _db.
        /// </summary>
        private readonly ApplicationDbContext _db;

        /// <summary>
        /// The logged in user id.
        /// </summary>
        private readonly string loggedInUserId;

        /// <summary>
        /// Initializes a new instance of the <see cref="DoctorController"/> class.
        /// </summary>
        /// <param name="db">
        /// The db.
        /// </param>
        /// <param name="httpContextAccessor">
        /// The http context accessor.
        /// </param>
        public DoctorController(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            this._db = db;
            this.loggedInUserId = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        public IActionResult Index()
        {
            var waiting = (from patients in _db.AssignDoctors
                           join paId in _db.Patients on patients.PId equals paId.PatientId
                           where patients.DocId == loggedInUserId
                           select new AttendVm() { PatientId = patients.PId, Name = paId.Name }).ToList();

            return View(waiting);
        }

        /// <summary>
        /// The patient details.
        /// </summary>
        /// <param name="patientId">
        /// The patient id.
        /// </param>
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        public IActionResult Consult(string patientId)
        {
            return this.View();
        }

        /// <summary>
        /// The consult.
        /// </summary>
        /// <param name="patient">
        /// The patient.
        /// </param>
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Consult(Consultations patient)
        {
            return this.View();
        }
    }
}