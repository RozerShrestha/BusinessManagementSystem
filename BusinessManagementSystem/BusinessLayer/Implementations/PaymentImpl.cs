using AspNetCore;
using AutoMapper;
using BusinessManagementSystem.BusinessLayer.Services;
using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Models;
using BusinessManagementSystem.Services;

namespace BusinessManagementSystem.BusinessLayer.Implementations
{
    public class PaymentImpl : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        ResponseDto<Payment> _responseDto;
        public ResponseDto<PaymentDto> _responsePaymentDto;
        public PaymentImpl(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _responseDto = new ResponseDto<Payment>();
            _responsePaymentDto = new ResponseDto<PaymentDto>();
        }
        public ResponseDto<PaymentDto> GetAllPayments()
        {
            _responsePaymentDto = _unitOfWork.Payment.GetAllPayments();
            return _responsePaymentDto;
        }

        public ResponseDto<PaymentDto> GetMyPayments(int userId)
        {
            _responsePaymentDto = _unitOfWork.Payment.GetMyPayments(userId);
            return _responsePaymentDto;
        }

        public ResponseDto<PaymentDto> GetMyPayments(Guid guid)
        {
            _responsePaymentDto = _unitOfWork.Payment.GetMyPayments(guid);
            return _responsePaymentDto;
        }
    }
}
