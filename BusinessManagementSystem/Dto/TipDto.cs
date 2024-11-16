using BusinessManagementSystem.Models;
using System.ComponentModel;

namespace BusinessManagementSystem.Dto
{
    public class TipDto:BaseEntity
    {
        public int TipId { get; set; }
        public int AppointmentId { get; set; }
        public double TipAmount { get; set; }
        public double TipAmountForUsers { get; set; }
        public string ArtistAssigned { get; set; }
        public string TipAssignedToUserFullName { get; set; }
        public bool TipSettlement { get; set; }
    }
}
