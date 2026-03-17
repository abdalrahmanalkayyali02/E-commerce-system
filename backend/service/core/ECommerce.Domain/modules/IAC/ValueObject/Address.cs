using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.modules.IAC.ValueObject
{
    public  sealed record Address
    {
        public string Value { get;  init; }

        private Address() { }

        private Address (string value )
        {
            Value = value;
        }

        public static Address Create (string value)
        {

            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException("the address can not be null");
            }

            if (value.Length < 5 || value.Length > 150)
            {
                throw new ArgumentOutOfRangeException("the address must be at least 5 char and at most 150 char");
            }


            return new Address (value); 
        }

        public override string ToString() => Value;


    }
}
