using System.Collections.Generic;
using UnityEngine;

namespace YukkuriC.AlienGuns
{
    public static class ResourceGrabber
    {
        static Dictionary<System.Type, Dictionary<string, Object>> CACHED = new Dictionary<System.Type, Dictionary<string, Object>>();

        public static void ClearCache() => CACHED.Clear();
        public static Dictionary<string, Object> GetTypeMap<T>()
        {
            var key = typeof(T);
            if (!CACHED.TryGetValue(key, out var map))
            {
                CACHED[key] = map = new Dictionary<string, Object>();
                foreach (var obj in Resources.FindObjectsOfTypeAll(key))
                {
                    map[obj.name] = obj;
                }
            }
            return map;
        }
        public static T Get<T>(string key) where T : Object
        {
            var map = GetTypeMap<T>();
            if (!map.TryGetValue(key, out var obj)) return default(T);
            return (T)obj;
        }
    }
}
