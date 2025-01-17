using AutoMapper;
using BusinessManagementSystem.BusinessLayer.Services;
using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Dto.Chart;
using BusinessManagementSystem.Models;
using BusinessManagementSystem.Services;
using Newtonsoft.Json;

namespace BusinessManagementSystem.BusinessLayer.Implementations
{
    public class DashboardImpl:IDashboardService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DashboardImpl(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
        }

        public string GetIncomeSegregation()
        {
            List<IncomeSegregationDataPoint> dataPoint = new List<IncomeSegregationDataPoint>();

            dataPoint.Add(new IncomeSegregationDataPoint("Payment", 403400));
            dataPoint.Add(new IncomeSegregationDataPoint("Tips", 57000));
            return JsonConvert.SerializeObject(dataPoint);
        }

        public string GetPaymentTipSegregation()
        {
            List<PaymentTipsDataPoint> dataPoints1 = new List<PaymentTipsDataPoint>();
            List<PaymentTipsDataPoint> dataPoints2 = new List<PaymentTipsDataPoint>();
            var Payments = _unitOfWork.Payment.GetAllPayments();
            var Tips = _unitOfWork.Tip.GetAllTips();
            foreach(var Payment in Payments)
            {
                dataPoints1.Add(new PaymentTipsDataPoint(Payment.FullName, Payment.TotalPaymentToArtist));
            }
            foreach(var Tip in Tips)
            {
                dataPoints2.Add(new PaymentTipsDataPoint(Tip.FullName, Tip.TotalPaymentToArtist));
            }

            

            //dataPoints1.Add(new PaymentTipsDataPoint("Sanik", 234000));
            //dataPoints1.Add(new PaymentTipsDataPoint("Laddu", 310000));
            //dataPoints1.Add(new PaymentTipsDataPoint("Dinesh", 170000));
            //dataPoints1.Add(new PaymentTipsDataPoint("Pramik", 120000));

            //dataPoints2.Add(new PaymentTipsDataPoint("Sanik", 20000));
            //dataPoints2.Add(new PaymentTipsDataPoint("Laddu", 20000));
            //dataPoints2.Add(new PaymentTipsDataPoint("Dinesh", 30000));
            //dataPoints2.Add(new PaymentTipsDataPoint("Pramik", 10000));

            return $"{JsonConvert.SerializeObject(dataPoints1)}##{JsonConvert.SerializeObject(dataPoints2)}";
        }
    }
}
