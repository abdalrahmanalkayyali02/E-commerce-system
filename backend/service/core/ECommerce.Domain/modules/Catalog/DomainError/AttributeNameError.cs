using Common.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.modules.Catalog.DomainError
{
    public static class AttributeNameError
    {
        public static readonly Error Required = Error.Validation(
            "AttributeName.Require", "Product Attribute Name is mandatory");

        public static readonly Error InvalidLength = Error.Validation(
            "AttributeName.InvalidLength", "Product Attribute Name must be between 1 and 50 characters.");

        public static readonly Error InvalidFormat = Error.Validation(
            "AttributeName.InvalidFormat", "Product Attribute Name must start and end with an alphanumeric character, and can only contain single spaces between words.");
    }
}
