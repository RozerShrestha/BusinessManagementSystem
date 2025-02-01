using AutoMapper;
using BusinessManagementSystem.BusinessLayer.Services;
using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Dto.Chart;
using BusinessManagementSystem.Enums;
using BusinessManagementSystem.Models;
using BusinessManagementSystem.Services;
using BusinessManagementSystem.Utility;
using Newtonsoft.Json;

namespace BusinessManagementSystem.BusinessLayer.Implementations
{
    public class DashboardImpl:IDashboardService
    {
        private readonly IUnitOfWork _unitOfWork;
        ResponseDto<DashboardDto> _responseDashboardDto;

        public DashboardImpl(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _responseDashboardDto = new ResponseDto<DashboardDto>();
        }

        public ResponseDto<DashboardDto> GetDashboardInfo(RequestDto requestDto, int userId)
        {
            return _unitOfWork.Dashboard.GetDashboardInfo(requestDto, userId);
        }

        public ResponseDto<DashboardDto> GetDashboardInfoAllEmployee(RequestDto requestDto)
        {
            return _unitOfWork.Dashboard.GetDashboardInfoAllEmployee(requestDto);
        }

        public string GetIncomeSegregation(RequestDto requestDto)
         {
            List<IncomeSegregationDataPoint> dataPoint = new List<IncomeSegregationDataPoint>();
            var paymentInfo = _unitOfWork.Payment.GetAllPaymentSegregation(requestDto);
            var tipInfo = _unitOfWork.Tip.GetTipsSegregation(requestDto);
            dataPoint.Add(new IncomeSegregationDataPoint("Total Payment To Artist",paymentInfo==null? 0 : paymentInfo.TotalPaymentToArtist));
            dataPoint.Add(new IncomeSegregationDataPoint("Total Payment To Studio", paymentInfo == null ? 0 : paymentInfo.TotalPaymentToStudio));
            dataPoint.Add(new IncomeSegregationDataPoint("Total Tips", tipInfo == null ? 0 : tipInfo.TotalTips));
            return JsonConvert.SerializeObject(dataPoint);
        }

        public string GetPaymentTipSegregation(RequestDto requestDto)
        {
            List<PaymentTipsDataPoint> dataPoints1 = new List<PaymentTipsDataPoint>();
            List<PaymentTipsDataPoint> dataPoints2 = new List<PaymentTipsDataPoint>();
            var users = _unitOfWork.Users.GetAll(p => p.Occupation == SD.Occupations[Occupation.TattooArtist.ToString()] && p.Status == true).Datas;
            
            var Payments = _unitOfWork.Payment.GetAllPaymentsDashboard(requestDto);
            var Tips = _unitOfWork.Tip.GetAllTipsDashboard(requestDto);
            foreach(var Payment in Payments)
            {
                dataPoints1.Add(new PaymentTipsDataPoint(Payment.FullName, Payment.TotalPaymentToArtist));
            }
            foreach(var Tip in Tips)
            {
                dataPoints2.Add(new PaymentTipsDataPoint(Tip.FullName, Tip.TotalPaymentToArtist));
            }
            foreach(var user in users)
            {
                if (!dataPoints1.Any(d => d.Label == user.FullName))
                {
                    dataPoints1.Add(new PaymentTipsDataPoint(user.FullName, 0));
                }
                if (!dataPoints2.Any(d => d.Label == user.FullName))
                {
                    dataPoints2.Add(new PaymentTipsDataPoint(user.FullName, 0));
                }
            }
            dataPoints1 = dataPoints1.OrderBy(dp => dp.Label).ToList();
            dataPoints2 = dataPoints2.OrderBy(dp => dp.Label).ToList();

            return $"{JsonConvert.SerializeObject(dataPoints1)}##{JsonConvert.SerializeObject(dataPoints2)}";
        }
    }
}
