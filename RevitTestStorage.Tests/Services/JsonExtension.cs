using Newtonsoft.Json;

namespace RevitTestStorage.Tests.Services
{
    public static class JsonExtension
    {
        public static string ToJson(this object obj)
        {
            if (obj is null) return null;
            return JsonConvert.SerializeObject(obj);
        }
        public static T FromJson<T>(this string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
        public static T FromJson<T>(this string json, JsonSerializerSettings settings)
        {
            return JsonConvert.DeserializeObject<T>(json, settings);
        }
    }
}