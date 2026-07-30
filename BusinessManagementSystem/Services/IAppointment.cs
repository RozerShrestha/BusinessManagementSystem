using TattooAppointmentSystem.Models;

namespace TattooAppointmentSystem.Services
{
    public interface IAppointment:IGeneric<Appointment>
    {
        //int GetTotalCost(string category, int totalHours, int deposit, int discount, int discountInHour);
    }
}

