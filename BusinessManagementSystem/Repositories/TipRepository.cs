﻿using BusinessManagementSystem.Data;
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
        public ResponseDto<TipSettlementDto> _responseTipSettlementDto;
        public TipRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
            _responseTipDto = new ResponseDto<TipDto>();
            _responseTipSettlementDto = new ResponseDto<TipSettlementDto>();
        }
        public ResponseDto<TipDto> GetAllTips(RequestDto requestDto)
        {

            var query = from t in _dbContext.Tips
                               join a in _dbContext.Appointments on t.AppointmentId equals a.Id
                               join u in _dbContext.Users on t.TipAssignedToUser equals u.Id
                               where t.CreatedAt >= requestDto.StartDate && t.CreatedAt <= requestDto.EndDate.AddDays(1)
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

        public dynamic GetAllTipsDashboard(RequestDto requestDto)
        {
            var result = (from t in _dbContext.Tips
                          join u in _dbContext.Users
                          on t.TipAssignedToUser equals u.Id
                          where t.TipSettlement == true && t.CreatedAt >= requestDto.StartDate && t.CreatedAt <= requestDto.EndDate.AddDays(1)
                          group t by new { u.FullName } into g
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
                        where t.TipAssignedToUser==userId && t.CreatedAt >= requestDto.StartDate && t.CreatedAt <= requestDto.EndDate.AddDays(1)
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

        public ResponseDto<TipSettlementDto> GetTipSettlement(RequestDto requestDto)
        {
            try
            {
                var queryTips = from u in _dbContext.Users
                                join a in _dbContext.Appointments on u.Id equals a.UserId
                                join t in _dbContext.Tips on a.Id equals t.AppointmentId
                                select new
                                {
                                    User = u,
                                    Appointment = a,
                                    Tip = t
                                };

                // Apply conditions dynamically
                if (requestDto.StartDate != null)
                    queryTips = queryTips.Where(x => x.Tip.CreatedAt >= requestDto.StartDate);
                if (requestDto.EndDate != null)
                    queryTips = queryTips.Where(x => x.Tip.CreatedAt <= requestDto.EndDate);
                if (requestDto.UserId > 0)
                    queryTips = queryTips.Where(x => x.Tip.TipAssignedToUser == requestDto.UserId);
                if (requestDto.Status != "All")
                    queryTips = queryTips.Where(x => x.Appointment.Status == requestDto.Status);
                if (requestDto.Settlement != "ALL")
                    queryTips = queryTips.Where(x => x.Tip.TipSettlement == bool.Parse(requestDto.Settlement));

                // Project the result
                var tipItems = queryTips
                    .Select(x => new TipSettlementDto
                    {
                        AppointmentId = x.Appointment.Id,
                        TipId = x.Tip.Id,
                        AppointmentDate = x.Appointment.AppointmentDate,
                        TipCreatedDate = x.Tip.CreatedAt,
                        ClientName = x.Appointment.ClientName,
                        ArtistName = x.User.FullName,
                        TipAssignedUser = x.Tip.TipAssignedToUser.ToString(),
                        TipAmount = x.Tip.TipAmount,
                        TipAmountForUser = x.Tip.TipAmountForUsers,
                        TipSettlement = x.Tip.TipSettlement,
                        Status = x.Appointment.Status,
                    })
                    .OrderByDescending(x => x.AppointmentDate)
                    .ToList();
                List<TipSettlementDto> tipItemsNew = new List<TipSettlementDto>();
                foreach (var tip in tipItems)
                {
                    tip.TipAssignedUser = _dbContext.Users.Find(Convert.ToInt32(tip.TipAssignedUser)).FullName;
                    tipItemsNew.Add(tip);
                }
                _responseTipSettlementDto.Datas = tipItemsNew;
            }
            catch (Exception ex)
            {
                _responseTipSettlementDto.StatusCode = HttpStatusCode.NotFound;
                _responseTipSettlementDto.Message = ex.Message;
            }
            return _responseTipSettlementDto;
        }
    }
}
