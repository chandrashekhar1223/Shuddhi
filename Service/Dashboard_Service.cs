using Shuddhi.Database;
using Shuddhi.Model;
using Shuddhi.Repository;

namespace Shuddhi.Service
{
    public class Dashboard_Service : IDashboard

    {
        IConfiguration configuration = new ConfigurationBuilder()
.SetBasePath(Directory.GetCurrentDirectory())
.AddJsonFile("appsettings.json")
.Build();
        public async Task<DashboardModel?> GetDashboard(string userId)
        {
            DashboardModel dashboardModel = new DashboardModel();
            DashboardDB
                dashboardDB = new DashboardDB(configuration);

            dashboardModel =await dashboardDB.GetDashboard(userId);
           

            return dashboardModel;
        }
    }
}
