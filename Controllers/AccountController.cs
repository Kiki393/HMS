// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountController.cs" company="">
//
// </copyright>
// <summary>
//   Defines the AccountController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace HMS.Controllers
{
    using System.Threading.Tasks;

    using HMS.Areas.Identity.Data;
    using HMS.Models.ViewModels;
    using HMS.Utilities;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// The account controller.
    /// </summary>
    public class AccountController : Controller
    {
        /// <summary>
        /// The db.
        /// </summary>
        private readonly ApplicationDbContext db;

        /// <summary>
        /// The user manager.
        /// </summary>
        private readonly UserManager<ApplicationUser> userManager;

        /// <summary>
        /// The sign in manager.
        /// </summary>
        private readonly SignInManager<ApplicationUser> signInManager;

        /// <summary>
        /// The role manager.
        /// </summary>
        private readonly RoleManager<IdentityRole> roleManager;

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
        public AccountController(ApplicationDbContext db, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            this.db = db;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }

        /// <summary>
        /// The login.
        /// </summary>
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        public IActionResult Login()
        {
            return this.View();
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
            switch (this.ModelState.IsValid)
            {
                case true:
                    {
                        var result = await this.signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                        if (result.Succeeded)
                        {
                            var user = await this.userManager.FindByNameAsync(model.Email);
                            this.HttpContext.Session.SetString("ssuserName", user.Name);
                            var roles = await this.userManager.GetRolesAsync(user);
                            if (roles.Contains("Admin"))
                            {
                                return this.RedirectToAction("Index", "Admin");
                            }
                            else if (roles.Contains("Nurse"))
                            {
                                return this.RedirectToAction("Index", "Nurse");
                            }
                            else if (roles.Contains("Doctor"))
                            {
                                return this.RedirectToAction("Index", "Doctor");
                            }
                            else if (roles.Contains("Receptionist"))
                            {
                                return this.RedirectToAction("Index", "Receptionist");
                            }
                            else if (roles.Contains("Lab Technician"))
                            {
                                return this.RedirectToAction("Index", "Lab");
                            }
                            else if (roles.Contains("Pharmacist"))
                            {
                                return this.RedirectToAction("Index", "Pharmacy");
                            }
                            else if (roles.Contains("Cashier"))
                            {
                                return this.RedirectToAction("Index", "Cashier");
                            }
                            else
                            {
                                return this.RedirectToAction("Index", "Home");
                            }
                        }
                        else
                        {
                            this.ModelState.AddModelError(string.Empty, "Email or Password is incorrect.");
                        }

                        break;
                    }
            }

            return this.View(model);
        }

        /// <summary>
        /// The register.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<IActionResult> Register()
        {
            if (!this.roleManager.RoleExistsAsync(UserRoles.Admin).GetAwaiter().GetResult())
            {
                await this.roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                await this.roleManager.CreateAsync(new IdentityRole(UserRoles.Doctor));
                await this.roleManager.CreateAsync(new IdentityRole(UserRoles.Nurse));
                await this.roleManager.CreateAsync(new IdentityRole(UserRoles.Cashier));
                await this.roleManager.CreateAsync(new IdentityRole(UserRoles.Lab));
                await this.roleManager.CreateAsync(new IdentityRole(UserRoles.Pharmacy));
                await this.roleManager.CreateAsync(new IdentityRole(UserRoles.Receptionist));
            }

            return this.View();
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
            if (this.ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    UserName = model.Username,
                    Email = model.Email,
                    Name = model.Name
                };

                var result = await this.userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await this.userManager.AddToRoleAsync(user, model.RoleName);
                    if (!this.User.IsInRole(UserRoles.Admin))
                    {
                        await this.signInManager.SignInAsync(user, isPersistent: false);
                    }
                    else
                    {
                        this.TempData["newAdminSignUp"] = user.Name;
                    }
                    return this.RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        this.ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return this.View(model);
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
            await this.signInManager.SignOutAsync();
            return this.RedirectToAction("Login", "Account");
        }
    }
}