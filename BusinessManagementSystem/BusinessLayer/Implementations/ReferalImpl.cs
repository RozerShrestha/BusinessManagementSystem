using AutoMapper;
using TattooAppointmentSystem.BusinessLayer.Services;
using TattooAppointmentSystem.Dto;
using TattooAppointmentSystem.Models;
using TattooAppointmentSystem.Services;
using System.Linq.Expressions;

namespace TattooAppointmentSystem.BusinessLayer.Implementations
{
    public class ReferalImpl : IReferalService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        ResponseDto<Referal> _responseDto;

        public ReferalImpl(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _responseDto = new ResponseDto<Referal>();

        }

        public ResponseDto<Referal> CreateReferal(Referal referal)
        {
            throw new NotImplementedException();
        }

        public ResponseDto<Referal> DeleteReferalById(int id)
        {
            throw new NotImplementedException();
        }

        public ResponseDto<Referal> GetAllReferal()
        {
            throw new NotImplementedException();
        }

        public ResponseDto<Referal> GetReferalById(int id)
        {
            throw new NotImplementedException();
        }

        public ResponseDto<Referal> GetReferalByStatus(string status)
        {
            throw new NotImplementedException();
        }

        public ResponseDto<Referal> UpdateReferal(Referal referal)
        {
            throw new NotImplementedException();
        }
        public dynamic GetAllActiveReferalList()
        {
            return _unitOfWork.Referal.ReferalList();
        }
    }
}

