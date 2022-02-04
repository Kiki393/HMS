// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TransactionsVm.cs" company="VVU">
// Copyright (c) VVU. All rights reserved.
// </copyright>
// <summary>
//   The transactions vm.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace HMS.Models.ViewModels
{
    /// <summary>
    /// The transactions view model.
    /// </summary>
    public class TransactionsVm
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Gets or sets the total.
        /// </summary>
        public double Total { get; set; }
    }
}