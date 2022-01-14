// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RegisterVM.cs" company="VVU">
// Copyright (c) VVU. All rights reserved.
// </copyright>
// <summary>
//   Defines the RegisterVm type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HMS.Models.ViewModels
{
    /// <summary>
    /// The register view model.
    /// </summary>
    public class RegisterVm
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        [Required]
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the role name.
        /// </summary>
        [Required]
        [DisplayName("Role")]
        public string RoleName { get; set; }

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        public string Gender { get; set; }
    }
}