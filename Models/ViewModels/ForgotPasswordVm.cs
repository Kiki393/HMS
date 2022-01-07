// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ForgotPasswordVm.cs" company="VVU">
// Copyright (c) VVU. All rights reserved.
// </copyright>
// <summary>
//   Defines the RandomNumber type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace HMS.Models.ViewModels
{
    /// <summary>
    /// The forgot password view model.
    /// </summary>
    public class ForgotPasswordVm
    {
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}