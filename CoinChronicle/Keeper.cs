using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace CoinChronicle
{
    public static class Keeper
    {
        public static List<ChronicleEntry> Load(string path)
        {
            if (!File.Exists(path)) return new List<ChronicleEntry>();
            try
            {
                var json = File.ReadAllText(path);
                return JsonSerializer.Deserialize<List<ChronicleEntry>>(json)
                       ?? new List<ChronicleEntry>();
            }
            catch
            {
                // On error, return empty list (caller can decide how to report)
                return new List<ChronicleEntry>();
            }
        }

        public static void Save(string path, IEnumerable<ChronicleEntry> entries)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(entries, options);

            var temp = path + ".tmp";
            File.WriteAllText(temp, json);
            File.Copy(temp, path, overwrite: true);
            File.Delete(temp);
        }
    }
}