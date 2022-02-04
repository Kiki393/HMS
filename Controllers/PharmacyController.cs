// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PharmacyController.cs" company="VVU">
// Copyright (c) VVU. All rights reserved.
// </copyright>
// <summary>
//   Defines the PharmacyController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using HMS.Areas.Identity.Data;
using HMS.Models;
using HMS.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HMS.Controllers
{
    using System.Linq;

    using AspNetCoreHero.ToastNotification.Abstractions;

    /// <summary>
    /// The pharmacy controller.
    /// </summary>
    [Authorize(Roles = "Pharmacist")]
    public class PharmacyController : Controller
    {
        private readonly ApplicationDbContext _db;

        private readonly INotyfService _notyf;

        /// <summary>
        /// Initializes a new instance of the <see cref="PharmacyController"/> class.
        /// </summary>
        /// <param name="db">
        /// The database.
        /// </param>
        /// <param name="notyf">
        /// The notification service.
        /// </param>
        public PharmacyController(ApplicationDbContext db, INotyfService notyf)
        {
            _db = db;
            _notyf = notyf;
        }

        /// GET: PharmacyController/
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
                           orderby DateTime.Now descending
                           select new AttendVm() { PatientId = patientId.PatientId, Name = pId.Name }).ToList();
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
        public IActionResult Dispense(string patientId)
        {
            TempData["id"] = patientId;
            TempData.Keep();
            ViewData["details"] = (from patient in this._db.PharmacyWaiting
                                   join prescription in this._db.Prescriptions on patient.PatientId equals prescription.PatientId
                                   where patient.PatientId == patientId && prescription.Date.Date == DateTime.Today.Date
                                   select new PrescriptionVm
                                   {
                                       Prescription = prescription.Prescription,
                                   }).ToList();
            return this.View();
        }

        /// <summary>
        /// The complete dispense.
        /// </summary>
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        public IActionResult CompleteDispense(Transactions transaction)
        {
            try
            {
                var payment = new Transactions
                {
                    PatientId = TempData["id"].ToString(),
                    Total = transaction.Total,
                    Date = System.DateTime.Now,
                };

                this._db.Transactions.Add(payment);
                this._db.SaveChanges();
            }
            catch (Exception e)
            {
                this._notyf.Error(e.ToString());
            }

            try
            {
                var pay = new PayWaiting
                {
                    PatientId = TempData["id"].ToString()
                };

                var obj = _db.PharmacyWaiting.First(e => e.PatientId == pay.PatientId);
                if (obj is null)
                {
                    return NotFound();
                }

                _db.PharmacyWaiting.Remove(obj);

                this._db.PayWaiting.Add(pay);
                this._db.SaveChanges();
            }
            catch (Exception e)
            {
                this._notyf.Error(e.ToString());
            }

            return this.RedirectToAction("Index");
        }

        /// GET: PharmacyController/
        /// <summary>
        /// The medicines.
        /// </summary>
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        public IActionResult Medicines()
        {
            IEnumerable<Medicine> medicines = _db.Medicines;
            return View(medicines);
        }

        /// GET: PharmacyController/Create
        /// <summary>
        /// The add medicine page.
        /// </summary>
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        public IActionResult AddMedicine()
        {
            return View();
        }

        /// POST: PharmacyController/Create
        /// <summary>
        /// Add Medicine.
        /// </summary>
        /// <param name="medicine">
        /// The medicine model.
        /// </param>
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddMedicine(Medicine medicine)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _db.Medicines.Add(medicine);
                    _db.SaveChanges();
                    _notyf.Success("Medicine Added");
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                _notyf.Error(e.ToString());
                return View();
            }

            return View();
        }

        /// GET: PharmacyController/Edit
        /// <summary>
        /// Edit Medicine.
        /// </summary>
        /// <param name="id">
        /// The medicine id.
        /// </param>
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        public IActionResult EditMedicine(int? id)
        {
            var medicineVm = new MedicineVm { Medicine = new Medicine(), };

            if (id is null or 0)
            {
                return NotFound();
            }

            medicineVm.Medicine = _db.Medicines.Find(id);
            if (medicineVm.Medicine is null)
            {
                return NotFound();
            }

            return View(medicineVm);
        }

        /// POST: PharmacyController/Edit
        /// <summary>
        /// Edit Medicine.
        /// </summary>
        /// <param name="obj">
        /// The medicine view model.
        /// </param>
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditMedicine(MedicineVm obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _db.Medicines.Update(obj.Medicine);
                    _db.SaveChanges();
                    _notyf.Success("Medicine Updated");
                    return RedirectToAction("Index");
                }

                return View();
            }
            catch (Exception e)
            {
                _notyf.Error(e.ToString());
                return View();
            }
        }

        /// GET: PharmacyController/Delete
        /// <summary>
        /// Delete medicine.
        /// </summary>
        /// <param name="id">
        /// The medicine id.
        /// </param>
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        public IActionResult Delete(int? id)
        {
            if (id is null or 0)
            {
                return NotFound();
            }

            var obj = _db.Medicines.Find(id);
            if (obj is null)
            {
                return NotFound();
            }

            return View();
        }

        /// POST: PharmacyController/Delete
        /// <summary>
        /// Delete medicine.
        /// </summary>
        /// <param name="id">
        /// The medicine id.
        /// </param>
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteMedicine(int? id)
        {
            var obj = _db.Medicines.Find(id);
            if (obj is null)
            {
                return NotFound();
            }

            _db.Medicines.Remove(obj);
            _db.SaveChanges();
            _notyf.Success("Medicine Deleted");

            return RedirectToAction("Index");
        }
    }
}