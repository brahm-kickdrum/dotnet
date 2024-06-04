using Assessment_1.Mappers;
using Assessment_1.Services.IServices;
using Assessment_1.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Assessment_1.Controllers
{
    [Route("api/driver")]
    [ApiController]
    public class DriverContoller : ControllerBase
    {
        private IDriverService _driverService;

        public DriverContoller(IDriverService driverService)
        {
            _driverService = driverService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public ActionResult<Guid> RegisterDriver(DriverRegisterRequestViewModel driverRegisterRequestViewModel)
        {
            Guid guid = _driverService.AddDriver(DriverMapper.ConvertDriverRegisterRequestViewModelToDriver(driverRegisterRequestViewModel));
            return Ok(guid);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult<string> Logindriver(DriverLoginRequestViewModel driverLoginRequestViewModel)
        {
            string token = _driverService.LoginDriver(driverLoginRequestViewModel.Email, driverLoginRequestViewModel.Password);
            return Ok(token);
        }
    }
}
