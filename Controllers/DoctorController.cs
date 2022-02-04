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
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Security.Claims;

    using AspNetCoreHero.ToastNotification.Abstractions;

    using HMS.Areas.Identity.Data;
    using HMS.Models;
    using HMS.Models.ViewModels;

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
        /// The notification.
        /// </summary>
        private readonly INotyfService _notyf;

        /// <summary>
        /// Initializes a new instance of the <see cref="DoctorController"/> class.
        /// </summary>
        /// <param name="db">
        /// The db.
        /// </param>
        /// <param name="httpContextAccessor">
        /// The http context accessor.
        /// </param>
        /// <param name="notyf">
        /// The notification.
        /// </param>
        public DoctorController(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor, INotyfService notyf)
        {
            this._db = db;
            this._notyf = notyf;
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
            TempData["id"] = patientId;
            TempData.Keep();
            ViewData["vitals"] = (from patient in this._db.AssignDoctors
                                  join vitals in this._db.Vitals on patient.PId equals vitals.PatientId
                                  where patient.PId == patientId
                                  select new VitalsVm
                                  {
                                      Bp = vitals.Bp,
                                      Date = vitals.Date,
                                      Weight = vitals.Weight,
                                      Temperature = vitals.Temperature,
                                  }).ToList();

            ViewData["prescription"] = (from patient in this._db.AssignDoctors
                                        join prescription in this._db.Prescriptions on patient.PId equals prescription.PatientId
                                        join docName in this._db.Users on patient.DocId equals docName.Id
                                        where patient.PId == patientId
                                        select new PrescriptionVm
                                        {
                                            Date = prescription.Date.ToString(CultureInfo.CurrentCulture),
                                            DoctorId = docName.Name,
                                            Prescription = prescription.Prescription,
                                        }).ToList();

            ViewData["labResults"] = (from patient in this._db.AssignDoctors
                                      join result in this._db.LabResults on patient.PId equals result.PatientId
                                      where patient.PId == patientId
                                      select new LabResultVm()
                                      {
                                          Date = result.Date,
                                          NormalAccuracy = result.NormalAccuracy,
                                          PneumoniaAccuracy = result.PneumoniaAccuracy,
                                          PredictedLabel = result.PredictedLabel,
                                      }).ToList();
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
        public IActionResult Consult(Consultations patient)
        {
            if (!ModelState.IsValid)
            {
                this._notyf.Error("Something went wrong.");
            }

            try
            {
                var consult = new Consultations
                {
                    PatientId = patient.PatientId,
                    Symptoms = patient.Symptoms,
                    Diagnosis = patient.Diagnosis,
                    Date = System.DateTime.Now,
                };

                this._db.Consultations.Add(consult);
                this._db.SaveChanges();
                this._notyf.Success("Consultation saved.");
            }
            catch (Exception e)
            {
                this._notyf.Error(e.ToString());
            }

            return this.RedirectToAction("Consult", new { patientId = TempData["id"] });
        }

        /// <summary>
        /// The complete consult.
        /// </summary>
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        public IActionResult CompleteConsult()
        {
            try
            {
                var pharmacy = new PharmWaiting
                {
                    PatientId = TempData["id"].ToString()
                };

                var obj = _db.AssignDoctors.First(e => e.PId == pharmacy.PatientId);
                if (obj is null)
                {
                    return NotFound();
                }

                _db.AssignDoctors.Remove(obj);

                this._db.PharmacyWaiting.Add(pharmacy);
                this._db.SaveChanges();
            }
            catch (Exception e)
            {
                this._notyf.Error(e.ToString());
                return this.RedirectToAction("Consult", new { patientId = TempData["id"] });
            }

            return this.RedirectToAction("Index");
        }

        /// <summary>
        /// The forward to lab.
        /// </summary>
        /// <param name="patient">
        /// The patient.
        /// </param>
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        [HttpPost]
        public IActionResult ForwardToLab([FromBody] LabWaiting patient)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _db.LabWaiting.Add(patient);
                    _db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                _notyf.Error(e.ToString());
            }

            return Json(patient);
        }

        /// <summary>
        /// The save prescription.
        /// </summary>
        /// <param name="prescription">
        /// The prescription.
        /// </param>
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        [HttpPost]
        public IActionResult SavePrescription([FromBody] Prescriptions prescription)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var med = new Prescriptions
                    {
                        PatientId = prescription.PatientId,
                        Date = System.DateTime.Now,
                        DoctorId = this.loggedInUserId,
                        Prescription = prescription.Prescription
                    };

                    _db.Prescriptions.Add(med);
                    _db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                _notyf.Error(e.ToString());
            }

            return Json(prescription);
        }
    }
}