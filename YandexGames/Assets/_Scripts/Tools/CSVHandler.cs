using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace _Scripts.Tools
{
    public static class CSVHandler
    {
        private static string GetFilePath(string fileName) =>
            Path.Combine(Application.persistentDataPath, fileName);

        public static List<T> ReadCSV<T>(string fileName) where T : new()
        {
            string filePath = GetFilePath(fileName);

            if (!File.Exists(filePath))
            {
                Debug.Log($"File '{filePath}' not found, trying to load from Resources...");
                TextAsset textAsset = Resources.Load<TextAsset>("Config/CardsData/" + fileName);

                if (textAsset == null)
                {
                    Debug.LogError($"CSV file '{fileName}' not found in Resources/Config/CardsData/");
                    return new List<T>();
                }

                File.WriteAllText(filePath, textAsset.text);
                Debug.Log($"Created file '{filePath}' from Resources.");
            }

            return ReadCSVFromFile<T>(filePath);
        }

        private static List<T> ReadCSVFromFile<T>(string filePath) where T : new()
        {
            List<T> objects = new List<T>();

            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                bool isFirstLine = true;

                while ((line = reader.ReadLine()) != null)
                {
                    Debug.Log(line);
                    line = line.Trim();

                    if (isFirstLine)
                    {
                        isFirstLine = false;
                        continue;
                    }

                    string[] values = line.Split(',');

                    if (TryParseCsvLine(values, out T obj))
                    {
                        objects.Add(obj);
                    }
                    else
                    {
                        Debug.LogError("Failed to parse CSV line: " + line);
                    }
                }
            }

            return objects;
        }

        public static bool TryParseCsvLine<T>(string[] values, out T result) where T : new()
        {
            result = new T();
            Type type = typeof(T);

            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            if (values.Length < properties.Length)
            {
                Debug.LogError("CSV line does not have enough values.");
                return false;
            }
            else if (values.Length > properties.Length)
            {
                Debug.LogError("Object does not have enough fields.");
                return false;
            }

            try
            {
                for (int i = 0; i < properties.Length; i++)
                {
                    PropertyInfo property = properties[i];

                    if (property.PropertyType == typeof(int))
                        property.SetValue(result, int.Parse(values[i]));
                    else if (property.PropertyType == typeof(bool))
                        property.SetValue(result, bool.Parse(values[i]));
                    else if (property.PropertyType.IsEnum)
                    {
                        if (Enum.TryParse(property.PropertyType, values[i], out object enumValue))
                            property.SetValue(result, enumValue);
                        else
                        {
                            Debug.LogError($"Invalid enum value '{values[i]}' for {property.Name}");
                            return false;
                        }
                    }
                    else
                    {
                        Debug.Log(values[i]);
                        property.SetValue(result, values[i]);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error parsing CSV line: {ex.Message}");
                return false;
            }
        }

        public static void SaveCSV<T>(string fileName, List<T> data)
        {
            string filePath = GetFilePath(fileName);

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                PropertyInfo[] properties = typeof(T).GetProperties();

                // Записываем заголовки
                writer.WriteLine(string.Join(",", properties.Select(p => p.Name)));

                // Записываем данные
                foreach (T item in data)
                {
                    string line = string.Join(",", properties.Select(p => p.GetValue(item)?.ToString() ?? ""));
                    writer.WriteLine(line);
                }
            }

            Debug.Log($"CSV file '{filePath}' saved.");
        }

        public static void ResetProgress(string fileName)
        {
            string filePath = GetFilePath(fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                Debug.Log($"Deleted '{filePath}', progress reset.");
            }
        }
    }
}