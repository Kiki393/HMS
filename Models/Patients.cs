// <copyright file="Patients.cs" company="VVU">
// Copyright (c) VVU. All rights reserved.
// </copyright>

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMS.Models
{
    /// <summary>
    /// The patients.
    /// </summary>
    public class Patients
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the patient id.
        /// </summary>
        [DisplayName("Patient ID")]
        public string PatientId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the date of birth .
        /// </summary>
        [DisplayName("Date of birth")]
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Dob { get; set; }

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
        [DisplayName("Phone Number")]
        public string Contact { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the NHIS id.
        /// </summary>
        [DisplayName("NHIS ID")]
        public string Nhis { get; set; }
    }
}