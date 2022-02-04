// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TransactionsListVm.cs" company="VVU">
// Copyright (c) VVU. All rights reserved.
// </copyright>
// <summary>
//   Defines the TransactionsListVm type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace HMS.Models.ViewModels
{
    using System;

    /// <summary>
    /// The transactions list vm.
    /// </summary>
    public class TransactionsListVm
    {
        /// <summary>
        /// Gets or sets the patient id.
        /// </summary>
        public string PatientId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the total.
        /// </summary>
        public double Total { get; set; }

        /// <summary>
        /// Gets or sets the method.
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        public DateTime Date { get; set; }
    }
}