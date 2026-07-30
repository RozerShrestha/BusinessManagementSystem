using TattooAppointmentSystem.Models;

namespace TattooAppointmentSystem.Services
{
    public interface IReferal : IGeneric<Referal>
    {
        dynamic ReferalList();
    }
}

