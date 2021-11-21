// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AdminController.cs" company="">
//
// </copyright>
// <summary>
//   Defines the AdminController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace HMS.Controllers
{
    using System.Collections.Generic;

    using HMS.Areas.Identity.Data;
    using HMS.Models;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// The admin controller.
    /// </summary>
    [Authorize]
    public class AdminController : Controller
    {
        /// <summary>
        /// The _db.
        /// </summary>
        private readonly ApplicationDbContext _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminController"/> class.
        /// </summary>
        /// <param name="db">
        /// The db.
        /// </param>
        public AdminController(ApplicationDbContext db)
        {
            this._db = db;
        }

        /// GET: AdminController
        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Index()
        {
            IEnumerable<Patients> obj = this._db.Patients;
            return this.View(obj);
        }
    }
}