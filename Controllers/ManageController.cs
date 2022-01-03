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
        /// The _sign in manager.
        /// </summary>
        private readonly SignInManager<ApplicationUser> _signInManager;

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
        /// <param name="signInManager">
        /// The sign In Manager.
        /// </param>
        public ManageController(UserManager<ApplicationUser> userManager, ApplicationDbContext db, INotyfService notyf, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _db = db;
            _notyf = notyf;
            _signInManager = signInManager;
        }

        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        public IActionResult Users()
        {
            IEnumerable<ApplicationUser> application = _db.Users;

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
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                _notyf.Error($"User with Id = {id} cannot be found.");
                return View("Error");
            }
            else
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    _notyf.Success("User Deleted");
                    return RedirectToAction("Users");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View("Users");
        }

        /// <summary>
        /// The change password.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public IActionResult ChangePassword()
        {
            return View();
        }

        /// <summary>
        /// The change password.
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordVm model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                // ChangePasswordAsync changes the user password
                var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

                // The new password did not meet the complexity rules or
                // the current password is incorrect. Add these errors to
                // the ModelState and re render ChangePassword view
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }

                    return View();
                }

                // Upon successfully changing the password refresh sign-in cookie
                _notyf.Success("Password Updated");
                await _signInManager.RefreshSignInAsync(user);
                return View();
            }

            return View(model);
        }
    }
}