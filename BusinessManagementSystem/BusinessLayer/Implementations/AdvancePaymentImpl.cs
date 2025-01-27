using AutoMapper;
using BusinessManagementSystem.BusinessLayer.Services;
using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Models;
using BusinessManagementSystem.Services;
using System.Net;

namespace BusinessManagementSystem.BusinessLayer.Implementations
{
    public class AdvancePaymentImpl:IAdvancePaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        ResponseDto<AdvancePayment> _responseDto;

        public AdvancePaymentImpl(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _responseDto = new ResponseDto<AdvancePayment>();

        }

        public ResponseDto<AdvancePayment> CreateAdvancePayment(AdvancePayment advancePayment)
        {
            advancePayment.guid = Helper.Helpers.GenerateGUID();
            _responseDto = _unitOfWork.AdvancePayment.Insert(advancePayment);
           return _responseDto;
        }

        public ResponseDto<AdvancePayment> DeleteAdvancePayment(int id)
        {
            var item=_unitOfWork.AdvancePayment.GetById(id);
            if(item.StatusCode==HttpStatusCode.OK)
                _responseDto = _unitOfWork.AdvancePayment.Delete(item.Data);

            return _responseDto;
        }

        public ResponseDto<AdvancePayment> GetAdvancePayment(int id)
        {
            _responseDto = _unitOfWork.AdvancePayment.GetById(id);
            return _responseDto;
        }

        public ResponseDto<AdvancePayment> GetAdvancePayment(Guid guid)
        {
            _responseDto = _unitOfWork.AdvancePayment.GetFirstOrDefault(p => p.guid == guid);
            return _responseDto;
        }

        public ResponseDto<AdvancePayment> GetAllAdvancePayment(RequestDto requestDto)
        {
            _responseDto = _unitOfWork.AdvancePayment.GetAll(p => p.CreatedAt >= requestDto.StartDate && p.CreatedAt <= requestDto.EndDate,
                                        orderBy: p => p.CreatedAt,
                                        orderByDescending: true,
                                        includeProperties: "User");
            return _responseDto;
        }

        public ResponseDto<AdvancePayment> GetMyAdvancePayment(RequestDto requestDto, int userId)
        {
            _responseDto = _unitOfWork.AdvancePayment.GetAll(p => p.CreatedAt >= requestDto.StartDate && p.CreatedAt <= requestDto.EndDate && p.UserId==userId,
                                        orderBy: p => p.CreatedAt,
                                        orderByDescending: true,
                                        includeProperties: "User");
            return _responseDto;
        }

        public ResponseDto<AdvancePayment> UpdateAdvancePayment(AdvancePayment advancePayment)
        {
            var item=_unitOfWork.AdvancePayment.GetFirstOrDefault(p=>p.guid==advancePayment.guid);
            if (item.StatusCode == HttpStatusCode.OK)
            {
                item.Data.Amount = advancePayment.Amount;
                item.Data.Reason=advancePayment.Reason;
                item.Data.PaymentMethod=advancePayment.PaymentMethod;
                item.Data.PaidDate=advancePayment.Status==true? DateOnly.FromDateTime(DateTime.Now):default;
                item.Data.Status=advancePayment.Status;
            }
            
            _responseDto = _unitOfWork.AdvancePayment.Update(item.Data);
            return _responseDto;
        }
    }
}
