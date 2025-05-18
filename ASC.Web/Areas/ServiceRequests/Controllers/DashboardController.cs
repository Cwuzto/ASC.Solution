using Microsoft.AspNetCore.Mvc;
using ASC.Web.Controllers;
using Microsoft.Extensions.Options;
using ASC.Web.Configuration;
using Business.Interfaces;
using ASC.Web.Data;
using Model.BaseTypes;
using Model.Models;
using Utilities;
using ASC.Web.Areas.ServiceRequests.Models;

namespace ASC.Web.Areas.ServiceRequests.Controllers
{
    [Area("ServiceRequests")]
    public class DashboardController : BaseController
    {
        //private IOptions<ApplicationSettings> _settings;
        private readonly IServiceRequestOperations _serviceRequestOperations;
        private readonly IMasterDataOperations _masterData;
        public DashboardController(IServiceRequestOperations operations, IMasterDataOperations masterData)
        {
            _serviceRequestOperations = operations;
            _masterData = masterData;
        }
        public async Task<IActionResult> Dashboard()
        {
            // List of Status which were to be queried.
            var status = new List<string>
            {
                Status.New.ToString(),
                Status.InProgress.ToString(),
                Status.Initiated.ToString(),
                Status.RequestForInformation.ToString()
            };

            List<ServiceRequest> serviceRequests = new List<ServiceRequest>();

            if (HttpContext.User.IsInRole(Roles.Admin.ToString()))
            {
                serviceRequests = await _serviceRequestOperations
                    .GetServiceRequestsByRequestedDateAndStatus(
                        DateTime.UtcNow.AddDays(-7), 
                        status);
            }
            else if (HttpContext.User.IsInRole(Roles.Engineer.ToString()))
            {
                serviceRequests = await _serviceRequestOperations
                    .GetServiceRequestsByRequestedDateAndStatus(
                        DateTime.UtcNow.AddDays(-7),
                        status,
                        serviceEngineerEmail: HttpContext.User.GetCurrentUserDetails().Email);
            }
            else
            {
                serviceRequests = await _serviceRequestOperations
                    .GetServiceRequestsByRequestedDateAndStatus(
                        DateTime.UtcNow.AddYears(-1),
                        status,
                        email: HttpContext.User.GetCurrentUserDetails().Email);
            }

            return View(new DashboardViewModel
            {
                ServiceRequests = serviceRequests.OrderByDescending(p => p.RequestedDate).ToList()
            });
        }
    }
}
