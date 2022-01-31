// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PrescriptionVm.cs" company="VVU">
// Copyright (c) VVU. All rights reserved.
// </copyright>
// <summary>
//   The prescription vm.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace HMS.Models.ViewModels
{
    /// <summary>
    /// The prescription view model.
    /// </summary>
    public class PrescriptionVm
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string DoctorId { get; set; }

        /// <summary>
        /// Gets or sets the temperature.
        /// </summary>
        public string Prescription { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        public string Date { get; set; }
    }
}