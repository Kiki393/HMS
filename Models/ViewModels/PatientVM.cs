// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PatientVM.cs" company="">
//
// </copyright>
// <summary>
//   Defines the PatientVm type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace HMS.Models.ViewModels
{
    using HMS.Models;

    /// <summary>
    /// The patient view model.
    /// </summary>
    public class PatientVm
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the patients.
        /// </summary>
        public Patients Patients { get; set; }
    }
}