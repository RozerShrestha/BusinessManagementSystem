using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Models;

namespace BusinessManagementSystem.BusinessLayer.Services
{
    public interface IAppointmentService
    {
        ResponseDto<AppointmentDto> GetAllAppointment();
        ResponseDto<Appointment> GetAppointmentById(int id);
        ResponseDto<Appointment> GetAppointmentByGuid(Guid guid);
        ResponseDto<Appointment> GetAppointmentByStatus(string status);
        ResponseDto<Appointment> CreateAppointment(Appointment appointment);
        ResponseDto<Appointment> UpdateAppointment(Appointment appointment);
        ResponseDto<Appointment> DeleteAppointmentByGuid(Guid guid);
        ResponseDto<Appointment> DeleteAppointmentById(int id);
        string GetTotalCost(string category, int totalHours, int deposit, int discount, int discountInHour, out int totalCost);

    }
}
