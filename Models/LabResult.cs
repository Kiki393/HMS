// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LabResult.cs" company="VVU">
// Copyright (c) VVU. All rights reserved.
// </copyright>
// <summary>
//   The lab result.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace HMS.Models
{
    using System;

    /// <summary>
    /// The lab result.
    /// </summary>
    public class LabResult
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
        /// Gets or sets the predicted label.
        /// </summary>
        public string PredictedLabel { get; set; }

        /// <summary>
        /// Gets or sets the normal accuracy.
        /// </summary>
        public string NormalAccuracy { get; set; }

        /// <summary>
        /// Gets or sets the pneumonia accuracy.
        /// </summary>
        public string PneumoniaAccuracy { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        public DateTime Date { get; set; }
    }
}