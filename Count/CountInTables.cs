// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CountInTables.cs" company="VVU">
// Copyright (c) VVU. All rights reserved.
// </copyright>
// <summary>
//   The count in tables.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HMS.Areas.Identity.Data;
using HMS.Models;
using Microsoft.AspNetCore.Identity;

namespace HMS.Count
{
    /// <summary>
    /// The count in tables.
    /// </summary>
    public class CountInTables
    {
        /// <summary>
        /// Gets or sets the patients.
        /// </summary>
        public IEnumerable<Patients> Patients { get; set; }

        /// <summary>
        /// Gets or sets the medicines.
        /// </summary>
        public IEnumerable<Medicine> Medicines { get; set; }

        /// <summary>
        /// Gets or sets the confirmed appointments.
        /// </summary>
        public IEnumerable<Appointment> ConfirmedAppointments { get; set; }

        /// <summary>
        /// Gets or sets the pending appointments.
        /// </summary>
        public IEnumerable<Appointment> PendingAppointments { get; set; }

        /// <summary>
        /// Gets or sets the application users.
        /// </summary>
        public IEnumerable<ApplicationUser> UsersD { get; set; }

        /// <summary>
        /// Gets or sets the application users.
        /// </summary>
        public IEnumerable<ApplicationUser> UsersN { get; set; }

        /// <summary>
        /// Gets or sets the application users.
        /// </summary>
        public IEnumerable<ApplicationUser> UsersP { get; set; }

        /// <summary>
        /// Gets or sets the application users.
        /// </summary>
        public IEnumerable<ApplicationUser> UsersL { get; set; }

        /// <summary>
        /// Gets or sets the application users.
        /// </summary>
        public IEnumerable<ApplicationUser> UsersR { get; set; }

        /// <summary>
        /// Gets or sets the application users.
        /// </summary>
        public IEnumerable<ApplicationUser> UsersC { get; set; }
    }
}