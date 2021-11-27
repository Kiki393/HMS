// <copyright file="Medicine.cs" company="VVU">
// Copyright (c) VVU. All rights reserved.
// </copyright>

namespace HMS.Models
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class Medicine
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Range(1.0, double.MaxValue, ErrorMessage = "Value must be more than 0.")]
        public double Cost { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [DisplayName("NHIS (Yes or No)")]
        public bool NHIS { get; set; }
    }
}