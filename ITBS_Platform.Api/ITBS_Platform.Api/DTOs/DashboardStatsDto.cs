namespace ITBS_Platform.Api.DTOs
{
    public class DashboardStatsDto
    {
        public int TotalEvents { get; set; }
        public int TotalCourses { get; set; }
        public int ActiveCourses { get; set; }
        public int PendingRegistrations { get; set; } // demandes en attente
        public int TotalUsers { get; set; }
    }
}
