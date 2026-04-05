using Api.ViewModels.web.Adminstration.Request;
using ECommerce.Application.Feature.Adminstration.ManegeShop.MarkAsViewSellerDocument.Command;
using ECommerce.Application.Feature.Adminstration.ManegeShop.MarkAsViewShopDocument.Command;
using ECommerce.Application.Feature.Adminstration.ManegeShop.VerfiedSeller.Command;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controller.Adminstration
{
    [ApiController]
    [Route("/admin/sellers")]
    [Authorize]
    public class SellerAdminstrationController : BaseApiController
    {
        private readonly IMediator _mediator;

        public SellerAdminstrationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPatch("{id}/verified")]
        public async Task<IActionResult> VerifiedSeller([FromRoute] Guid id) 
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var adminId))
            {
                return Unauthorized("User ID not found in token.");
            }

            var result = await _mediator.Send(new VerfiedSellerCommand(adminId, id));

            return HandleResult(result);
        }

        [HttpPatch("{id}/view-shop-doc")]
        public async Task <IActionResult> viewShopDocument([FromRoute] MarkAsViewDocumentCommand command)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var adminId))
            {
                return Unauthorized("User ID not found in token.");
            }

            var result = await _mediator.Send(new MarkAsViewSellerDocumentCommand(adminId, command.SellerId));
            return HandleResult(result);
        }

        [HttpPatch("{id}/view-seller-doc")]
        public async Task<IActionResult> viewSellerDocument([FromRoute] MarkAsViewDocumentCommand command)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var adminId))
            {
                return Unauthorized("User ID not found in token.");
            }

            var result = await _mediator.Send(new MarkAsViewSellerDocumentCommand(adminId, command.SellerId));
            return HandleResult(result);
        }






    }
}
