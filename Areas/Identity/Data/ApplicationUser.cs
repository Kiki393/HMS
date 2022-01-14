// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApplicationUser.cs" company="VVU">
// Copyright (c) VVU. All rights reserved.
// </copyright>
// <summary>
//   Defines the ApplicationUser type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.AspNetCore.Identity;

namespace HMS.Areas.Identity.Data
{
    using System.ComponentModel;

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

        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// Gets or sets the staff id.
        /// </summary>
        [DisplayName("Staff Id")]
        public string StaffId { get; set; }

        /// <summary>
        /// Gets or sets the profile picture.
        /// </summary>
        public byte[] ProfilePicture { get; set; }

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        public string Gender { get; set; }
    }
}