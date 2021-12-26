// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProfileVm.cs" company="VVU">
//   Copyright (c) VVU. All rights reserved.
// </copyright>
// <summary>
//   The profile view model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace HMS.Models.ViewModels
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The profile view model.
    /// </summary>
    public class ProfileVm
    {
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        [DisplayName("Phone number")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the profile picture.
        /// </summary>
        [DisplayName("Profile Picture")]
        public byte[] ProfilePicture { get; set; }
    }
}