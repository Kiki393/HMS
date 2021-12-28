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
    [Authorize]
    public class PatientController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly INotyfService _notyf;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;

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
            _db = db;
            _notyf = notyf;
            _userManager = userManager;
            _emailSender = emailSender;
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
                            Email = patients.Email
                        };
                        _db.Patients.Add(patient);
                        await _db.SaveChangesAsync();

                        var user = new ApplicationUser
                        {
                            UserName = patient.PatientId,
                            Email = patients.Email,
                            Name = patients.Name,
                            Role = "Patient",
                        };

                        var password = Password.Generate(12, 1);
                        var result = await _userManager.CreateAsync(user, password);
                        if (result.Succeeded)
                        {
                            await _userManager.AddToRoleAsync(user, "Patient");
                        }

                        _notyf.Success("Created Successfully.", 10);

                        try
                        {
                            // Emailing reset password link
                            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                            var callbackUrl = Url.Action(
                                "ResetPassword",
                                "Account",
                                new { code },
                                Request.Scheme);

                            await _emailSender.SendEmailAsync(
                                patients.Email,
                                "Account Created",
                                $"Your account has been created on the HMS system. Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a> if you wish. <br>This link would expire in 3 hours. <br> Your username is: " + patient.PatientId + " and your default password is " + password);
                        }
                        catch (Exception e)
                        {
                            _notyf.Error(e.ToString());
                        }
                    }
                    catch (Exception e)
                    {
                        _notyf.Error(e.ToString());
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