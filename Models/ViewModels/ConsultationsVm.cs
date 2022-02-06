// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsultationsVm.cs" company="VVU">
// Copyright (c) VVU. All rights reserved.
// </copyright>
// <summary>
//   The consultations vm.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace HMS.Models.ViewModels
{
    using System;

    /// <summary>
    /// The consultations vm.
    /// </summary>
    public class ConsultationsVm
    {
        /// <summary>
        /// Gets or sets the symptoms.
        /// </summary>
        public string Symptoms { get; set; }

        /// <summary>
        /// Gets or sets the diagnosis.
        /// </summary>
        public string Diagnosis { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        public DateTime Date { get; set; }
    }
}