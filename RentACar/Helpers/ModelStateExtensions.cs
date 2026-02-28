using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace RentACar.Helpers
{
    public static class ModelStateExtensions
    {
        public static List<string> GetModelStateErrorList(this ModelStateDictionary modelState)
        {
            return modelState
                .Where(ms => ms.Value.Errors.Count > 0)
                .SelectMany(ms => ms.Value.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
        }
    }
}
