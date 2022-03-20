using Accounting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Accounting.Helpers
{
    public static class DataHelper
    {
        private readonly static string fileName = "Data.json";
        public static bool WriteFile(object data)
        {
            string jsonString = JsonSerializer.Serialize(data);
            System.IO.File.WriteAllText(fileName, jsonString);
            return true;
        }
        public static string ReadFile()
        {
            string jsonString = System.IO.File.ReadAllText(fileName);
            return jsonString;
        }
    }
}
