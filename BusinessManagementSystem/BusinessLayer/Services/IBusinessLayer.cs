using BusinessManagementSystem.Services;

namespace BusinessManagementSystem.BusinessLayer.Services
{
    public interface IBusinessLayer
    {
        IBaseService BaseService { get; }
        IUserService UserService { get; }
        IBasicConfigurationService BasicConfigurationService { get; }
        IMenuService MenuService { get; }
        IRoleService RoleService { get; }
        IAppointmentService AppointmentService { get; }
        IReferalService ReferalService { get; }
        ITipService TipService { get; }
        IPaymentService PaymentService { get; }
    }
}
