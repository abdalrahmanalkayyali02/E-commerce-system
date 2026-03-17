//using ECommerce.Application.Abstraction.IExternalService;
//using ECommerce.Application.DTO.IAC.User.Response;
//using ECommerce.Application.Feature.IAC.Commands.Login.Command;
//using FluentValidation;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace ECommerce.Application.Feature.IAC.Commands.Login.Handler
//{
//    public class UserLoginCommandHandler 
//    {
//        private readonly IUserReadRepository _userService;
//        private readonly IPasswordService _passwordService;
//        private readonly IJWTService _jWTService;
//        private IValidator<LoginUserCommand> _validator;

//        public UserLoginCommandHandler(IUserReadRepository userService,IPasswordService _passwordService, IJWTService jWTService, IValidator<LoginUserCommand> _validator)
//        { 
//            _userService = userService;
//            this._passwordService = _passwordService;
//            _jWTService = jWTService;
//            this._validator = _validator;
//        }

//        //public async Task<LoginUserResponse> Handle(LoginUserCommand command, CancellationToken cancellation)
//        //{
//        //    var validationResult = await _validator.ValidateAsync(command,cancellation);

//        //    if (!validationResult.IsValid)
//        //    {
//        //     //   return new LoginUserResponse(validationResult.Errors.First().ErrorMessage);
//        //    }




//        //}

        


//    }
//}
