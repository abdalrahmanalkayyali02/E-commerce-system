using Common.DTOs.UserMangement.User;
using Common.Enum;
using Common.Exceptions.System.userMangement;
using Common.Impl.Result;
using Common.Result;
using ECommerce.Application.Abstraction.Data;
using ECommerce.Application.Abstraction.IExternalService;
using ECommerce.Application.Feature.userMangement.User.UpdateForgetPassword.Command;
using ECommerce.Domain.modules.UserMangement.ValueObject;
using FluentValidation;
using MediatR;

namespace ECommerce.Application.Feature.userMangement.User.UpdateForgetPassword.Handler
{
    public class UpdateForgetPasswordCommandHandler : IRequestHandler<updateForgetPasswordCommand, Result<UpdateForgetPasswordReponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordService _passwordService;
        private readonly IJWTService _jwtService;
        private readonly IValidator<updateForgetPasswordCommand> _validator;

        public UpdateForgetPasswordCommandHandler(
            IUnitOfWork unitOfWork,
            IPasswordService passwordService,
            IJWTService jwtService,
            IValidator<updateForgetPasswordCommand> validator)
        {
            _unitOfWork = unitOfWork;
            _passwordService = passwordService;
            _jwtService = jwtService;
            _validator = validator;
        }

        public async Task<Result<UpdateForgetPasswordReponse>> Handle(updateForgetPasswordCommand command, CancellationToken ct)
        {
            var validationResult = await _validator.ValidateAsync(command, ct);
            if (!validationResult.IsValid)
            {
                return Result<UpdateForgetPasswordReponse>.Failure(
                    Error.Validation("Input.Invalid", validationResult.Errors.First().ErrorMessage));
            }

            var userEntity = await _unitOfWork.Users.GetUserByEmail(command.email, ct);
            if (userEntity is null)
            {
                return Result<UpdateForgetPasswordReponse>.Failure(EmailAppError.NotFound);
            }

            var passwordVoResult = Password.From(command.password);
            if (passwordVoResult.IsError)
            {
                return Result<UpdateForgetPasswordReponse>.Failure(passwordVoResult.Errors);
            }

            string hashedPassword = _passwordService.PasswordHash(passwordVoResult.Value.Value);
            var hashedVo = Password.Reconstruct(hashedPassword);

            var resetResult = userEntity.CompletePasswordReset(hashedVo);

            if (resetResult.IsError)
            {
                return Result<UpdateForgetPasswordReponse>.Failure(resetResult.Errors);
            }

            _unitOfWork.Users.Update(userEntity);
            var saveResult = await _unitOfWork.SaveChangesAsync(ct);

            if (saveResult <= 0)
            {
                return Result<UpdateForgetPasswordReponse>.Failure(
                    Error.Failure("Database.Error", "Failed to finalize password update."));
            }

            var token = _jwtService.GenerateToken(userEntity.Id.ToString(), userEntity.userType.ToString());

            return Result<UpdateForgetPasswordReponse>.Success(new UpdateForgetPasswordReponse(
                "Password updated successfully. Your session is now secure.",
                token,
                userEntity.IsEmailConfirmed,
                userEntity.AccountStatus));
        }
    }
}