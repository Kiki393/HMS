// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CashierController.cs" company="VVU">
// Copyright (c) VVU. All rights reserved.
// </copyright>
// <summary>
//   Defines the CashierController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HMS.Controllers
{
    using System;
    using System.Linq;

    using AspNetCoreHero.ToastNotification.Abstractions;

    using HMS.Areas.Identity.Data;
    using HMS.Models;
    using HMS.Models.ViewModels;

    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// The cashier controller.
    /// </summary>
    [Authorize(Roles = "Cashier")]
    public class CashierController : Controller
    {
        /// <summary>
        /// The _db.
        /// </summary>
        private readonly ApplicationDbContext _db;

        /// <summary>
        /// The notification.
        /// </summary>
        private readonly INotyfService _notyf;

        /// <summary>
        /// Initializes a new instance of the <see cref="CashierController"/> class.
        /// </summary>
        /// <param name="notyf">
        /// The notification.
        /// </param>
        /// <param name="db">
        /// The db.
        /// </param>
        public CashierController(INotyfService notyf, ApplicationDbContext db)
        {
            this._notyf = notyf;
            this._db = db;
        }

        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        public IActionResult Index()
        {
            var waiting = (from patientId in _db.PayWaiting
                           join pId in _db.Patients on patientId.PatientId equals pId.PatientId
                           select new AttendVm() { PatientId = patientId.PatientId, Name = pId.Name }).ToList();
            return View(waiting);
        }

        /// <summary>
        /// The payment.
        /// </summary>
        /// <param name="patientId">
        /// The patient Id.
        /// </param>
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        public IActionResult Payment(string patientId)
        {
            TempData["id"] = patientId;
            TempData["details"] = (from patients in _db.PayWaiting
                                   join transactions in this._db.Transactions on patients.PatientId equals transactions.PatientId
                                   join paId in _db.Patients on patients.PatientId equals paId.PatientId
                                   where patients.PatientId == patientId && transactions.Date.Date == DateTime.Today.Date
                                   select new TransactionsVm()
                                   {
                                       Id = transactions.Id,
                                       Total = transactions.Total,
                                   }).ToList();

            return this.View();
        }

        /// <summary>
        /// The payment.
        /// </summary>
        /// <param name="transaction">
        /// The transaction.
        /// </param>
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Payment(Transactions transaction)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var payment = new Transactions
                    {
                        Date = System.DateTime.Now,
                        Method = transaction.Method,
                        PatientId = transaction.PatientId,
                        Total = transaction.Total,
                        Id = transaction.Id
                    };

                    var obj = _db.PayWaiting.First(e => e.PatientId == payment.PatientId);
                    if (obj is null)
                    {
                        return NotFound();
                    }

                    _db.PayWaiting.Remove(obj);

                    this._db.Entry(payment).State = EntityState.Modified;
                    this._db.SaveChanges();
                    this._notyf.Success("Transaction Saved.");

                    return this.RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                this._notyf.Error(e.ToString());
                return this.RedirectToAction("Payment", new { patientId = TempData["id"] });
            }

            return this.RedirectToAction("Payment", new { patientId = TempData["id"] });
        }

        /// <summary>
        /// The transaction history.
        /// </summary>
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        public IActionResult TransactionHistory()
        {
            var transactions = (from patientId in _db.Transactions
                                join pId in _db.Patients on patientId.PatientId equals pId.PatientId
                                select new TransactionsListVm()
                                {
                                    PatientId = patientId.PatientId,
                                    Name = pId.Name,
                                    Method = patientId.Method,
                                    Date = patientId.Date,
                                    Total = patientId.Total
                                }).ToList();
            return this.View(transactions);
        }
    }
}