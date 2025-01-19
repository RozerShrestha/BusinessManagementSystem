using BusinessManagementSystem.Data;
using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Dto.Chart;
using BusinessManagementSystem.Models;
using BusinessManagementSystem.Services;
using BusinessManagementSystem.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BusinessManagementSystem.Repositories
{
    public class DashboardRepository : GenericRepository<DashboardDto>, IDashboard
    {
        private readonly ApplicationDBContext _db;
        public ResponseDto<DashboardDto> _responseDtoEmployee;
        public ResponseDto<DashboardDto> _responseDtoAllEmployee;
        private string[] possibleStatuses= new[] { "InProgress", "Scheduled/Rescheduled", "Confirmed", "Completed"};


        public DashboardRepository(ApplicationDBContext db) : base(db)
        {
            _responseDtoEmployee = new ResponseDto<DashboardDto>();
            _responseDtoAllEmployee = new ResponseDto<DashboardDto>();
            _db = db;
        }

        public ResponseDto<DashboardDto> GetDashboardInfo(RequestDto requestDto, int userId)
        {
            var appointmentStatuses = _db.Appointments
                                        .Where(a => a.AppointmentDate >= requestDto.StartDate && a.AppointmentDate <= requestDto.EndDate && a.UserId == userId)
                                        .GroupBy(a => a.Status == "Scheduled" || a.Status == "Rescheduled" ? "Scheduled/Rescheduled" : a.Status)
                                        .Select(g => new
                                        {
                                            StatusGroup = g.Key,
                                            Count = g.Count()
                                        }).ToList();


            var result = possibleStatuses
                        .GroupJoin(
                            appointmentStatuses,
                            ps => ps,                   // Key from possible statuses
                            asg => asg.StatusGroup,     // Key from grouped appointments
                            (ps, asg) => new
                            {
                                StatusGroup = ps,
                                Count = asg.Sum(x => x.Count) // Sum counts or return 0 if no match
                            })
                        .Select(x => new
                        {
                            x.StatusGroup,
                            Count = x.Count
                        })
                        .ToList();
            _responseDtoEmployee.Dynamic_Datas = result;
            return _responseDtoEmployee;
        }

        public ResponseDto<DashboardDto> GetDashboardInfoAllEmployee(RequestDto requestDto)
        {
            var appointmentStatuses = _db.Appointments
                                        .Where(a => a.AppointmentDate >= requestDto.StartDate && a.AppointmentDate <= requestDto.EndDate)
                                        .GroupBy(a => a.Status == "Scheduled" || a.Status == "Rescheduled" ? "Scheduled/Rescheduled" : a.Status)
                                        .Select(g => new
                                        {
                                            StatusGroup = g.Key,
                                            Count = g.Count()
                                        }).ToList();


            var result = possibleStatuses
                        .GroupJoin(
                            appointmentStatuses,
                            ps => ps,                   // Key from possible statuses
                            asg => asg.StatusGroup,     // Key from grouped appointments
                            (ps, asg) => new
                            {
                                StatusGroup = ps,
                                Count = asg.Sum(x => x.Count) // Sum counts or return 0 if no match
                            })
                        .Select(x => new
                        {
                            x.StatusGroup,
                            Count = x.Count
                        })
                        .ToList();
            _responseDtoAllEmployee.Dynamic_Datas = result;
            return _responseDtoAllEmployee;
        }
    }
}
