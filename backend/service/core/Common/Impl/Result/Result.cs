
using Common.Result;

namespace Common.Impl.Result
{
    public readonly record struct Success;
    public readonly record struct Created;
    public readonly record struct Updated;
    public readonly record struct Deleted;

    public sealed class Result<TValue>
    {
        private readonly TValue? _value;
        private readonly List<Error> _errors = [];

        public bool IsSuccess { get; }
        public bool IsError => !IsSuccess;
        public DateTime Timestamp { get; } = DateTime.UtcNow;
        public List<Error> Errors => _errors;

        public TValue Value => IsSuccess
            ? _value!
            : throw new InvalidOperationException("Cannot access Value when IsError is true.");

        private Result(TValue value)
        {
            _value = value ?? throw new ArgumentNullException(nameof(value));
            IsSuccess = true;
        }




        private Result(Error error)
        {
            IsSuccess = false;
            _errors = [error];
        }

        private Result(List<Error> errors)
        {
            if (errors == null || errors.Count == 0)
            {
                throw new ArgumentException("Must provide at least one error.", nameof(errors));
            }
            IsSuccess = false;
            _errors = errors;
        }

        public TNextValue Match<TNextValue>(Func<TValue, TNextValue> onValue, Func<List<Error>, TNextValue> onError)
            => IsSuccess ? onValue(_value!) : onError(_errors);

        public static Result<TValue> Success(TValue value) => new(value);
        public static Result<TValue> Failure(Error error) => new(error);
        public static Result<TValue> Failure(List<Error> errors) => new(errors);

        // Operators
        public static implicit operator Result<TValue>(TValue value) => new(value);
        public static implicit operator Result<TValue>(Error error) => new(error);
        public static implicit operator Result<TValue>(List<Error> errors) => new(errors);
    }
}