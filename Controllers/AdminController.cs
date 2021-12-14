// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AdminController.cs" company="VVU">
// Copyright (c) VVU. All rights reserved.
// </copyright>
// <summary>
//   Defines the AdminController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using HMS.Areas.Identity.Data;
using HMS.Count;
using HMS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HMS.Controllers
{
    /// <summary>
    /// The admin controller.
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        /// <summary>
        /// The _db.
        /// </summary>
        private readonly ApplicationDbContext _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminController"/> class.
        /// </summary>
        /// <param name="db">
        /// The db.
        /// </param>
        public AdminController(ApplicationDbContext db)
        {
            _db = db;
        }

        /// GET: AdminController
        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Index()
        {
            var obj = new CountInTables
            {
                Patients = _db.Patients.ToList(),
                ConfirmedAppointments = _db.Appointments.Where(x => x.IsDoctorApproved == true).ToList(),
                PendingAppointments = _db.Appointments.Where(x => x.IsDoctorApproved == false).ToList(),
                Medicines = _db.Medicines.ToList(),

                // ApplicationUsers = (IEnumerable<ApplicationUser>)_db.UserRoles.Where(x => x.RoleId == "905afffd-5a73-4b46-8c82-90f375b47993").ToList(),
            };

            return View(obj);
        }
    }
}