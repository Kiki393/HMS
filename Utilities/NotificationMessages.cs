// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotificationMessages.cs" company="VVU">
// Copyright (c) VVU. All rights reserved.
// </copyright>
// <summary>
//   Defines the NotificationMessages type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace HMS.Utilities
{
    /// <summary>
    /// The notification messages.
    /// </summary>
    public static class NotificationMessages
    {
        /// <summary>
        /// The appointment added.
        /// </summary>
        public const string AppointmentAdded = "Appointment added successfully.";

        /// <summary>
        /// The appointment updated.
        /// </summary>
        public const string AppointmentUpdated = "Appointment updated successfully.";

        /// <summary>
        /// The appointment deleted.
        /// </summary>
        public const string AppointmentDeleted = "Appointment deleted successfully.";

        /// <summary>
        /// The appointment exists.
        /// </summary>
        public const string AppointmentExists = "Appointment for selected date and time already exists.";

        /// <summary>
        /// The appointment not exists.
        /// </summary>
        public const string AppointmentNotExists = "Appointment does not exist.";

        /// <summary>
        /// The appointment confirm.
        /// </summary>
        public const string AppointmentConfirm = "Appointment confirmed.";

        /// <summary>
        /// The appointment confirm error.
        /// </summary>
        public const string AppointmentConfirmError = "Something went wrong, appointment not confirmed. Please try again.";

        /// <summary>
        /// The appointment add error.
        /// </summary>
        public const string AppointmentAddError = "Something went wrong. Please try again.";

        /// <summary>
        /// The appointment update error.
        /// </summary>
        public const string AppointmentUpdateError = "Something went wrong. Please try again.";

        /// <summary>
        /// The something went wrong.
        /// </summary>
        public const string SomethingWentWrong = "Something went wrong. Please try again.";

        /// <summary>
        /// The success code.
        /// </summary>
        public const int SuccessCode = 1;

        /// <summary>
        /// The failure code.
        /// </summary>
        public const int FailureCode = 0;
    }
}