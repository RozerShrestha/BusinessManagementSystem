using BusinessManagementSystem.Models;
using System.ComponentModel;

namespace BusinessManagementSystem.Dto
{
    public class UserDetailDto
    {
        public string UserId { get; set; }
        public string ProfilePictureLink { get; set; }
        public string FullName { get; set; }
        public string Occupation { get; set; }
        public string PhoneNumber { get; set; }
        public string? FacebookLink { get; set; }
        public string? InstagramLink { get; set; }
        public string? TiktokLink { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string? Skills { get; set; }
        public string? Notes { get; set; }
        public int Percentage { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }
}
