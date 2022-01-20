// <copyright file="PatientVitals.cs" company="VVU">
// Copyright (c) VVU. All rights reserved.
// </copyright>

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMS.Models
{
    /// <summary>
    /// The patient vitals.
    /// </summary>
    public class PatientVitals
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
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the temperature.
        /// </summary>
        public double Temperature { get; set; }

        /// <summary>
        /// Gets or sets the weight.
        /// </summary>
        [DisplayName("Weight (Kg)")]
        public double Weight { get; set; }

        /// <summary>
        /// Gets or sets the bp.
        /// </summary>
        [DisplayName("Blood Pressure")]
        public string Bp { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        public string Date { get; set; }
    }
}