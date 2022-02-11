// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DoctorReferralsVm.cs" company="VVU">
// Copyright (c) VVU. All rights reserved.
// </copyright>
// <summary>
//   The doctor referrals vm.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace HMS.Models.ViewModels
{
    using System;

    /// <summary>
    /// The doctor referrals vm.
    /// </summary>
    public class DoctorReferralsVm
    {
        /// <summary>
        /// Gets or sets the p id.
        /// </summary>
        public string PatientId { get; set; }

        /// <summary>
        /// Gets or sets the p id.
        /// </summary>
        public string PatientName { get; set; }

        /// <summary>
        /// Gets or sets the p id.
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// Gets or sets the severity.
        /// </summary>
        public string Severity { get; set; }

        /// <summary>
        /// Gets or sets the condition.
        /// </summary>
        public string Condition { get; set; }

        /// <summary>
        /// Gets or sets the hospital.
        /// </summary>
        public string Hospital { get; set; }

        /// <summary>
        /// Gets or sets the comments.
        /// </summary>
        public string Comments { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        public DateTime Date { get; set; }
    }
}