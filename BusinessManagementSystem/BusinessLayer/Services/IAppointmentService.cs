using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Dto.Chart;
using BusinessManagementSystem.Models;

namespace BusinessManagementSystem.BusinessLayer.Services
{
    public interface IAppointmentService
    {
        ResponseDto<AppointmentDto> GetAllAppointment(RequestDto requestDto);
        ResponseDto<AppointmentDto> GetAllAppointmentByArtist(int userId, RequestDto requestDto);
        ResponseDto<AppointmentDto> GetAppointmentById(int id);
        ResponseDto<AppointmentDto> GetAppointmentByGuid(Guid guid);
        ResponseDto<Appointment> GetAppointmentByStatus(string status);
        ResponseDto<Appointment> CreateAppointment(AppointmentDto appointmentDto);
        ResponseDto<Appointment> UpdateAppointment(AppointmentDto appointmentDto);
        ResponseDto<Appointment> DeleteAppointmentByGuid(Guid guid);
        ResponseDto<Appointment> DeleteAppointmentById(int id);
        string GetDueCost(bool isForeigner,string category, double totalHours, int deposit, int discount, double discountInHour, out double dueAmount);
    }
}
