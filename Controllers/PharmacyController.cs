// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PharmacyController.cs" company="">
//
// </copyright>
// <summary>
//   Defines the PharmacyController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace HMS.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// The pharmacy controller.
    /// </summary>
    public class PharmacyController : Controller
    {
        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        public IActionResult Index()
        {
            return this.View();
        }
    }
}