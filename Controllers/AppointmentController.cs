﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppointmentController.cs" company="">
//
// </copyright>
// <summary>
//   Defines the AppointmentController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace HMS.Controllers
{
    using HMS.Services;
    using HMS.Utilities;

    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// The appointment controller.
    /// </summary>
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
            this._appointmentService = appointmentService;
        }

        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        public IActionResult Index()
        {
            this.ViewBag.DocList = this._appointmentService.GetDoctorList();
            this.ViewBag.PatientList = this._appointmentService.GetPatientList();
            this.ViewBag.Duration = DropDowns.GetTimeDropDown();
            return this.View();
        }
    }
}