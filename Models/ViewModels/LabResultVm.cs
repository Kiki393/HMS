// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LabResultVm.cs" company="VVU">
// Copyright (c) VVU. All rights reserved.
// </copyright>
// <summary>
//   The lab result vm.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace HMS.Models.ViewModels
{
    using System;

    /// <summary>
    /// The lab result view model.
    /// </summary>
    public class LabResultVm
    {
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
