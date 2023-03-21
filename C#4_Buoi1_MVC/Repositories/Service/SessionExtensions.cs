using System.Text.Json;

namespace C_4_Buoi1_MVC.Repositories.Service
{
    public static class SessionExtensions
    {
        //Phương thức mở rộng để có thể lưu 1 object vào session
        public static void SetObject(this ISession session, string key, object value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T GetObject<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonSerializer.Deserialize<T>(value);
        }
    }
}
