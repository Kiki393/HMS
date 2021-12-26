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
    using HMS.Areas.Identity.Data;

    using Microsoft.AspNetCore.Identity;

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
        /// The _sign in manager.
        /// </summary>
        private readonly SignInManager<ApplicationUser> _signInManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ManageController"/> class.
        /// </summary>
        /// <param name="userManager">
        /// The user manager.
        /// </param>
        /// <param name="signInManager">
        /// The sign in manager.
        /// </param>
        public ManageController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        public IActionResult Index()
        {
            return View();
        }
    }
}