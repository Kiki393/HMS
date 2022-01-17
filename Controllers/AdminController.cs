// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AdminController.cs" company="VVU">
// Copyright (c) VVU. All rights reserved.
// </copyright>
// <summary>
//   Defines the AdminController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using HMS.Areas.Identity.Data;
using HMS.Count;
using HMS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HMS.Controllers
{
    using System;

    using AspNetCoreHero.ToastNotification.Abstractions;

    /// <summary>
    /// The admin controller.
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        /// <summary>
        /// The _db.
        /// </summary>
        private readonly ApplicationDbContext _db;

        private readonly INotyfService _notyf;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminController"/> class.
        /// </summary>
        /// <param name="db">
        /// The db.
        /// </param>
        /// <param name="notyf">
        /// The notification.
        /// </param>
        public AdminController(ApplicationDbContext db, INotyfService notyf)
        {
            _db = db;
            this._notyf = notyf;
        }

        /// GET: AdminController
        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public IActionResult Index()
        {
            var obj = new CountInTables
            {
                Patients = _db.Patients.ToList(),
                ConfirmedAppointments = _db.Appointments.Where(x => x.IsDoctorApproved == true).ToList(),
                PendingAppointments = _db.Appointments.Where(x => x.IsDoctorApproved == false).ToList(),
                Medicines = _db.Medicines.ToList(),
                UsersD = this._db.Users.Where(x => x.Role == "Doctor").ToList(),
                UsersN = this._db.Users.Where(x => x.Role == "Nurse").ToList(),
                UsersC = this._db.Users.Where(x => x.Role == "Cashier").ToList(),
                UsersL = this._db.Users.Where(x => x.Role == "Lab Technician").ToList(),
                UsersP = this._db.Users.Where(x => x.Role == "Pharmacist").ToList(),
                UsersR = this._db.Users.Where(x => x.Role == "Receptionist").ToList(),
            };

            return View(obj);
        }

        /// <summary>
        /// The add announcement.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public IActionResult AddAnnouncement()
        {
            return View();
        }

        /// <summary>
        /// The add announcement.
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddAnnouncement(Announcements model)
        {
            if (model.End >= DateTime.Now.Date)
            {
                _db.Announcements.Add(model);
                _db.SaveChanges();
                return RedirectToAction("ListOfAnnouncement");
            }
            else
            {
                this._notyf.Information("Enter a date that is not today.");
            }

            return View(model);
        }

        /// List of Announcement
        /// <summary>
        /// The list of announcement.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public IActionResult ListOfAnnouncement()
        {
            var list = _db.Announcements.ToList();
            return View(list);
        }

        /// Edit Announcement
        /// <summary>
        /// The edit announcement.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public IActionResult EditAnnouncement(int id)
        {
            var announcement = _db.Announcements.Find(id);
            if (announcement is null)
            {
                return this.NotFound();
            }

            return View(announcement);
        }

        /// <summary>
        /// The edit announcement.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditAnnouncement(int id, Announcements model)
        {
            var announcement = _db.Announcements.Find(id);
            if (model.End >= DateTime.Now.Date)
            {
                announcement.Announcement = model.Announcement;
                announcement.End = model.End;
                announcement.For = model.For;
                _db.SaveChanges();
                return RedirectToAction("ListOfAnnouncement");
            }
            else
            {
                this._notyf.Information("Enter a date that is not today.");
            }

            return View(announcement);
        }

        /// Delete Announcement
        /// <summary>
        /// The delete announcement.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public IActionResult DeleteAnnouncement(int? id)
        {
            if (id is null or 0)
            {
                return NotFound();
            }

            var obj = _db.Announcements.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        /// <summary>
        /// The delete announcement.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteAnnouncement(int id)
        {
            var announcement = _db.Announcements.Find(id);
            if (announcement is null)
            {
                return this.NotFound();
            }

            _db.Announcements.Remove(announcement);
            _db.SaveChanges();
            this._notyf.Success("Announcement Deleted.");
            return RedirectToAction("ListOfAnnouncement");
        }

        public IActionResult ListOfMessages()
        {
            var messages = _db.Messages.ToList();
            return View(messages);
        }

        public IActionResult EditMessage(int id)
        {
            var messages = _db.Messages.Find(id);
            return View(messages);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditMessage(int id, Messages model)
        {
            var messages = _db.Messages.Find(id);
            messages.Message = model.Message;
            messages.Reply = model.Reply;
            _db.SaveChanges();
            this._notyf.Success("Reply Sent.");
            return RedirectToAction("ListOfMessages");
        }

        public IActionResult DeleteMessage()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteMessage(int id)
        {
            var messages = _db.Messages.Find(id);
            if (messages is null)
            {
                return this.NotFound();
            }

            _db.Messages.Remove(messages);
            _db.SaveChanges();
            this._notyf.Success("Message Deleted.");
            return RedirectToAction("ListOfMessages");
        }
    }
}