using IAC.Application.Contract.Create_User.Request;
using IAC.Application.Contract.Create_User.Response;
using IAC.Infrastructure.Persistence.DB;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace IAC.Api.Controller
{
    [ApiController]
    [Route("/register")]
    public class RegisterationController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        RegisterationController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        //[HttpPost("/customer")]
        //public async Task<IActionResult> RegisterCustomer([FromBody] IRegisterCustomerRequest  request)
        //{

        //}


        [HttpPost]
        public async Task <ActionResult<IRegisterCustomerRequest>> RegisterCusomter([FromBody] IRegisterCustomerRequest request)
        {
            
        }
        public async Task<IActionResult> RegisterSeller([FromBody] request)
        {

        }

    }
}
