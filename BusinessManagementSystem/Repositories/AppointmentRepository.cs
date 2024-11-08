using AspNetCore;
using BusinessManagementSystem.Data;
using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Models;
using BusinessManagementSystem.Services;

namespace BusinessManagementSystem.Repositories
{
    public class AppointmentRepository : GenericRepository<Appointment>, IAppointment
    {
        public ResponseDto<Appointment> _responseDto;
        public ResponseDto<AppointmentDto> _responseAppointmentDto;

        public AppointmentRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
            _responseDto = new ResponseDto<Appointment>();
            _responseAppointmentDto = new ResponseDto<AppointmentDto>();
        }

        //public int GetTotalCost(string category, int totalHours, int deposit, int discount, int discountInHour)
        //{
        //   var categoryFee=
        //}
    }
}
