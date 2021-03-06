// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Prescriptions.cs" company="VVU">
// Copyright (c) VVU. All rights reserved.
// </copyright>
// <summary>
//   The prescriptions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace HMS.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The prescriptions.
    /// </summary>
    public class Prescriptions
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the patient id.
        /// </summary>
        public string PatientId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string DoctorId { get; set; }

        /// <summary>
        /// Gets or sets the temperature.
        /// </summary>
        [Required]
        public string Prescription { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        public DateTime Date { get; set; }
    }
}