using Common.Impl.Result;
using Common.Result;
using ECommerce.Application.Abstraction.Data;
using ECommerce.Application.Abstraction.IExternalService;
using ECommerce.Application.Feature.Catalog.Product.Create.Command;
using ECommerce.Domain.modules.Catalog.Entity;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Application.Feature.Catalog.Product.Create.Handler
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateProductCommand> _validator;
        private readonly IFileStorgeService _fileStorageService;

        public CreateProductCommandHandler(
            IUnitOfWork unitOfWork,
            IValidator<CreateProductCommand> validator,
            IFileStorgeService fileStorageService)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
            _fileStorageService = fileStorageService;
        }

        public async Task<Result<Guid>> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
            {
                return Result<Guid>.Failure(validationResult.Errors
                    .Select(e => Error.Validation(e.PropertyName, e.ErrorMessage)).ToList());
            }

            try
            {
                var photoEntities = new List<ProductPhotoEntity>();

                foreach (var photoDto in command.ProductPhotos)
                {
                    string uploadedUrl = await _fileStorageService.UploadAsync(photoDto.FileStream);

                    var photoResult = ProductPhotoEntity.Create(
                        Guid.NewGuid(),
                        uploadedUrl,
                        photoDto.AltText,
                        photoDto.IsMain);

                    if (photoResult.IsError) return Result<Guid>.Failure(photoResult.Errors);

                    photoEntities.Add(photoResult.Value);
                }

                var productResult = ProductEntity.Create(
                    command.CategoryId,
                    command.SellerId,
                    command.ProductName,
                    command.ProductDescription,
                    command.BasePrice,
                    photoEntities);

                if (productResult.IsError) return Result<Guid>.Failure(productResult.Errors);

                

                var product = productResult.Value;

                foreach (var attrDto in command.ProductAttributes)
                {
                    var attrResult = ProductAttributeEntity.Create(
                        Guid.NewGuid(),
                        attrDto.Name,
                        attrDto.Value,
                        attrDto.IsFilterable,
                        attrDto.IsVariant,
                        attrDto.DisplayOrder,
                        attrDto.Unit);

                    if (attrResult.IsError) return Result<Guid>.Failure(attrResult.Errors);

                    product.AddAttribute(attrResult.Value);
                }

                await _unitOfWork.productRepository.AddAsync(product, cancellationToken);
                
                var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);

                if (saveResult <= 0)
                {
                    return Result<Guid>.Failure(Error.Failure("Database.SaveError", "Failed to save product."));
                }

                return Result<Guid>.Success(product.Id);
            }
            catch (Exception ex)
            {
                return Result<Guid>.Failure(Error.Failure("Server.Exception", ex.Message));
            }
        }
    }
}