// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Announcement.cs" company="VVU">
// Copyright (c) VVU. All rights reserved.
// </copyright>
// <summary>
//   Defines the Announcement type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace HMS.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The announcement.
    /// </summary>
    public class Announcements
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the announcements.
        /// </summary>
        [Required]
        public string Announcement { get; set; }

        /// <summary>
        /// Gets or sets the announcement for.
        /// </summary>
        [Required]
        [Display(Name = "Announcement For")]
        public string For { get; set; }

        /// <summary>
        /// Gets or sets the end.
        /// </summary>
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? End { get; set; }
    }
}