// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CashierController.cs" company="VVU">
// Copyright (c) VVU. All rights reserved.
// </copyright>
// <summary>
//   Defines the CashierController type.
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
    /// The cashier controller.
    /// </summary>
    [Authorize(Roles = "Cashier")]
    public class CashierController : Controller
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
        /// Initializes a new instance of the <see cref="CashierController"/> class.
        /// </summary>
        /// <param name="notyf">
        /// The notification.
        /// </param>
        /// <param name="db">
        /// The db.
        /// </param>
        public CashierController(INotyfService notyf, ApplicationDbContext db)
        {
            this._notyf = notyf;
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
            var waiting = (from patientId in _db.PharmacyWaiting
                           join pId in _db.Patients on patientId.PatientId equals pId.PatientId
                           select new AttendVm() { PatientId = patientId.PatientId, Name = pId.Name }).ToList();
            return View(waiting);
        }
    }
}