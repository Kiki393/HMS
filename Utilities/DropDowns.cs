// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DropDowns.cs" company="VVU">
// Copyright (c) VVU. All rights reserved.
// </copyright>
// <summary>
//   Defines the DropDowns type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HMS.Utilities
{
    /// <summary>
    /// The drop downs.
    /// </summary>
    public static class DropDowns
    {
        /// <summary>
        /// The get roles for drop down.
        /// </summary>
        /// <param name="isAdmin">
        /// The is admin.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public static IEnumerable<SelectListItem> GetRolesForDropDown(bool isAdmin)
        {
            if (isAdmin)
            {
                return new List<SelectListItem>
                           {
                               new SelectListItem { Value = UserRoles.Admin, Text = UserRoles.Admin },
                               new SelectListItem { Value = UserRoles.Doctor, Text = UserRoles.Doctor },
                               new SelectListItem { Value = UserRoles.Nurse, Text = UserRoles.Nurse },
                               new SelectListItem { Value = UserRoles.Cashier, Text = UserRoles.Cashier },
                               new SelectListItem { Value = UserRoles.Lab, Text = UserRoles.Lab },
                               new SelectListItem { Value = UserRoles.Pharmacy, Text = UserRoles.Pharmacy },
                               new SelectListItem { Value = UserRoles.Receptionist, Text = UserRoles.Receptionist },
                               new SelectListItem { Value = UserRoles.Patient, Text = UserRoles.Patient },
                           };
            }

            return new List<SelectListItem>
            {
                new SelectListItem { Value = UserRoles.Patient, Text = UserRoles.Patient },
            };
        }

        /// <summary>
        /// The get time drop down.
        /// </summary>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public static List<SelectListItem> GetTimeDropDown()
        {
            var minute = 60;
            var duration = new List<SelectListItem>();
            for (var i = 1; i <= 12; i++)
            {
                duration.Add(new SelectListItem { Value = minute.ToString(), Text = i + " Hr" });
                minute += 30;
                duration.Add(new SelectListItem { Value = minute.ToString(), Text = i + " Hr 30 min" });
                minute += 30;
            }

            return duration;
        }

        /// <summary>
        /// The get gender drop down.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public static IEnumerable<SelectListItem> GetGenderDropDown()
        {
            return new List<SelectListItem>
                       {
                           new () { Value = "Male", Text = "Male" },
                           new () { Value = "Female", Text = "Female" },
                           new () { Value = "Other", Text = "Other" },
                       };
        }
    }
}