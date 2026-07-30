using TattooAppointmentSystem.Models;

namespace TattooAppointmentSystem.Services
{
    public interface IMenuRole : IGeneric<MenuRole>
    {
        dynamic GetRolesAssignedToMenu(int id);
    }
}

