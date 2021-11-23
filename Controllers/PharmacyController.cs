// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PharmacyController.cs" company="VVU">
// Copyright (c) VVU. All rights reserved.
// </copyright>
// <summary>
//   Defines the PharmacyController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace HMS.Controllers
{
    using System;
    using System.Collections.Generic;

    using HMS.Areas.Identity.Data;
    using HMS.Models;
    using HMS.Models.ViewModels;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// The pharmacy controller.
    /// </summary>
    [Authorize(Roles = "Pharmacist")]
    public class PharmacyController : Controller
    {
        private readonly ApplicationDbContext _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="PharmacyController"/> class.
        /// </summary>
        /// <param name="db">
        /// The database.
        /// </param>
        public PharmacyController(ApplicationDbContext db)
        {
            this._db = db;
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
            IEnumerable<Medicine> medicines = this._db.Medicines;
            return this.View(medicines);
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
            return this.View();
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
                if (this.ModelState.IsValid)
                {
                    this._db.Medicines.Add(medicine);
                    this._db.SaveChanges();
                    return this.RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                return this.View();
            }

            return this.View();
        }

        /// GET: PharmacyController/Edit
        /// <summary>
        ///
        /// </summary>
        /// <param name="id">
        /// </param>
        /// <returns>
        /// </returns>
        public IActionResult EditMedicine(int? id)
        {
            var medicineVm = new MedicineVm() { Medicine = new Medicine(), };

            if (id is null or 0)
            {
                return this.NotFound();
            }

            medicineVm.Medicine = this._db.Medicines.Find(id);
            if (medicineVm.Medicine is null)
            {
                return this.NotFound();
            }

            return this.View(medicineVm);
        }

        /// POST: PharmacyController/Edit
        /// <summary>
        ///
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditMedicine(MedicineVm obj)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    this._db.Medicines.Update(obj.Medicine);
                    this._db.SaveChanges();
                    return this.RedirectToAction("Index");
                }

                return this.View();
            }
            catch (Exception e)
            {
                return this.View();
            }
        }

        /// GET: PharmacyController/Delete
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Delete(int? id)
        {
            if (id is null or 0)
            {
                return this.NotFound();
            }

            var obj = this._db.Medicines.Find(id);
            if (obj is null)
            {
                return this.NotFound();
            }

            return this.View();
        }

        /// GET: PharmacyController/Delete
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult DeleteMedicine(int? id)
        {
            var obj = this._db.Medicines.Find(id);
            if (obj is null)
            {
                return this.NotFound();
            }

            this._db.Medicines.Remove(obj);
            this._db.SaveChanges();

            return this.RedirectToAction("Index");
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public IActionResult Administer()
        {
            return this.View();
        }
    }
}