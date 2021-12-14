using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HMS.Areas.Identity.Data;
using HMS.Models;
using Microsoft.AspNetCore.Identity;

namespace HMS.Count
{
    public class CountInTables
    {
        public IEnumerable<Patients> Patients { get; set; }

        public IEnumerable<Medicine> Medicines { get; set; }

        public IEnumerable<Appointment> ConfirmedAppointments { get; set; }

        public IEnumerable<Appointment> PendingAppointments { get; set; }

        public IEnumerable<ApplicationUser> ApplicationUsers { get; set; }
    }
}