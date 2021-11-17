// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Appointment.cs" company="">
//
// </copyright>
// <summary>
//   Defines the Appointment type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace HMS.Models
{
    using System;

    /// <summary>
    /// The appointment.
    /// </summary>
    public class Appointment
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the duration.
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// Gets or sets the doctor id.
        /// </summary>
        public string DoctorId { get; set; }

        /// <summary>
        /// Gets or sets the patient id.
        /// </summary>
        public string PatientId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is doctor approved.
        /// </summary>
        public bool IsDoctorApproved { get; set; }

        /// <summary>
        /// Gets or sets the admin id.
        /// </summary>
        public string AdminId { get; set; }
    }
}