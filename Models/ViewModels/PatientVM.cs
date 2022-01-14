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

        /// <summary>
        /// Gets or sets the patient id.
        /// </summary>
        public string PatientId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the date of birth.
        /// </summary>
        public string Dob { get; set; }

        /// <summary>
        /// Gets or sets the age.
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// Gets or sets the contact.
        /// </summary>
        public string Contact { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the NHIS id.
        /// </summary>
        public string Nhis { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the patients.
        /// </summary>
        public Patients Patients { get; set; }
    }
}