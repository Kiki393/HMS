// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountController.cs" company="VVU">
// Copyright (c) VVU. All rights reserved.
// </copyright>
// <summary>
//   Defines the AccountController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using HMS.Areas.Identity.Data;
using HMS.Models;
using HMS.Models.ViewModels;
using HMS.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HMS.Controllers
{
    /// <summary>
    /// The account controller.
    /// </summary>
    public class AccountController : Controller
    {
        /// <summary>
        /// The toast notification.
        /// </summary>
        private readonly ApplicationDbContext _db;

        /// <summary>
        /// The user manager.
        /// </summary>
        private readonly UserManager<ApplicationUser> _userManager;

        /// <summary>
        /// The sign in manager.
        /// </summary>
        private readonly SignInManager<ApplicationUser> _signInManager;

        /// <summary>
        /// The role manager.
        /// </summary>
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly INotyfService _notyf;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="db">
        /// The database.
        /// </param>
        /// <param name="userManager">
        /// The user manager.
        /// </param>
        /// <param name="signInManager">
        /// The sign in manager.
        /// </param>
        /// <param name="roleManager">
        /// The role manager.
        /// </param>
        /// <param name="notyf">
        /// Toast Notification.
        /// </param>
        public AccountController(ApplicationDbContext db, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, INotyfService notyf)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _notyf = notyf;
        }

        /// <summary>
        /// The login.
        /// </summary>
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        public IActionResult Login()
        {
            return View();
        }

        /// Post - Login
        /// <summary>
        /// The login.
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVm model)
        {
            switch (ModelState.IsValid)
            {
                case true:
                    {
                        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                        if (result.Succeeded)
                        {
                            _notyf.Success("Log in Successful.");
                            var user = await _userManager.FindByNameAsync(model.Email);
                            HttpContext.Session.SetString("ssuserName", user.Name);
                            var roles = await _userManager.GetRolesAsync(user);
                            if (roles.Contains("Admin"))
                            {
                                return RedirectToAction("Index", "Admin");
                            }

                            if (roles.Contains("Nurse"))
                            {
                                return RedirectToAction("Index", "Nurse");
                            }

                            if (roles.Contains("Doctor"))
                            {
                                return RedirectToAction("Index", "Doctor");
                            }

                            if (roles.Contains("Receptionist"))
                            {
                                return RedirectToAction("Index", "Receptionist");
                            }

                            if (roles.Contains("Lab Technician"))
                            {
                                return RedirectToAction("Index", "Lab");
                            }

                            if (roles.Contains("Pharmacist"))
                            {
                                return RedirectToAction("Index", "Pharmacy");
                            }

                            if (roles.Contains("Cashier"))
                            {
                                return RedirectToAction("Index", "Cashier");
                            }

                            if (roles.Contains("Patient"))
                            {
                                return RedirectToAction("Index", "Patient");
                            }

                            return RedirectToAction("Index", "Home");
                        }

                        ModelState.AddModelError(string.Empty, "Email or Password is incorrect.");

                        break;
                    }
            }

            return View(model);
        }

        /// <summary>
        /// The register.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<IActionResult> Register()
        {
            if (!_roleManager.RoleExistsAsync(UserRoles.Admin).GetAwaiter().GetResult())
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Doctor));
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Nurse));
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Cashier));
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Lab));
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Pharmacy));
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Receptionist));
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Patient));
            }

            return View();
        }

        /// Post - Register
        /// <summary>
        /// The register.
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVm model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Username,
                    Email = model.Email,
                    Name = model.Name,
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, model.RoleName);
                    _notyf.Success("Account Created.");
                    if (!User.IsInRole(UserRoles.Admin))
                    {
                        var patient = new Patients { ApplicationUserId = user.Id, Name = model.Name, Email = model.Email };
                        _db.Add(patient);
                        await _db.SaveChangesAsync();
                        await _signInManager.SignInAsync(user, isPersistent: false);
                    }
                    else
                    {
                        TempData["newAdminSignUp"] = user.Name;
                    }

                    return RedirectToAction("Login", "Account");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        /// POST - Log out
        /// <summary>
        /// The log out.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}