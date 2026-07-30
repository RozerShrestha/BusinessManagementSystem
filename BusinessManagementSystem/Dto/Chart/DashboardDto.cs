using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TattooAppointmentSystem.Dto.Chart
{
    public class DashboardDto
    {
        public int InProgress { get; set; }
        public int Scheduled_ReScheduled { get; set; }
        public int Confirmed { get; set; }
        public int Completed { get; set; }
    }
}

