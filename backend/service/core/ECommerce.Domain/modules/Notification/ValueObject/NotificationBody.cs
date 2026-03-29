using Common.Impl.Result;
using Common.Result;
using Microsoft.Extensions.Primitives;
using System.Text.RegularExpressions;

namespace ECommerce.Domain.modules.Notification.ValueObject
{
    public record NotificationBody
    {
        public string Value { get; }
        private static readonly Regex AllowedPattern = new Regex(@"^[a-zA-Z0-9!]+( [& ]?[a-zA-Z0-9!]+)*$");

        private NotificationBody(string value) => Value = value;

        public static Result<NotificationBody> Create(string body)
        {
            if (string.IsNullOrWhiteSpace(body))
                return Result<NotificationBody>.Failure(Error.Validation("Body.Empty", "Body cannot be empty."));

            if (body.Length > 500)
                return Result<NotificationBody>.Failure(Error.Validation("Body.Length", "Body too long."));

            if (!AllowedPattern.IsMatch(body))
                return Result<NotificationBody>.Failure(Error.Validation("Body.Format",
                    "Body format invalid. Check for double spaces or unauthorized special characters."));

            return Result<NotificationBody>.Success(new NotificationBody(body));
        }

        public static NotificationBody Reconstrcter(string value)
        {
            return new NotificationBody(value);
        }

        public static implicit operator string(NotificationBody body) => body.Value;
    }
}