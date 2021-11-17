// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CashierController.cs" company="">
//
// </copyright>
// <summary>
//   Defines the CashierController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace HMS.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// The cashier controller.
    /// </summary>
    public class CashierController : Controller
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