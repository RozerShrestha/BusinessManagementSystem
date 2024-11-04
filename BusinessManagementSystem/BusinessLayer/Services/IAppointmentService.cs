using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Models;

namespace BusinessManagementSystem.BusinessLayer.Services
{
    public interface IAppointmentService
    {
        ResponseDto<AppointmentDto> GetAllAppointment();
        ResponseDto<AppointmentDto> GetAppointmentById(int id);
        ResponseDto<AppointmentDto> GetAppointmentByGuid(Guid guid);
        ResponseDto<AppointmentDto> GetAppointmentByStatus(string status);
        ResponseDto<AppointmentDto> CreateAppointment(Appointment appointment);
        ResponseDto<AppointmentDto> UpdateAppointment(Appointment appointment);
        ResponseDto<AppointmentDto> DeleteAppointmentByGuid(Guid guid);
        ResponseDto<AppointmentDto> DeleteAppointmentById(int id);

    }
}
