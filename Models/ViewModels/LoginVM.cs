// <copyright file="LoginVM.cs" company="VVU">
// Copyright (c) VVU. All rights reserved.
// </copyright>

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HMS.Models.ViewModels
{
    /// <summary>
    /// The login view model.
    /// </summary>
    public class LoginVm
    {
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether remember me.
        /// </summary>
        [DisplayName("Remember Me?")]
        public bool RememberMe { get; set; }
    }
}