namespace HMS.Models
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Patients
    {
        public int Id { get; init; }

        [Required]
        [DisplayName("Patient ID")]
        public string PatientId { get; init; }

        [Required]
        public string Name { get; init; }

        [DisplayName("Date of birth")]
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Dob { get; init; }

        public int Age { get; init; }

        [DisplayName("Phone Number")]
        public string Contact { get; init; }

        public string Address { get; init; }

        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; init; }

        [DataType(DataType.MultilineText)]
        public string Diagnosis { get; set; }
    }
}