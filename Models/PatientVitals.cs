﻿// <copyright file="PatientVitals.cs" company="VVU">
// Copyright (c) VVU. All rights reserved.
// </copyright>

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMS.Models
{
    public class PatientVitals
    {
        public int Id { get; set; }

        public string PatientId { get; set; }

        public double Temperature { get; set; }

        [DisplayName("Weight (Kg)")]
        public double Weight { get; set; }

        [DisplayName("Blood Pressure")]
        public string Bp { get; set; }

        public string Date { get; set; }
    }
}