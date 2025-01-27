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

            var query = from t in _dbContext.Tips
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
                               };
            if (requestDto.Settlement != "ALL")
                query = (IQueryable<TipDto>)query.Where(p => p.TipSettlement == Convert.ToBoolean(requestDto.Settlement));

            var tipDto = query.ToList();

            if (tipDto.Count > 0 ) 
                _responseTipDto.Datas = tipDto;
            else
            {
                _responseTipDto.StatusCode = HttpStatusCode.NotFound;
                _responseTipDto.Message = "Not Found";
            }
            
            return _responseTipDto;
                             
        }

        public dynamic GetAllTips()
        {
            var result = (from p in _dbContext.Tips
                          join u in _dbContext.Users
                          on p.TipAssignedToUser equals u.Id
                          where p.TipSettlement == true
                          group p by new { u.FullName } into g
                          select new
                          {
                              FullName = g.Key.FullName,
                              TotalPaymentToArtist = g.Sum(p => p.TipAmountForUsers)
                          }).ToList();
            return result;
        }

        public ResponseDto<TipDto> GetMyTips(int userId, RequestDto requestDto)
        {
            var query = from t in _dbContext.Tips
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
                        };

            if (requestDto.Settlement != "ALL")
                query = (IQueryable<TipDto>)query.Where(p => p.TipSettlement == Convert.ToBoolean(requestDto.Settlement));

            var tipDto = query.ToList();

            if (tipDto.Count > 0)
                _responseTipDto.Datas = tipDto;
            else
            {
                _responseTipDto.StatusCode = HttpStatusCode.NotFound;
                _responseTipDto.Message = "Not Found";
            }
            _responseTipDto.Datas = tipDto;
            return _responseTipDto;
        }

        public dynamic GetTipsSegregation(RequestDto requestDto)
        {
            var result = _dbContext.Tips.Where(t => t.TipSettlement == true && t.UpdatedAt >= requestDto.StartDate && t.UpdatedAt <= requestDto.EndDate)
                       .GroupBy(t => 1)
                       .Select(g => new
                       {
                           TotalTips = g.Sum(t => t.TipAmountForUsers)
                       }).FirstOrDefault();
            return result;
        }
    }
}
