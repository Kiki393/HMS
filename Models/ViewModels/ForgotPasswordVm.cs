// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ForgotPasswordVm.cs" company="VVU">
// </copyright>
// <summary>
//   The forgot password view model.
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