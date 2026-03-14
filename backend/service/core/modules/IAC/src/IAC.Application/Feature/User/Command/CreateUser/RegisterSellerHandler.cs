using IAC.Application.Abstraction;
using IAC.Application.Contract.Create_User.Response;
using IAC.Application.DTO.Create_User.Response;
using IAC.Domain.AggregateRoot;
using IAC.Domain.Enum;
using IAC.Domain.Repository.Read;
using IAC.Domain.Repository.Write;
using IAC.Domain.Value_Object;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IAC.Application.Feature.User.Command.CreateUser
{

    public sealed class RegisterSellerHandler : IRequestHandler<RegisterSellerCommand, RegisterSellerResponse>
    {
        private readonly IEmailGatway emailGatway;
        private readonly IPasswordService passwordService;             
        private readonly IUserReadRepository _readUserRepo;
        private readonly IUserWriteRepository _userWriteRepo;
        private readonly ISellerWriteRepository _sellerWriteRepo;


        public RegisterSellerHandler
            (IEmailGatway emailGatway, IPasswordService passwordService,
            IUserReadRepository readUserRepo, IUserWriteRepository userWriteRepo, ISellerWriteRepository sellerWriteRepo)
        {
            this.emailGatway = emailGatway;
            this.passwordService = passwordService;
            _readUserRepo = readUserRepo;
            _userWriteRepo = userWriteRepo;
            _sellerWriteRepo = sellerWriteRepo;
        }


        public  async Task<RegisterSellerResponse> Handler(RegisterSellerCommand request, CancellationToken cancellationToken)
        {
            var isEmailExist = await _readUserRepo.GetUserByEmail(request.Email);

            if (isEmailExist is not null)
            {
                throw new Exception("the email already used !!");
            }

            var userId = Guid.CreateVersion7();
            var hashedPassword = passwordService.PasswordHash(request.Password);

            var user = UserEntity.Create
                (
                   userId,
                   Name.FromStrict(request.FirstName),
                   Name.FromStrict(request.LastName),
                   Name.From(request.UserName),
                   DateOfBirth.From(request.DateOfBirth),
                   Email.From(request.Email),
                   PhoneNumber.From(request.PhoneNumber),
                  Password.From(hashedPassword),
                  UserRole.Seller
                );

            // registeration for seller 



            // make email service 



            // then send notification for the admin to verfied the  seller and shop


            return new  RegisterSellerResponse(userId, "The seller registered successfully");


        }
    }
}
