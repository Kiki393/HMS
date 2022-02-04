// <copyright file="Medicine.cs" company="VVU">
// Copyright (c) VVU. All rights reserved.
// </copyright>

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HMS.Models
{
    /// <summary>
    /// The medicine.
    /// </summary>
    public class Medicine
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the cost.
        /// </summary>
        [Required]
        [Range(1.0, double.MaxValue, ErrorMessage = "Value must be more than 0.")]
        public double Cost { get; set; }

        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        [Required]
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether nhis.
        /// </summary>
        [Required]
        [DisplayName("NHIS (Yes or No)")]
        public string NHIS { get; set; }
    }
}