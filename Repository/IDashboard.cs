using Shuddhi.Model;

namespace Shuddhi.Repository
{
    public interface IDashboard
    {
        Task<DashboardModel?> GetDashboard(string userId);
    }
}
