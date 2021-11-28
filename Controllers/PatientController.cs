// <copyright file="PatientController.cs" company="VVU">
// Copyright (c) VVU. All rights reserved.
// </copyright>

namespace HMS.Controllers
{
    using System.Collections.Generic;
    using AspNetCoreHero.ToastNotification.Abstractions;
    using HMS.Areas.Identity.Data;
    using HMS.Models;
    using HMS.Models.ViewModels;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class PatientController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly INotyfService _notyf;

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientController"/> class.
        /// </summary>
        /// <param name="db">
        /// The database.
        /// </param>
        public PatientController(ApplicationDbContext db, INotyfService notyf)
        {
            this._db = db;
            this._notyf = notyf;
        }

        // GET: PatientController
        public IActionResult Index()
        {
            IEnumerable<Patients> objLisItems = this._db.Patients;

            return this.View(objLisItems);
        }

        // GET: PatientController/Create
        public IActionResult Create()
        {
            return this.View();
        }

        // POST: PatientController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Patients patients)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    this._notyf.Success("Added Successfully.", 10);
                    this._db.Patients.Add(patients);
                    this._db.SaveChanges();
                    return this.RedirectToAction("Index");
                }
            }
            catch
            {
                return this.View("Error");
            }

            return this.View(patients);
        }

        // GET: PatientController/Edit/5
        public IActionResult Edit(int? id)
        {
            var patientVm = new PatientVm()
            {
                Patients = new Patients(),
            };

            if (id is null or 0)
            {
                return this.NotFound();
            }

            patientVm.Patients = this._db.Patients.Find(id);
            if (patientVm.Patients is null)
            {
                return this.NotFound();
            }

            return this.View(patientVm);
        }

        // POST: PatientController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PatientVm obj)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    this._notyf.Success("Edited Successfully.", 10);
                    this._db.Patients.Update(obj.Patients);
                    this._db.SaveChanges();
                    return this.RedirectToAction("Index");
                }

                return this.View(obj);
            }
            catch
            {
                return this.View("Error");
            }
        }

        // GET: PatientController/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id is null or 0)
            {
                return this.NotFound();
            }

            var obj = this._db.Patients.Find(id);
            if (obj == null)
            {
                return this.NotFound();
            }

            return this.View(obj);
        }

        // POST: PatientController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = this._db.Patients.Find(id);
            if (obj is null)
            {
                return this.NotFound();
            }

            this._notyf.Success("Deleted Successfully.", 10);
            this._db.Patients.Remove(obj);
            this._db.SaveChanges();
            return this.RedirectToAction("Index");
        }
    }
}