using BusinessManagementSystem.Data;
using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Enums;
using BusinessManagementSystem.Models;
using BusinessManagementSystem.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BusinessManagementSystem.Repositories
{
    public class TipRepository : GenericRepository<Tip>, ITip
    {
        public ResponseDto<TipDto> _responseTipDto;

        public TipRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
            _responseTipDto = new ResponseDto<TipDto>();
        }

        public ResponseDto<TipDto> GetAllTips(RequestDto requestDto)
        {
            var query = (from t in _dbContext.Tips
                               join a in _dbContext.Appointments on t.AppointmentId equals a.Id
                               join u in _dbContext.Users on t.TipAssignedToUser equals u.Id
                               where t.CreatedAt >= requestDto.StartDate && t.CreatedAt <= requestDto.EndDate
                                select new TipDto
                               {
                                     TipId = t.Id,
                                    AppointmentId = a.Id,
                                    AppointmentGuid=a.guid,
                                    TipAmount=t.TipAmount,
                                    TipAmountForUsers=t.TipAmountForUsers,
                                    TipAssignedToUserFullName= a.UserId == t.TipAssignedToUser
                                            ? $"{u.FullName} (Artist)"
                                            : u.FullName, // Append conditionally
                                   TipSettlement =t.TipSettlement,
                                    CreatedAt=t.CreatedAt,
                                    UpdatedAt=t.UpdatedAt,
                                    CreatedBy=t.CreatedBy,
                                    UpdatedBy=t.UpdatedBy,
                               }).ToList();
            if (query.Count > 0 ) _responseTipDto.Datas = query;
            else
            {
                _responseTipDto.StatusCode = HttpStatusCode.NotFound;
                _responseTipDto.Message = "Not Found";
            }
            
            return _responseTipDto;
                             
        }

        public ResponseDto<TipDto> GetMyTips(int userId, RequestDto requestDto)
        {
            var query = (from t in _dbContext.Tips
                        join a in _dbContext.Appointments on t.AppointmentId equals a.Id
                        join u in _dbContext.Users on t.TipAssignedToUser equals u.Id
                        where t.TipAssignedToUser==userId && t.CreatedAt >= requestDto.StartDate && t.CreatedAt <= requestDto.EndDate
                         select new TipDto
                        {
                            TipId = t.Id,
                            AppointmentId = a.Id,
                            AppointmentGuid = a.guid,
                            TipAmount = t.TipAmount,
                            TipAmountForUsers = t.TipAmountForUsers,
                            TipAssignedToUserFullName = a.UserId == t.TipAssignedToUser
                                     ? $"{u.FullName} (Artist)"
                                     : u.FullName, // Append conditionally
                            TipSettlement = t.TipSettlement,
                            CreatedAt = t.CreatedAt,
                            UpdatedAt = t.UpdatedAt,
                            CreatedBy = t.CreatedBy,
                            UpdatedBy = t.UpdatedBy,
                        }).ToList();
            if (query.Count > 0)_responseTipDto.Datas = query;
            else
            {
                _responseTipDto.StatusCode = HttpStatusCode.NotFound;
                _responseTipDto.Message = "Not Found";
            }
            _responseTipDto.Datas = query;
            return _responseTipDto;
        }
    }
}
