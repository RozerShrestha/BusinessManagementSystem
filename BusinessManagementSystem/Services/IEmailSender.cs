using BusinessManagementSystem.Dto;

namespace BusinessManagementSystem.Services
{
    public interface IEmailSender
    {
       Task SendEmailAsync(string email, string subject, string htmlMessage);
        string PrepareEmail(UserDto userDto, string message);
        string PrepareEmailNewAppointmentArtist(AppointmentDto appointmentDto, string message);
        string PrepareEmailInsurancePlan(string message, string fullName, string oldPlan, string newPlan, string iNumber, int id = 0);
        string PrepareEmailHrApproved(string message, string fullName);


    }
}
