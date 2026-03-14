using IAC.Infrastructure.Persistence.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Text;

namespace IAC.Api.Controller
{
    [ApiController]
    [Route("/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public AuthController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        [HttpPost("/login")]
        public Task <IActionResult> Login([FromBody] string t)
        {

        }

    }
}
