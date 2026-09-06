using DannyGoodacre.Primitives;

namespace Blackjack.Application;

internal static class ValidationStateExtensions
{
    extension(ValidationState state)
    {
        public bool IsNonEmptyGuid(Guid value, string name)
        {
            if (value != Guid.Empty)
            {
                return true;
            }

            state.AddError(name, "Must not be empty.");

            return false;
        }

        public bool IsNotNullEmptyOrWhitespace(string? value, string name)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                return true;
            }

            state.AddError(name, "Must not be null, empty, or whitespace.");

            return false;
        }
    }
}
