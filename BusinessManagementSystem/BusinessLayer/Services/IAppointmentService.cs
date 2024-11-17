using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Models;

namespace BusinessManagementSystem.BusinessLayer.Services
{
    public interface IAppointmentService
    {
        ResponseDto<AppointmentDto> GetAllAppointment();
        ResponseDto<AppointmentDto> GetAllAppointmentByArtist(int userId);

        ResponseDto<AppointmentDto> GetAppointmentById(int id);
        ResponseDto<AppointmentDto> GetAppointmentByGuid(Guid guid);
        ResponseDto<Appointment> GetAppointmentByStatus(string status);
        ResponseDto<Appointment> CreateAppointment(AppointmentDto appointmentDto);
        ResponseDto<Appointment> UpdateAppointment(AppointmentDto appointmentDto);
        ResponseDto<Appointment> DeleteAppointmentByGuid(Guid guid);
        ResponseDto<Appointment> DeleteAppointmentById(int id);
        string GetTotalCost(bool isForeigner,string category, int totalHours, int deposit, int discount, int discountInHour, out int totalCost);

    }
}
