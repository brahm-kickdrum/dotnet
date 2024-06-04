using Assessment_1.Mappers;
using Assessment_1.Services.IServices;
using Assessment_1.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Assessment_1.Controllers
{
    [Route("api/rider")]
    [ApiController]
    public class RiderController : ControllerBase
    {
        private IRiderService _riderService;

        public RiderController(IRiderService riderService)
        {
            _riderService = riderService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public ActionResult<Guid> RegisterRider(RiderRegisterRequestViewModel riderRegisterRequestView)
        {
            Guid guid = _riderService.AddRider(RiderMapper.ConvertRiderRegisterRequestViewModelToRider(riderRegisterRequestView));
            return Ok(guid);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult<string> LoginRider(RiderLoginRequestViewModel riderLoginRequestViewModel)
        {
            string token = _riderService.LoginRider(riderLoginRequestViewModel.Email, riderLoginRequestViewModel.Password); 
            return Ok(token);
        }

    }
}
