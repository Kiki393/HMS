// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommonResponse.cs" company="VVU">
// Copyright (c) VVU. All rights reserved.
// </copyright>
// <summary>
//   Defines the CommonResponse type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace HMS.Models.ViewModels
{
    /// <summary>
    /// The common response.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public class CommonResponse<T>
    {
        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the dataenum.
        /// </summary>
        public T Dataenum { get; set; }
    }
}