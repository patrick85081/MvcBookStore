using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MvcBookStore.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SortOrder
    {
        ASC,
        DESC
    }
}