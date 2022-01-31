// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PayWaiting.cs" company="VVU">
// Copyright (c) VVU. All rights reserved.
// </copyright>
// <summary>
//   The pay waiting.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace HMS.Models
{
    /// <summary>
    /// The pay waiting.
    /// </summary>
    public class PayWaiting
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
    }
}