using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessManagementSystem.Models
{
    public class PaymentHistory:BaseEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public double TotalPayment { get; set; }
        public double TotalTips { get; set; }
        public double GrandTotal { get; set; }
        public string PaidStatus { get; set; }
        public DateOnly PaymentFrom { get; set; }
        public DateOnly PaymentTo { get; set; }
        [ValidateNever]
        [ForeignKey("UserId")]
        public User User { get; set; }

    }
}
   