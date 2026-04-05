using Api.ViewModels.web.UserMangement.Request;
using ECommerce.Application.Feature.userMangement.User.Create.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller.UserMangement
{
    [ApiController]
  //  [ApiVersion("1.0")]
    [Route("register")]
    public class RegisterationController : BaseApiController
    {
        private readonly IMediator _mediator;

        public RegisterationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("customer")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CustomerRegister([FromForm] RegisterCustomerRequest request)
        {

            var imageStream = request.ProfilePhoto?.OpenReadStream();

            var command = new CreateCustomerCommand(
                request.FirstName,
                request.LastName,
                request.UserName,
                request.DateOfBirth,
                request.Email,
                request.PhoneNumber,
                request.Password,
                imageStream, 
                request.Address
            );
            var result = await _mediator.Send(command);
            return HandleResult(result);
        }

        [HttpPost("seller")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> SellerRegister([FromForm] RegisterSellerRequest request)
        {
            var profilePhotoimageStream = request.ProfilePhoto?.OpenReadStream();
            var ShopPhotoImageStream = request.shopPhoto.OpenReadStream();
            var VerfiedShopDocumentImageStream = request.verfiedShopDocument.OpenReadStream();
            var VerfiedSellerDocumentImageStream = request.verfiedSellerDocument.OpenReadStream();

            var command = new CreateSellerCommand(
               request.FirstName,
               request.LastName,
               request.UserName,
               request.DateOfBirth,
               request.Email,
               request.PhoneNumber,
               request.Password,
               profilePhotoimageStream,
               request.Address,
               request.shopName,
               ShopPhotoImageStream,
               VerfiedSellerDocumentImageStream,
               VerfiedShopDocumentImageStream
           );

            var result = await _mediator.Send(command);
            return HandleResult(result);
        }
    }
}