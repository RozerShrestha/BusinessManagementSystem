using AutoMapper;
using BusinessManagementSystem.BusinessLayer.Services;
using BusinessManagementSystem.Data;
using BusinessManagementSystem.Services;
using Microsoft.EntityFrameworkCore.Storage;

namespace BusinessManagementSystem.BusinessLayer.Implementations
{
    public class BusinessLayerImpl : IBusinessLayer
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;
        public BusinessLayerImpl(IUnitOfWork unitOfWork, IMapper mapper, ApplicationDBContext dBContext) 
        { 
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            BaseService = new BaseImpl(_unitOfWork);
            UserService = new UserImpl(_unitOfWork, mapper);
            BasicConfigurationService = new BasicConfigurationImpl(_unitOfWork);
            MenuService = new MenuImpl(_unitOfWork);
            RoleService=new RoleImpl(_unitOfWork);
            AppointmentService = new AppointmentImpl(_unitOfWork, mapper);
            ReferalService = new ReferalImpl(_unitOfWork, mapper);
            TipService = new TipImpl(_unitOfWork, mapper);
            PaymentService = new PaymentImpl(_unitOfWork, mapper);
            DashboardService=new DashboardImpl(_unitOfWork, mapper);
            AdvancePaymentService=new AdvancePaymentImpl(_unitOfWork, mapper);
        }
        public IBaseService BaseService { get; private set; }

        public IUserService UserService { get; private set; }

        public IBasicConfigurationService BasicConfigurationService { get; private set; }

        public IMenuService MenuService { get; private set; }
        public IRoleService RoleService { get; private set; }
        public IAppointmentService AppointmentService { get; private set; }
        public IReferalService ReferalService { get; private set; }
        public ITipService TipService { get; private set; }
        public IPaymentService PaymentService { get; private set; }
        public IDashboardService DashboardService { get; private set; }

        public IAdvancePaymentService AdvancePaymentService { get; private set; }
    }
}
