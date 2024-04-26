using UnityEngine;

namespace Runtime.Extensions
{
    public static class Extension
    {
        public static string ToJson<T>(this T source) => JsonUtility.ToJson(source);
        public static T FromJson<T>(this string jsonStr) => JsonUtility.FromJson<T>(jsonStr);
    }
}
