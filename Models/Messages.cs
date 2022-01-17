// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Messages.cs" company="VVU">
// Copyright (c) VVU. All rights reserved.
// </copyright>
// <summary>
//   Defines the Messages type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace HMS.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The messages.
    /// </summary>
    public class Messages
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        [Required]
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the reply.
        /// </summary>
        public string Reply { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        public DateTime? Date { get; set; }
    }
}