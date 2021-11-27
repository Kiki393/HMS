namespace HMS.Models
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class PatientVitals
    {
        public int Id { get; set; }

        [ForeignKey("PatientId")]
        public virtual Patients Patients { get; set; }

        public double Temperature { get; set; }

        [DisplayName("Weight (Kg)")]
        public double Weight { get; set; }

        [DisplayName("Blood Pressure")]
        public string Bp { get; set; }

        public DateTime Date { get; set; }
    }
}