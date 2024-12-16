using AspNetCore;
using AutoMapper;
using BusinessManagementSystem.BusinessLayer.Services;
using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Models;
using BusinessManagementSystem.Services;
using System.Net;

namespace BusinessManagementSystem.BusinessLayer.Implementations
{
    public class PaymentImpl : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        ResponseDto<PaymentTipSettlementDto> _responsePaymentTipSettlementDto;
        ResponseDto<PaymentDto> _responsePaymentDto;
        public PaymentImpl(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _responsePaymentDto = new ResponseDto<PaymentDto>();
        }
        public ResponseDto<PaymentDto> GetAllPayments(RequestDto requestDto)
        {
            _responsePaymentDto = _unitOfWork.Payment.GetAllPayments(requestDto);
            return _responsePaymentDto;
        }

        public ResponseDto<PaymentDto> GetMyPayments(int userId, RequestDto requestDto)
        {
            _responsePaymentDto = _unitOfWork.Payment.GetMyPayments(userId, requestDto);
            return _responsePaymentDto;
        }

        public ResponseDto<PaymentDto> GetMyPayments(Guid guid, RequestDto requestDto)
        {
            _responsePaymentDto = _unitOfWork.Payment.GetMyPayments(guid, requestDto);
            return _responsePaymentDto;
        }

        public ResponseDto<PaymentTipSettlementDto> GetPaymentTipSettlement(RequestDto requestDto)
        {
            _responsePaymentTipSettlementDto = _unitOfWork.Payment.GetPaymentTipSettlement(requestDto);
            return _responsePaymentTipSettlementDto;
        }

        public bool UpdatePaymentTipSettlement(PaymentTipSettlementDto paymentTipSettlementDto)
        {
            bool isUpdated = true;
            //List<Payment> paymentList = new List<Payment>();
            foreach (var p in paymentTipSettlementDto.PaymentSettlements)
             {
                var payment = _unitOfWork.Payment.GetById(p.PaymentId);
                payment.Data.PaymentSettlement = true;
                var response = _unitOfWork.Payment.Update(payment.Data);
                if (response.StatusCode != HttpStatusCode.OK)
                    return false;
            }
            foreach(var t in paymentTipSettlementDto.TipSettlements)
            {
                var tip = _unitOfWork.Tip.GetById(t.TipId);
                tip.Data.TipSettlement = true;
                var response=_unitOfWork.Tip.Update(tip.Data);
                if (response.StatusCode != HttpStatusCode.OK)
                    return false;
            }
            return isUpdated;
            //_responsePaymentTipSettlementDto = _unitOfWork.Payment.UpdatePaymentTipSettlement(paymentTipSettlementDto);
            //return _responsePaymentTipSettlementDto;
        }
    }
}
