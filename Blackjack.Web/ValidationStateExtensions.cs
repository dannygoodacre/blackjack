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
    }
}
