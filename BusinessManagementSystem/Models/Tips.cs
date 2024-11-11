using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessManagementSystem.Models
{
    public class Tip:BaseEntity
    {
        public int Id { get; set; }
        public double TipAmount { get; set; }
        public int AppointmentId { get; set; }

        public string TipAssignedUsers { get; set; }


        [ForeignKey("AppointmentId")]
        [ValidateNever]
        public Appointment Appointment { get; set; }

    }
}
