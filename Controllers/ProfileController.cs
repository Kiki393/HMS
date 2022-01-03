using Microsoft.AspNetCore.Mvc;

namespace HMS.Controllers
{
    using System.Collections.Specialized;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using AspNetCoreHero.ToastNotification.Abstractions;

    using HMS.Areas.Identity.Data;
    using HMS.Models.ViewModels;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;

    /// <summary>
    /// The profile controller.
    /// </summary>
    public class ProfileController : Controller
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
        /// The notification.
        /// </summary>
        private readonly INotyfService _notyf;

        /// <summary>
        /// The _db.
        /// </summary>
        private readonly ApplicationDbContext _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileController"/> class.
        /// </summary>
        /// <param name="userManager">
        /// The user manager.
        /// </param>
        /// <param name="signInManager">
        /// The sign in manager.
        /// </param>
        /// <param name="notyf">
        /// The notification.
        /// </param>
        /// <param name="db">
        /// The db.
        /// </param>
        public ProfileController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, INotyfService notyf, ApplicationDbContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _notyf = notyf;
            _db = db;
        }

        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        public async Task<IActionResult> Index()
        {
            var username = User.Identity.Name;

            // Fetch the user profile
            var user = _db.Users.FirstOrDefault(u => u.UserName.Equals(username));

            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var name = user.Name;
            var profilePicture = user.ProfilePicture;

            var profile = new ProfileVm()
            {
                PhoneNumber = phoneNumber,
                Name = name,
                Username = userName,
                ProfilePicture = profilePicture
            };
            return View(profile);
        }

        /// <summary>
        /// The index.
        /// </summary>
        /// <param name="profile">
        /// The profile.
        /// </param>
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> Index(ProfileVm profile)
        {
            var user = await _userManager.GetUserAsync(User);

            if (!ModelState.IsValid)
            {
                return View(profile);
            }

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (profile.Name != user.Name)
            {
                user.Name = profile.Name;
                await _userManager.UpdateAsync(user);
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (profile.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, profile.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    _notyf.Error("Unexpected error when trying to set phone number.");
                    return View();
                }
            }

            if (Request.Form.Files.Count > 0)
            {
                IFormFile file = Request.Form.Files.FirstOrDefault();
                using (var dataStream = new MemoryStream())
                {
                    await file.CopyToAsync(dataStream);
                    user.ProfilePicture = dataStream.ToArray();
                }

                await _userManager.UpdateAsync(user);
            }

            await _signInManager.RefreshSignInAsync(user);
            _notyf.Success("Your profile has been updated");

            return View(profile);
        }
    }
}