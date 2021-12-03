// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PatientVM.cs" company="VVU">
// Copyright (c) VVU. All rights reserved.
// </copyright>
// <summary>
//   Defines the PatientVm type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace HMS.Models.ViewModels
{
    /// <summary>
    /// The patient view model.
    /// </summary>
    public class PatientVm
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int? Id { get; set; }

        public string PatientId { get; init; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        public string Dob { get; init; }

        public int Age { get; init; }

        public string Contact { get; init; }

        public string Address { get; init; }

        /// <summary>
        /// Gets or sets the patients.
        /// </summary>
        public Patients Patients { get; set; }
    }
}