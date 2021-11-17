// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotificationMessages.cs" company="">
//
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
        public static string appointmentAdded = "Appointment added successfully.";

        /// <summary>
        /// The appointment updated.
        /// </summary>
        public static string appointmentUpdated = "Appointment updated successfully.";

        /// <summary>
        /// The appointment deleted.
        /// </summary>
        public static string appointmentDeleted = "Appointment deleted successfully.";

        /// <summary>
        /// The appointment exists.
        /// </summary>
        public static string appointmentExists = "Appointment for selected date and time already exists.";

        /// <summary>
        /// The appointment not exists.
        /// </summary>
        public static string appointmentNotExists = "Appointment does not exist.";

        /// <summary>
        /// The appointment confirm.
        /// </summary>
        public static string appointmentConfirm = "Appointment confirmed.";

        /// <summary>
        /// The appointment confirm error.
        /// </summary>
        public static string appointmentConfirmError = "Something went wrong, appointment not confirmed. Please try again.";

        /// <summary>
        /// The appointment add error.
        /// </summary>
        public static string appointmentAddError = "Something went wrong. Please try again.";

        /// <summary>
        /// The appointment update error.
        /// </summary>
        public static string appointmentUpdateError = "Something went wrong. Please try again.";

        /// <summary>
        /// The something went wrong.
        /// </summary>
        public static string somethingWentWrong = "Something went wrong. Please try again.";

        /// <summary>
        /// The success code.
        /// </summary>
        public static int successCode = 1;

        /// <summary>
        /// The failure code.
        /// </summary>
        public static int failureCode = 0;
    }
}