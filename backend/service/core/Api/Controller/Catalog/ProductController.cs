//using ECommerce.Application.Feature.Catalog.Product.Create.Command;
//using ECommerce.Infrastructure.Persistence;
//using MediatR;
//using Microsoft.AspNetCore.Mvc;
//using Org.BouncyCastle.Asn1.Ocsp;

//namespace Api.Controller.Catalog
//{
//    [ApiController]
//    [Route("/products")]
//    public class ProductController : BaseApiController
//    {
//        private readonly IMediator _mediator;

//        public ProductController(IMediator mediater)
//        {
//           _mediator = mediater;
//        }

//        [HttpPost]
//        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
//        {
//            var result = await _mediator.Send(command);
//            return HandleResult(result);
//        }



//    }
//}
