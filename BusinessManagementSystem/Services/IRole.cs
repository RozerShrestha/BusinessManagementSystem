using TattooAppointmentSystem.Models;

namespace TattooAppointmentSystem.Services
{
    public interface IRole : IGeneric<Role>
    {
        dynamic GetRoles();
    }
}

