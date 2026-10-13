using Microsoft.AspNetCore.Mvc;
using Shuddhi.Model;
using Shuddhi.Repository;

namespace Shuddhi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : Controller
    {
        private readonly IDashboard _dashboard;

        public DashboardController(IDashboard dashboard)
        {
            _dashboard = dashboard;
        }

       
        

        [HttpPost("Rewards")]
        public async  Task<DashboardModel> Dashboard(string UserId)
        {
            DashboardModel dashboardModel = new DashboardModel();
            dashboardModel= await _dashboard.GetDashboard(UserId);

            return dashboardModel;
        }
    }
}
