// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NurseController.cs" company="">
//
// </copyright>
// <summary>
//   The nurse controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace HMS.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// The nurse controller.
    /// </summary>
    public class NurseController : Controller
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