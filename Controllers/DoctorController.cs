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

    using Microsoft.AspNetCore.Http;

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
            var list = new List<AssignDoctor>();
            foreach (var item in this._db.AssignDoctors.Where(x => x.DocId == this.loggedInUserId))
            {
                list.Add(item);
            }

            //var patient = (from patients in this._db.Vitals
            //               select new PatientVitals
            //               {
            //                   PatientId = patients.PatientId,
            //                   Bp = patients.Bp,
            //                   Date = patients.Date,
            //                   Temperature = patients.Temperature,
            //                   Weight = patients.Weight
            //               }).ToList();

            return View(list);
        }
    }
}