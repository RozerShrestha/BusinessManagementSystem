using BusinessManagementSystem.BusinessLayer.Services;
using BusinessManagementSystem.Data;
using BusinessManagementSystem.Services;
using Microsoft.EntityFrameworkCore.Storage;

namespace BusinessManagementSystem.BusinessLayer.Implementations
{
    public class BusinessLayerImpl : IBusinessLayer
    {
        protected readonly IUnitOfWork _unitOfWork=null;
        public BusinessLayerImpl(IUnitOfWork unitOfWork, ApplicationDBContext dBContext) 
        { 
            _unitOfWork = unitOfWork;
            BaseService = new BaseImpl(_unitOfWork);
            UserService = new UserImpl(_unitOfWork);
            BasicConfigurationService = new BasicConfigurationImpl(_unitOfWork);
            MenuService = new MenuImpl(_unitOfWork);
        }
        public IBaseService BaseService { get; private set; }

        public IUserService UserService { get; private set; }

        public IBasicConfigurationService BasicConfigurationService { get; private set; }

        public IMenuService MenuService { get; private set; }

    }
}
