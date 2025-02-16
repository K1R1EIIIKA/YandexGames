using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using _Scripts.Data;
using _Scripts.Data.Cards;
using UnityEngine;

namespace _Scripts.Tools
{
    public static class CardCSVHandler
    {
        private static string GetFilePath(string fileName) =>
            Path.Combine(Application.persistentDataPath, fileName);

        public static List<CardData> ReadCSV(string fileName)
        {
            string filePath = GetFilePath(fileName);

            if (!File.Exists(filePath))
            {
                Debug.Log($"File '{filePath}' not found, trying to load from Resources...");
                TextAsset textAsset = Resources.Load<TextAsset>("Config/CardsData/" + fileName);

                if (textAsset == null)
                {
                    Debug.LogError($"CSV file '{fileName}' not found in Resources/Config/CardsData/");
                    return new List<CardData>();
                }

                File.WriteAllText(filePath, textAsset.text);
                Debug.Log($"Created file '{filePath}' from Resources.");
            }

            return ReadCSVFromFile(filePath);
        }

        private static List<CardData> ReadCSVFromFile(string filePath)
        {
            List<CardData> cardDataList = new List<CardData>();

            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                bool isFirstLine = true;

                while ((line = reader.ReadLine()) != null)
                {
                    line = line.Trim();

                    if (isFirstLine)
                    {
                        isFirstLine = false;
                        continue;
                    }

                    string[] values = line.Split(',');

                    if (TryParseCsvLine(values, out CardData cardData))
                    {
                        cardDataList.Add(cardData);
                    }
                    else
                    {
                        Debug.LogError("Failed to parse CSV line: " + line);
                    }
                }
            }

            return cardDataList;
        }

        public static bool TryParseCsvLine(string[] values, out CardData result)
        {
            result = new CardData();

            if (values.Length != 6)
            {
                Debug.LogError("CSV line does not have the correct number of values.");
                return false;
            }

            try
            {
                result.Id = values[0];
                result.Name = values[1];
                result.ImageName = values[2];
                result.Cost = int.Parse(values[3]);
                result.Rare = Enum.TryParse(values[4], out Rare rare) ? rare : Rare.Common; // По умолчанию Rare.Common
                result.IsOpen = bool.Parse(values[5]);

                return true;
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error parsing CSV line: {ex.Message}");
                return false;
            }
        }

        public static void SaveCSV(string fileName, List<CardData> data)
        {
            string filePath = GetFilePath(fileName);

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine("Id,Name,ImageName,Cost,Rare,IsOpen");

                foreach (CardData card in data)
                {
                    string line = string.Join(",", new string[]
                    {
                        card.Id,
                        card.Name,
                        card.ImageName,
                        card.Cost.ToString(),
                        card.Rare.ToString(),
                        card.IsOpen.ToString()
                    });
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
