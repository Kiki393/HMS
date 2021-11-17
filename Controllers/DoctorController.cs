// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DoctorController.cs" company="">
//
// </copyright>
// <summary>
//   Defines the DoctorController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace HMS.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// The doctor controller.
    /// </summary>
    public class DoctorController : Controller
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