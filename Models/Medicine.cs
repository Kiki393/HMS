// <copyright file="Medicine.cs" company="VVU">
// Copyright (c) VVU. All rights reserved.
// </copyright>

namespace HMS.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Medicine
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public double Cost { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Display(Name = "NHIS (Yes or No)")]
        public bool NHIS { get; set; }
    }
}