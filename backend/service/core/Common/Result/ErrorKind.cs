namespace Common.Result
{
    public enum ErrorKind
    {
        Failure = 0,
        Unexpected = 1,
        Validation = 2,
        Conflict = 3,
        NotFound = 4,
        Unauthorized = 5,
        Forbidden = 6
    }

    public readonly record struct Error
    {
        public string Code { get; }
        public string Description { get; }
        public ErrorKind Type { get; }

        private Error(string code, string description, ErrorKind type)
        {
            Code = code;
            Description = description;
            Type = type;
        }

        // Static factories for cleaner code
        public static Error NotFound(string code, string description) => new(code, description, ErrorKind.NotFound);
        public static Error Validation(string code, string description) => new(code, description, ErrorKind.Validation);
        public static Error Conflict(string code, string description) => new(code, description, ErrorKind.Conflict);
        public static Error Failure(string code, string description) => new(code, description, ErrorKind.Failure);

        public static readonly Error None = new(string.Empty, string.Empty, ErrorKind.Failure);
    }
}