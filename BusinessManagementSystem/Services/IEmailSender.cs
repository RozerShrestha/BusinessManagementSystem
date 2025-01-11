using BusinessManagementSystem.Dto;

namespace BusinessManagementSystem.Services
{
    public interface IEmailSender
    {
       Task SendEmailAsync(string email, string subject, string htmlMessage);
        string PrepareEmail(UserDto userDto, string message);
        string PrepareEmailAppointmentArtist(AppointmentDto appointmentDto, string message);
        string PrepareEmailAppointmentClient(AppointmentDto appointmentDto, string message);
        string PrepareEmailPaymentSettlement(PaymentTipSettlementDto paymentTipSettlementDto, string message);
    }
}
