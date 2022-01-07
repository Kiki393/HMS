// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ChangePasswordVm.cs" company="VVU">
// Copyright (c) VVU. All rights reserved.
// </copyright>
// <summary>
//   Defines the ChangePasswordVm type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace HMS.Models.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The change password view model.
    /// </summary>
    public class ChangePasswordVm
    {
        /// <summary>
        /// Gets or sets the current password.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current Password")]
        public string CurrentPassword { get; set; }

        /// <summary>
        /// Gets or sets the new password.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        /// <summary>
        /// Gets or sets the confirm password.
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm New Password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}