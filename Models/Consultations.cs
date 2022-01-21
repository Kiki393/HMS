// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Consultations.cs" company="VVU">
// Copyright (c) VVU. All rights reserved.
// </copyright>
// <summary>
//   The consultations.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace HMS.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The consultations.
    /// </summary>
    public class Consultations
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
        /// Gets or sets the symptoms.
        /// </summary>
        [Required]
        public string Symptoms { get; set; }

        /// <summary>
        /// Gets or sets the diagnosis.
        /// </summary>
        [Required]
        public string Diagnosis { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        public DateTime Date { get; set; }
    }
}