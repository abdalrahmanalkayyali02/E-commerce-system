using IAC.Application.Contract.Create_User.Response;
using MediatR;
namespace IAC.Application.Contract.Create_User.Request
{
    public record IRegisterCustomerRequest
    (
          string FirstName,
          string LastName,
          string UserName,
          string Email,
          string PhoneNumber,
          string DateOfBirth,
          string Password,
          string Address
    );
}
