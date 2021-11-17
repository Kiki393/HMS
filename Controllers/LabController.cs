// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LabController.cs" company="">
//
// </copyright>
// <summary>
//   The lab controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace HMS.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// The lab controller.
    /// </summary>
    public class LabController : Controller
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