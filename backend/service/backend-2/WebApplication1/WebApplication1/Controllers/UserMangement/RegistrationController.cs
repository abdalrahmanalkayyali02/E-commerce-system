using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using WebApplication1.DTOs;
using WebApplication1.Service.Interface;
using WebApplication1.Service.Interface.UserMangement;
using WebApplication1.Shared;

namespace WebApplication1.Controllers.UserMangement;
[ApiController]
[Route("register")]
public class RegistrationController : BaseApiController
{
    private readonly IRegistrationService _registrationService;
    
    public RegistrationController(IRegistrationService registeredServices)
    {
        _registrationService = registeredServices;
    }
    
    
    [HttpPost("customer")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> RegisterCustomer([FromForm]  CreateCustomerDtOs request, CancellationToken ct)
    {
        var result = await _registrationService.CreateCustomer(request, ct);
        return HandleResult(result);
    }

    [HttpPost("seller")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> RegisterSeller([FromForm] CreateSellerDtOs request, CancellationToken ct)
    {
        var result = await _registrationService.CreateSeller(request, ct);
        return HandleResult(result);
    }
    
}