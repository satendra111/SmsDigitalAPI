using API.Model;
using AutoMapper;
using BAL.Abstract;
using Core.Concrete;
using Domain.Dto;
using Domain.EntityModel;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;
        private readonly IMapper mapper;
        public AccountController(
            IAccountService accountService,
            IMapper mapper
           
            )
        {
            this.accountService = accountService;
            this.mapper = mapper;
            

        }
        [HttpPost("SignIn")]
        public async Task<ActionResult<ApiResponseModel>> Login(LoginRequestModel loginRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponseModel(ErrorCodes.InvalidRequest));

            var item = await accountService.SignInAsync(loginRequest.UserName, loginRequest.Password);
            return Ok(new ApiResponseModel(item));

        }
       

    }
}
