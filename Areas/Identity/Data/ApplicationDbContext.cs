// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApplicationDbContext.cs" company="VVU">
// Copyright (c) VVU. All rights reserved.
// </copyright>
// <summary>
//   Defines the ApplicationDbContext type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using HMS.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HMS.Areas.Identity.Data
{
    /// <summary>
    /// The application database context.
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
        /// </summary>
        /// <param name="options">
        /// The options.
        /// </param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the appointments.
        /// </summary>
        public DbSet<Appointment> Appointments { get; set; }

        /// <summary>
        /// Gets or sets the patients.
        /// </summary>
        public DbSet<Patients> Patients { get; set; }

        /// <summary>
        /// Gets or sets the medicines.
        /// </summary>
        public DbSet<Medicine> Medicines { get; set; }

        /// <summary>
        /// Gets or sets the vitals.
        /// </summary>
        public DbSet<PatientVitals> Vitals { get; set; }

        /// <summary>
        /// Gets or sets the assign doctors.
        /// </summary>
        public DbSet<AssignDoctor> AssignDoctors { get; set; }

        /// <summary>
        /// Gets or sets the announcements.
        /// </summary>
        public DbSet<Announcements> Announcements { get; set; }

        /// <summary>
        /// Gets or sets the messages.
        /// </summary>
        public DbSet<Messages> Messages { get; set; }

        /// <summary>
        /// Gets or sets the waiting.
        /// </summary>
        public DbSet<VitalsWaiting> Waiting { get; set; }

        /// <summary>
        /// Gets or sets the consultations.
        /// </summary>
        public DbSet<Consultations> Consultations { get; set; }

        /// <summary>
        /// Gets or sets the prescriptions.
        /// </summary>
        public DbSet<Prescriptions> Prescriptions { get; set; }

        /// <summary>
        /// Gets or sets the lab waiting.
        /// </summary>
        public DbSet<LabWaiting> LabWaiting { get; set; }

        /// <summary>
        /// Gets or sets the lab results.
        /// </summary>
        public DbSet<LabResult> LabResults { get; set; }

        /// <summary>
        /// Gets or sets the pay waiting.
        /// </summary>
        public DbSet<PayWaiting> PayWaiting { get; set; }

        /// <summary>
        /// Gets or sets the pharmacy waiting.
        /// </summary>
        public DbSet<PharmWaiting> PharmacyWaiting { get; set; }

        /// <summary>
        /// Gets or sets the transactions.
        /// </summary>
        public DbSet<Transactions> Transactions { get; set; }

        /// <summary>
        /// Gets or sets the referrals.
        /// </summary>
        public DbSet<Referrals> Referrals { get; set; }

        /// <summary>
        /// The on model creating.
        /// </summary>
        /// <param name="builder">
        /// The builder.
        /// </param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.Entity<Patients>()
                .HasAlternateKey(p => p.PatientId);
        }
    }
}