using System.Text.Json;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace RentACar.Helpers
{
    public static class TempDataExtensions
    {
        public static void Put<T>(this ITempDataDictionary tempData, string key, T value)
        {
            tempData[key] = JsonSerializer.Serialize(value);
        }

        public static T? Get<T>(this ITempDataDictionary tempData, string key)
        {
            tempData.TryGetValue(key, out object? o);

            if (o == null)
                return default;

            var result = JsonSerializer.Deserialize<T>((string)o);

            if (result is OperationResult operation && operation.Data is JsonElement element && operation.DataType != null)
            {
                var type = Type.GetType(operation.DataType);

                if (type != null)
                {
                    operation.Data = element.Deserialize(type);
                }
            }

            return result;
        }
    }
}
