// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountController.cs" company="VVU">
// Copyright (c) VVU. All rights reserved.
// </copyright>
// <summary>
//   Defines the AccountController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Web;
using AspNetCoreHero.ToastNotification.Abstractions;
using HMS.Areas.Identity.Data;
using HMS.Models;
using HMS.Models.ViewModels;
using HMS.Password_Generator;
using HMS.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace HMS.Controllers
{
    using HMS.Random_Number_Generator;

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

        /// <summary>
        /// The notification.
        /// </summary>
        private readonly INotyfService _notyf;

        /// <summary>
        /// The _email sender.
        /// </summary>
        private readonly IEmailSender _emailSender;

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
        /// <param name="emailSender">
        /// The email Sender.
        /// </param>
        public AccountController(ApplicationDbContext db, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, INotyfService notyf, IEmailSender emailSender)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _notyf = notyf;
            _emailSender = emailSender;
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
                    Role = model.RoleName,
                    StaffId = "STFID" + RandomNumber.RandomNum(1000, 9000),
                };

                var password = Password.Generate(12, 1);
                var result = await _userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, model.RoleName);
                    _notyf.Success("Account Created.");

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
                            model.Email,
                            "Account Created",
                            $"Your account has been created on the HMS system. Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a> if you wish. <br>This link would expire in 3 hours. <br> Your username is: " + model.Username + " and your default password is: " + password);
                    }
                    catch (Exception e)
                    {
                        _notyf.Error(e.ToString());
                    }

                    if (User.IsInRole(UserRoles.Admin))
                    {
                        TempData["newAdminSignUp"] = user.Name;
                        return RedirectToAction("Index", "Admin");
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

        /// GET: /Account/ForgotPassword
        /// <summary>
        /// The forgot password.
        /// </summary>
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        public IActionResult ForgotPassword()
        {
            return View();
        }

        /// POST: /Account/ForgotPassword
        /// <summary>
        /// The forgot password.
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordVm model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                // code = HttpUtility.UrlEncode(code);
                try
                {
                    var callbackUrl = Url.Action(
                        "ResetPassword",
                        "Account",
                        new { code },
                        Request.Scheme);

                    await _emailSender.SendEmailAsync(
                        model.Email,
                        "Forgot Password",
                        $"You requested to reset your password. If you initiated this request, ignore this email. \nPlease reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>. \nThis link would expire in 3 hours");
                }
                catch (Exception e)
                {
                    _notyf.Error(e.ToString());
                }

                return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        /// GET: /Account/ForgotPasswordConfirmation
        /// <summary>
        /// The forgot password confirmation.
        /// </summary>
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        /// GET: /Account/ResetPassword
        /// <summary>
        /// The reset password.
        /// </summary>
        /// <param name="code">
        /// The code.
        /// </param>
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        public IActionResult ResetPassword(string code)
        {
            if (code != null)
            {
                var resetPass = new ResetPasswordVm
                {
                    Code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code)),

                    // Code = HttpUtility.UrlDecode(code),
                };

                return View(resetPass);
            }
            else
            {
                return BadRequest("A code must be supplied for password reset.");
            }
        }

        /// POST: /Account/ResetPassword
        /// <summary>
        /// The reset password.
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordVm model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    // Don't reveal that the user does not exist
                    return RedirectToAction("ResetPasswordConfirmation");
                }

                var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("ResetPasswordConfirmation");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        /// GET: /Account/ResetPasswordConfirmation
        /// <summary>
        /// The reset password confirmation.
        /// </summary>
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }
    }
}