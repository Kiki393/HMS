// <copyright file="Patients.cs" company="VVU">
// Copyright (c) VVU. All rights reserved.
// </copyright>

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMS.Models
{
    public class Patients
    {
        public int Id { get; init; }

        public string ApplicationUserId { get; set; }

        [DisplayName("Patient ID")]
        public string PatientId { get; init; }

        [Required]
        public string Name { get; init; }

        [DisplayName("Date of birth")]
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Dob { get; init; }

        public int Age { get; init; }

        [DisplayName("Phone Number")]
        public string Contact { get; init; }

        public string Address { get; init; }

        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; init; }

        [DataType(DataType.MultilineText)]
        public string Diagnosis { get; set; }
    }
}