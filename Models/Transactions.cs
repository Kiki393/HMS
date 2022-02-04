// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Transactions.cs" company="VVU">
// Copyright (c) VVU. All rights reserved.
// </copyright>
// <summary>
//   The transactions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace HMS.Models
{
    using System;

    /// <summary>
    /// The transactions.
    /// </summary>
    public class Transactions
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