// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Referrals.cs" company="VVU">
// Copyright (c) VVU. All rights reserved.
// </copyright>
// <summary>
//   Defines the Referrals type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace HMS.Models
{
    using System;

    using Microsoft.CodeAnalysis.CSharp.Syntax;

    /// <summary>
    /// The referrals.
    /// </summary>
    public class Referrals
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the doc id.
        /// </summary>
        public string DoctorId { get; set; }

        /// <summary>
        /// Gets or sets the p id.
        /// </summary>
        public string PatientId { get; set; }

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