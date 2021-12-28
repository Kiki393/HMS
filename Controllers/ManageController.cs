// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ManageController.cs" company="VVU">
//   Copyright (c) VVU. All rights reserved.
// </copyright>
// <summary>
//
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HMS.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AspNetCoreHero.ToastNotification.Abstractions;

    using HMS.Areas.Identity.Data;
    using HMS.Models.ViewModels;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// The manage controller.
    /// </summary>
    [Authorize]
    public class ManageController : Controller
    {
        /// <summary>
        /// The _user manager.
        /// </summary>
        private readonly UserManager<ApplicationUser> _userManager;

        /// <summary>
        /// The _db.
        /// </summary>
        private readonly ApplicationDbContext _db;

        /// <summary>
        /// The notification.
        /// </summary>
        private readonly INotyfService _notyf;

        /// <summary>
        /// Initializes a new instance of the <see cref="ManageController"/> class.
        /// </summary>
        /// <param name="userManager">
        /// The user manager.
        /// </param>
        /// <param name="db">
        /// The db.
        /// </param>
        /// <param name="notyf">
        /// The notification.
        /// </param>
        public ManageController(UserManager<ApplicationUser> userManager, ApplicationDbContext db, INotyfService notyf)
        {
            this._userManager = userManager;
            this._db = db;
            this._notyf = notyf;
        }

        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        public IActionResult Users()
        {
            IEnumerable<ApplicationUser> application = this._db.Users;

            return View(application);
        }

        /// <summary>
        /// The delete user.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await this._userManager.FindByIdAsync(id);

            if (user == null)
            {
                this._notyf.Error($"User with Id = {id} cannot be found.");
                return View("Error");
            }
            else
            {
                var result = await this._userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    this._notyf.Success("User Deleted");
                    return this.RedirectToAction("Users");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return this.View("Users");
        }

        //public async Task<IActionResult> changeEmail()
        //{
        //    return this.View();
        //}
    }
}