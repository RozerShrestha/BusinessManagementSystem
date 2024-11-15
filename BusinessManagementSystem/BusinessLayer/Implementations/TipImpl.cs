using AutoMapper;
using BusinessManagementSystem.BusinessLayer.Services;
using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Models;
using BusinessManagementSystem.Services;

namespace BusinessManagementSystem.BusinessLayer.Implementations
{
    public class TipImpl : ITipService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        ResponseDto<Tip> _responseDto;
        public TipImpl(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _responseDto = new ResponseDto<Tip>();
        }
        public ResponseDto<Tip> GetAllTips()
        {
           var items= _unitOfWork.Tip.GetAll(includeProperties:"Appointment").Datas;
            foreach(var item in items)
            {
                var user = _unitOfWork.Users.GetFirstOrDefault(p => p.Id == item.TipAssignedToUser).Data;
                item.User = user;
                _responseDto.Datas.Add(item);
            }
            return _responseDto;
        }

        public ResponseDto<Tip> GetMyTips(int userId)
        {
            _responseDto = _unitOfWork.Tip.GetAll(p => p.TipAssignedToUser == userId);
            return _responseDto;
        }
    }
}
