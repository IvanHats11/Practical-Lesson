using System.Text.Json;
using System.Xml.Serialization;

namespace Practical_Lesson
{

    public static class FruitStorage
    {
        public static List<Fruit> GetYellowFruits(List<Fruit> fruits)
        {
            return fruits
                .Where(f => f.Color.Equals("yellow", StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public static List<Fruit> SortByName(List<Fruit> fruits)
        {
            return fruits.OrderBy(f => f.Name).ToList();
        }

        public static void PrintToFile(List<Fruit> fruits, string path)
        {
            using (StreamWriter writer = new StreamWriter(path))
            {
                foreach (Fruit fruit in fruits)
                {
                    fruit.Print(writer);
                }
            }
        }

        public static List<Fruit> ReadFromFile(string path)
        {
            List<Fruit> fruits = new List<Fruit>();

            using (StreamReader reader = new StreamReader(path))
            {
                string type;
                while ((type = reader.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(type))
                    {
                        continue; 
                    }

                    Fruit fruit;
                    if (type.Trim() == "Fruit")
                    {
                        fruit = new Fruit();
                    }
                    else if (type.Trim() == "Citrus")
                    {
                        fruit = new Citrus();
                    }
                    else
                    {
                        throw new InvalidDataException($"Unknown fruit type: '{type}'.");
                    }

                    fruit.Input(reader); 
                    fruits.Add(fruit);
                }
            }

            return fruits;
        }

        public static void SerializeToXml(List<Fruit> fruits, string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Fruit>));
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                serializer.Serialize(stream, fruits);
            }
        }

        public static List<Fruit> DeserializeFromXml(string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Fruit>));
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                return (List<Fruit>)serializer.Deserialize(stream);
            }
        }

        public static void SerializeToJson(List<Fruit> fruits, string path)
        {
            JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(fruits, options);
            File.WriteAllText(path, json);
        }

        public static List<Fruit> DeserializeFromJson(string path)
        {
            string json = File.ReadAllText(path);
            List<Fruit> fruits = JsonSerializer.Deserialize<List<Fruit>>(json);
            if (fruits == null)
            {
                throw new InvalidDataException("JSON file does not contain a list of fruits.");
            }
            return fruits;
        }
    }
}
