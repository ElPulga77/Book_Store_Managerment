using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;


namespace Book_Store_Memoir.Models
{
    public static class MySessions
    {
        public static T Get<T>(ISession session, string key)
        {
            if (string.IsNullOrEmpty(session.GetString(key)))
            {
                session.SetString(key, JsonConvert.SerializeObject(null));
            }
            return JsonConvert.DeserializeObject<T>(session.GetString(key));
        }
        public static void Set<T>(ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
        public static void SetObject(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObject<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }

    }
}
