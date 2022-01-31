// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsultingVm.cs" company="VVU">
// Copyright (c) VVU. All rights reserved.
// </copyright>
// <summary>
//   The consulting view model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace HMS.Models.ViewModels
{
    /// <summary>
    /// The consulting view model.
    /// </summary>
    public class VitalsVm
    {
        /// <summary>
        /// Gets or sets the temperature.
        /// </summary>
        public double Temperature { get; set; }

        /// <summary>
        /// Gets or sets the weight.
        /// </summary>
        public double Weight { get; set; }

        /// <summary>
        /// Gets or sets the bp.
        /// </summary>
        public string Bp { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        public string Date { get; set; }
    }
}