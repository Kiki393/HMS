// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppointmentVM.cs" company="">
//
// </copyright>
// <summary>
//   Defines the AppointmentVM type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace HMS.Models.ViewModels
{
    /// <summary>
    /// The appointment view model.
    /// </summary>
    public class AppointmentVm
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int? Id { get; set; }

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
        public string StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        public string EndDate { get; set; }

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

        /// <summary>
        /// Gets or sets the doctor name.
        /// </summary>
        public string DoctorName { get; set; }

        /// <summary>
        /// Gets or sets the patient name.
        /// </summary>
        public string PatientName { get; set; }

        /// <summary>
        /// Gets or sets the admin name.
        /// </summary>
        public string AdminName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is for client.
        /// </summary>
        public bool IsForClient { get; set; }
    }
}