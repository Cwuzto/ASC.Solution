using Microsoft.AspNetCore.Mvc;
using ASC.Web.Controllers;
using Microsoft.Extensions.Options;
using ASC.Web.Configuration;

namespace ASC.Web.Areas.ServiceRequests.Controllers
{
    [Area("ServiceRequests")]
    public class DashboardController : BaseController
    {
        private IOptions<ApplicationSettings> _settings;
        public DashboardController(IOptions<ApplicationSettings> settings)
        {
            _settings = settings;
        }

        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
