using BusinessManagementSystem.Models;

namespace BusinessManagementSystem.Services
{
    public interface IReferal : IGeneric<Referal>
    {
        dynamic ReferalList();
    }
}
