// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppUsersVm.cs" company="VVU">
// Copyright (c) VVU. All rights reserved.
// </copyright>
// <summary>
//   The app users view model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace HMS.Models.ViewModels
{
    using System.Collections.Generic;

    /// <summary>
    /// The app users view model.
    /// </summary>
    public class AppUsersVm
    {
        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the roles.
        /// </summary>
        public string Roles { get; set; }
    }
}