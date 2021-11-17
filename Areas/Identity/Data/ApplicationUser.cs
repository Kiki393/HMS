// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApplicationUser.cs" company="">
//
// </copyright>
// <summary>
//   Defines the ApplicationUser type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace HMS.Areas.Identity.Data
{
    using Microsoft.AspNetCore.Identity;

    /// Add profile data for application users by adding properties to the ApplicationUser class
    /// <summary>
    /// The application user.
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }
    }
}