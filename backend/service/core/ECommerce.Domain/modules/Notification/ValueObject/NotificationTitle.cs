using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.modules.Notification.ValueObject
{
    using Common.Impl.Result;
    using Common.Result;
    using System.Text.RegularExpressions;

    namespace ECommerce.Domain.modules.Notification.ValueObjects
    {
        public record NotificationTitle
        {
            public string Value { get; }

            private static readonly Regex AllowedPattern = new Regex(@"^[a-zA-Z0-9!]+( [& ]?[a-zA-Z0-9!]+)*$");

            private NotificationTitle(string value) => Value = value;

            public static Result<NotificationTitle> Create(string title)
            {
                if (string.IsNullOrWhiteSpace(title))
                    return Result<NotificationTitle>.Failure(Error.Validation("Title.Empty", "Title cannot be empty."));

                if (title.Length > 100)
                    return Result<NotificationTitle>.Failure(Error.Validation("Title.Length", "Title too long."));

                if (!AllowedPattern.IsMatch(title))
                    return Result<NotificationTitle>.Failure(Error.Validation("Title.Format",
                        "Title contains invalid characters or incorrect spacing. Only '!', '&', and single spaces are allowed between words."));

                return Result<NotificationTitle>.Success(new NotificationTitle(title));
            }


            public static NotificationTitle Reconstructor(string value)
            {
                return new NotificationTitle(value);
            }

            public static implicit operator string(NotificationTitle title) => title.Value;
        }
    }
}
