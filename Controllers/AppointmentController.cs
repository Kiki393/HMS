// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppointmentController.cs" company="VVU">
// Copyright (c) VVU. All rights reserved.
// </copyright>
// <summary>
//   Defines the AppointmentController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using HMS.Services;
using HMS.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HMS.Controllers
{
    /// <summary>
    /// The appointment controller.
    /// </summary>
    [Authorize]
    public class AppointmentController : Controller
    {
        /// <summary>
        /// The appointment service.
        /// </summary>
        private readonly IAppointmentService _appointmentService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppointmentController"/> class.
        /// </summary>
        /// <param name="appointmentService">
        /// The appointment service.
        /// </param>
        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        public IActionResult Index()
        {
            ViewBag.DocList = _appointmentService.GetDoctorList();
            ViewBag.PatientList = _appointmentService.GetPatientList();
            ViewBag.Duration = DropDowns.GetTimeDropDown();
            return View();
        }
    }
}