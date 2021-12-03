// <copyright file="PatientController.cs" company="VVU">
// Copyright (c) VVU. All rights reserved.
// </copyright>

using System.Collections.Generic;
using AspNetCoreHero.ToastNotification.Abstractions;
using HMS.Areas.Identity.Data;
using HMS.Models;
using HMS.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HMS.Controllers
{
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
            _db = db;
            _notyf = notyf;
        }

        // GET: PatientController
        public IActionResult Index()
        {
            IEnumerable<Patients> objLisItems = _db.Patients;

            return View(objLisItems);
        }

        // GET: PatientController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PatientController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Patients patients)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _notyf.Success("Added Successfully.", 10);
                    _db.Patients.Add(patients);
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View("Error");
            }

            return View(patients);
        }

        // GET: PatientController/Edit/5
        public IActionResult Edit(int? id)
        {
            var patientVm = new PatientVm
            {
                Patients = new Patients(),
            };

            if (id is null or 0)
            {
                return NotFound();
            }

            patientVm.Patients = _db.Patients.Find(id);
            if (patientVm.Patients is null)
            {
                return NotFound();
            }

            return View(patientVm);
        }

        // POST: PatientController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PatientVm obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _notyf.Success("Edited Successfully.", 10);
                    _db.Patients.Update(obj.Patients);
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(obj);
            }
            catch
            {
                return View("Error");
            }
        }

        // GET: PatientController/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id is null or 0)
            {
                return NotFound();
            }

            var obj = _db.Patients.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        // POST: PatientController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Patients.Find(id);
            if (obj is null)
            {
                return NotFound();
            }

            _notyf.Success("Deleted Successfully.", 10);
            _db.Patients.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}