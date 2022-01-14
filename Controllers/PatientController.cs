// <copyright file="PatientController.cs" company="VVU">
// Copyright (c) VVU. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using HMS.Areas.Identity.Data;
using HMS.Models;
using HMS.Models.ViewModels;
using HMS.Password_Generator;
using HMS.Random_Number_Generator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace HMS.Controllers
{
    /// <summary>
    /// The patient controller.
    /// </summary>
    [Authorize]
    public class PatientController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly INotyfService notyf;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IEmailSender emailSender;

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientController"/> class.
        /// </summary>
        /// <param name="db">
        /// The database.
        /// </param>
        /// <param name="notyf">
        /// Notification.
        /// </param>
        /// <param name="userManager">
        /// The user manager.
        /// </param>
        /// <param name="emailSender">
        /// Email sender.
        /// </param>
        public PatientController(ApplicationDbContext db, INotyfService notyf, UserManager<ApplicationUser> userManager, IEmailSender emailSender)
        {
            this.db = db;
            this.notyf = notyf;
            this.userManager = userManager;
            this.emailSender = emailSender;
        }

        /// GET: PatientController
        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public IActionResult Index()
        {
            IEnumerable<Patients> objLisItems = db.Patients;

            return View(objLisItems);
        }

        /// GET: PatientController/Create
        /// <summary>
        /// The create.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public IActionResult Create()
        {
            return View();
        }

        /// POST: PatientController/Create
        /// <summary>
        /// The create.
        /// </summary>
        /// <param name="patients">
        /// THe patients model.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Patients patients)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        var patient = new Patients
                        {
                            PatientId = "PID" + RandomNumber.RandomNum(1000, 9000),
                            Name = patients.Name,
                            Age = patients.Age,
                            Address = patients.Address,
                            Contact = patients.Contact,
                            Dob = patients.Dob,
                            Email = patients.Email,
                            Nhis = patients.Nhis,
                            Gender = patients.Gender
                        };
                        db.Patients.Add(patient);
                        await db.SaveChangesAsync();

                        var user = new ApplicationUser
                        {
                            UserName = patient.PatientId,
                            Email = patients.Email,
                            Name = patients.Name,
                            Role = "Patient",
                            Gender = patients.Gender
                        };

                        var password = Password.Generate(12, 1);
                        var result = await userManager.CreateAsync(user, password);
                        if (result.Succeeded)
                        {
                            await userManager.AddToRoleAsync(user, "Patient");
                        }

                        notyf.Success("Created Successfully.", 10);

                        try
                        {
                            // Emailing reset password link
                            var code = await userManager.GeneratePasswordResetTokenAsync(user);
                            var callbackUrl = Url.Action(
                                "ResetPassword",
                                "Account",
                                new { email = patients.Email, code },
                                Request.Scheme);

                            await emailSender.SendEmailAsync(
                                patients.Email,
                                "Account Created",
                                $"Your account has been created on the HMS system. Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a> if you wish. <br>This link would expire in 3 hours. <br> Your username is: " + patient.PatientId + " and your default password is " + password);
                        }
                        catch (Exception e)
                        {
                            notyf.Error(e.ToString());
                        }
                    }
                    catch (Exception e)
                    {
                        notyf.Error(e.ToString());
                    }

                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View("Error");
            }

            return View(patients);
        }

        /// GET: PatientController/Edit/5
        /// <summary>
        /// The edit.
        /// </summary>
        /// <param name="id">
        /// THe patient id.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
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

            patientVm.Patients = db.Patients.Find(id);
            if (patientVm.Patients is null)
            {
                return NotFound();
            }

            return View(patientVm);
        }

        /// POST: PatientController/Edit/5
        /// <summary>
        /// The edit.
        /// </summary>
        /// <param name="obj">
        /// THe patient view model.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PatientVm obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Patients.Update(obj.Patients);
                    db.SaveChanges();
                    notyf.Success("Record updated.");
                    return RedirectToAction("Index");
                }

                notyf.Error("Sorry, something went wrong.");
                return View(obj);
            }
            catch (Exception e)
            {
                notyf.Error(e.ToString());
                return View(obj);
            }
        }

        /// GET: PatientController/Delete/5
        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="id">
        /// THe patients id.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public IActionResult Delete(int? id)
        {
            if (id is null or 0)
            {
                return NotFound();
            }

            var obj = db.Patients.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        /// POST: PatientController/Delete/5
        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="id">
        /// THe patients id.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = db.Patients.Find(id);
            if (obj is null)
            {
                return NotFound();
            }

            notyf.Success("Deleted Successfully.", 10);
            db.Patients.Remove(obj);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}