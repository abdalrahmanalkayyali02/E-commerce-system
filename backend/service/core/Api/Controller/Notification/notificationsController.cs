using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller.Notification
{
    [ApiController]
    [Route("/notification")]
    [Authorize]
    public class notificationsController : BaseApiController
    {
        private readonly IMediator _mediater;

        public notificationsController(IMediator mediator)
        {
            _mediater = mediator;
        }

        
        

    }
}
