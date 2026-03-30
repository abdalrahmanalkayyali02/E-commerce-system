using ECommerce.Application.Feature.Adminstration.ManegeShop.MarkAsViewSellerDocument.Command;
using ECommerce.Application.Feature.Adminstration.ManegeShop.MarkAsViewShopDocument.Command;
using ECommerce.Application.Feature.Adminstration.ManegeShop.VerfiedSeller.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller.Adminstration
{
    [ApiController]
    [Route("/admin/sellers")]
    public class SellerAdminstrationController : BaseApiController
    {
        private readonly IMediator _mediator;

        public SellerAdminstrationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPatch("{id}/verfied")]
        public async Task<IActionResult> VerfiedSeller([FromRoute]VerfiedSellerCommand command)
        {
            throw new   NotImplementedException();
        }

        [HttpPatch("{id}/view-shop-doc")]
        public async Task <IActionResult> viewShopDocument([FromRoute] MarkAsViewShopDocumentCommand command)
        {
            throw new NotImplementedException();
        }

        [HttpPatch("{id}/view-seller-doc")]
        public async Task<IActionResult> viewSellerDocument([FromRoute] MarkAsViewSellerDocumentCommand command)
        {
            throw new NotImplementedException();
        }






    }
}
