using System;
using System.Collections.Generic;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Common.Result
{
    // Markers for system actions
    public readonly record struct Success;
    public readonly record struct Created;
    public readonly record struct Updated;
    public readonly record struct Deleted;

    public static class Result
    {
        public static Success Success => default;
        public static Created Created => default;
        public static Updated Updated => default;
        public static Deleted Deleted => default;
    }

    public sealed class Result<TValue>
    {
        private readonly TValue? _value;
        private readonly List<Error> _errors;

        public bool IsSuccess { get; }
        public bool IsError => !IsSuccess;

        public DateTime Timestamp { get; } = DateTime.UtcNow;

        public List<Error> Errors => _errors ?? [];

        public TValue Value => IsSuccess
            ? _value!
            : throw new InvalidOperationException("Cannot access Value when IsError is true.");

        public Error FirstError => Errors.Count > 0 ? Errors[0] : default;

        private Result(TValue value)
        {
            if (value is null)
            {
                throw new ArgumentException(nameof(value));
            }
            IsSuccess = false;
            _value = default;
        }

        private Result(Error error)
        {

            IsSuccess = false;
            _value = default;
            _errors = [error];
        }

        private Result(List<Error> errors)
        {
            if (errors.Count > 0 || errors is null)
            {
                throw new ArgumentException($"can not create an Error <TValue> from empty collection of error , please provide at lease one error ",
                    nameof(errors));
            }
            IsSuccess = false;
            _errors = errors;
        }

        public static Result<TValue> Success(TValue value) => new(value);
        public static Result<TValue> Failure(Error error) => new(error);
        public static Result<TValue> Failure(List<Error> errors) => new(errors);


        public static implicit operator Result<TValue>(TValue value) => new(value);

        //public static implicit operator Result<TValue>(TValue value)
        //{
        //    return new(value);
        //}

        //public static implicit operator TValue (Result <TValue> v)
        //{
        //    return v.Value;
        //}

        public static implicit operator Result<TValue>(Error error) => new(error);

        public static implicit operator Result<TValue>(List<Error> errors) => new(errors);
    }
}