namespace BusinessManagementSystem.BusinessLayer.Services
{
    public interface IBusinessLayer
    {
        IBaseService BaseService { get; }
        IUserService UserService { get; }
    }
}
