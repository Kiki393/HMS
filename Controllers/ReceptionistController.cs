// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReceptionistController.cs" company="VVU">
// Copyright (c) VVU. All rights reserved.
// </copyright>
// <summary>
//   Defines the ReceptionistController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace HMS.Controllers
{
    using System.Collections.Generic;

    using HMS.Areas.Identity.Data;
    using HMS.Models;
    using HMS.Models.ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    /// <summary>
    /// The receptionist controller.
    /// </summary>
    [Authorize(Roles = "Receptionist")]
    public class ReceptionistController : Controller
    {
        private readonly ApplicationDbContext _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReceptionistController"/> class.
        /// </summary>
        /// <param name="db">
        /// </param>
        public ReceptionistController(ApplicationDbContext db)
        {
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
            IEnumerable<Patients> obj = this._db.Patients;
            return this.View(obj);
        }
    }
}