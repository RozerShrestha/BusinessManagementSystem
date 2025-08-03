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
        ResponseDto<PaymentHistory> _responsePaymentHistoryDto;
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

        public ResponseDto<PaymentHistory> GetPaymentHistory(RequestDto requestDto)
        {
            _responsePaymentHistoryDto=_unitOfWork.Payment.GetPaymentHistory(requestDto);
            return _responsePaymentHistoryDto;
        }

        public ResponseDto<PaymentTipSettlementDto> GetPaymentTipSettlement(RequestDto requestDto)
        {
            _responsePaymentTipSettlementDto = _unitOfWork.Payment.GetPaymentTipSettlement(requestDto);
            return _responsePaymentTipSettlementDto;
        }
        public bool UpdatePaymentTipSettlement(PaymentTipSettlementDto paymentTipSettlementDto)
        {
            bool isUpdated = true;
            List<Payment> paymentList = new List<Payment>();
            List<Tip> tipList=new List<Tip>();
            List<AdvancePayment> advancePaymentList = new List<AdvancePayment>();
            if (paymentTipSettlementDto.UserId != 0)
            {
                foreach (var p in paymentTipSettlementDto.PaymentSettlements)
                {
                    var payment = _unitOfWork.Payment.GetById(p.PaymentId);
                    payment.Data.PaymentSettlement = true;
                    paymentList.Add(payment.Data);
                }
                foreach (var t in paymentTipSettlementDto.TipSettlements)
                {
                    var tip = _unitOfWork.Tip.GetById(t.TipId);
                    tip.Data.TipSettlement = true;
                    tipList.Add(tip.Data);
                }
                foreach(var pa in paymentTipSettlementDto.AdvancePaymentSettlements)
                {
                    var advancePayment = _unitOfWork.AdvancePayment.GetById(pa.Id);
                    advancePayment.Data.AdvancePaymentSettlement = true;
                    advancePaymentList.Add(advancePayment.Data);
                }
                var responsePayment = _unitOfWork.Payment.UpdateAll(paymentList);
                var responseTip = _unitOfWork.Tip.UpdateAll(tipList);
                var responseAdvancePayment = _unitOfWork.AdvancePayment.UpdateAll(advancePaymentList);
                if (responsePayment.StatusCode == HttpStatusCode.OK && responseTip.StatusCode == HttpStatusCode.OK && responseAdvancePayment.StatusCode==HttpStatusCode.OK)
                {
                    PaymentHistory paymentHistory = new PaymentHistory
                    {
                        UserId = paymentTipSettlementDto.UserId,
                        TotalPayment = paymentTipSettlementDto.TotalPayments,
                        TotalTips = paymentTipSettlementDto.TotalTips,
                        TotalAdvancePayment = paymentTipSettlementDto.TotalAdvancePayments,
                        GrandTotal = paymentTipSettlementDto.GrandTotal,
                        PaidStatus = "Paid",
                        PaymentFrom = DateOnly.FromDateTime(paymentTipSettlementDto.StartDate),
                        PaymentTo = DateOnly.FromDateTime(paymentTipSettlementDto.EndDate)
                    };
                    var reponsePaymentHistory = _unitOfWork.Payment.CreatePaymentHistory(paymentHistory);
                }
                else
                {
                    foreach (var p in paymentTipSettlementDto.PaymentSettlements)
                    {
                        var payment = _unitOfWork.Payment.GetById(p.PaymentId);
                        payment.Data.PaymentSettlement = false;
                        paymentList.Add(payment.Data);
                    }
                    foreach (var t in paymentTipSettlementDto.TipSettlements)
                    {
                        var tip = _unitOfWork.Tip.GetById(t.TipId);
                        tip.Data.TipSettlement = false;
                        tipList.Add(tip.Data);
                    }
                    foreach (var pa in paymentTipSettlementDto.AdvancePaymentSettlements)
                    {
                        var advancePayment = _unitOfWork.AdvancePayment.GetById(pa.Id);
                        advancePayment.Data.AdvancePaymentSettlement = false;
                        advancePaymentList.Add(advancePayment.Data);
                    }
                    _unitOfWork.Payment.UpdateAll(paymentList);
                    _unitOfWork.Tip.UpdateAll(tipList);
                    _unitOfWork.AdvancePayment.UpdateAll(advancePaymentList);
                }
                    return isUpdated;
            }
            else
            {
                isUpdated = false;
                return isUpdated;
            }
        }
    }
}
