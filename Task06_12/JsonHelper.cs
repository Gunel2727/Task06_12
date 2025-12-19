using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Task06_12
{
    public static class JsonHelper
    {
        public static void Write<T>(string path, List<T> data)
        {
            string json = JsonSerializer.Serialize(data, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(path, json);
        }
        public static List<T> Read<T>(string path)
        {
            if (!File.Exists(path))
                return new List<T>();
            string json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
        }
       
    }
}
