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
        public ResponseDto<TipDto> _responseTipDto;
        public TipImpl(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _responseDto = new ResponseDto<Tip>();
            _responseTipDto = new ResponseDto<TipDto>();
        }
        public ResponseDto<TipDto> GetAllTips()
        {
            _responseTipDto = _unitOfWork.Tip.GetAllTips();
            return _responseTipDto;
        }

        public ResponseDto<TipDto> GetMyTips(int userId)
        {
            _responseTipDto = _unitOfWork.Tip.GetMyTips(userId);
            return _responseTipDto;
         }
    }
}
