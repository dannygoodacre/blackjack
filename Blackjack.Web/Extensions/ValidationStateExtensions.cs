using DannyGoodacre.Primitives;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Blackjack.Web;

internal static class ValidationStateExtensions
{
    extension(ValidationState state)
    {
        public ValidationProblemDetails ToValidationProblemDetails()
        {
            ModelStateDictionary modelState = new();

            foreach ((string key, List<string> value) in state.Errors)
            {
                foreach (string error in value)
                {
                    modelState.AddModelError(key, error);
                }
            }

            return new ValidationProblemDetails(modelState);
        }

        public IActionResult ToHttpResponse()
            => new BadRequestObjectResult(state.ToValidationProblemDetails());

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
