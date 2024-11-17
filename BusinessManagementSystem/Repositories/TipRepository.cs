using BusinessManagementSystem.Data;
using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Models;
using BusinessManagementSystem.Services;

namespace BusinessManagementSystem.Repositories
{
    public class TipRepository : GenericRepository<Tip>, ITip
    {
        public ResponseDto<TipDto> _responseTipDto;

        public TipRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
            _responseTipDto = new ResponseDto<TipDto>();
        }

        public ResponseDto<TipDto> GetAllTips()
        {
            var tipD = (from t in _dbContext.Tips
                               join a in _dbContext.Appointments on t.AppointmentId equals a.Id
                               join u in _dbContext.Users on t.TipAssignedToUser equals u.Id
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
            _responseTipDto.Datas = tipD;
            return _responseTipDto;
                             
        }

        public ResponseDto<TipDto> GetMyTips(int userId)
        {
            var tipD = (from t in _dbContext.Tips
                        join a in _dbContext.Appointments on t.AppointmentId equals a.Id
                        join u in _dbContext.Users on t.TipAssignedToUser equals u.Id
                        where t.TipAssignedToUser==userId
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
            _responseTipDto.Datas = tipD;
            return _responseTipDto;
        }
    }
}
