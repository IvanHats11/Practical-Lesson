using System.Text.Json;

namespace Practical_Lesson
{
    public class Program
    {
        public static string AskChoice()
        {
            while (true)
            {
                Console.Write("Do you want to add a fruit from the console? (fruit / citrus / no): ");
                string line = Console.ReadLine();
                if (line == null)
                {
                    throw new InvalidDataException("Console input has ended.");
                }

                try
                {
                    return ParseChoice(line);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Warning: {ex.Message} Try again.");
                }
            }
        }

        public static string ParseChoice(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentException("Answer can not be empty.");
            }

            string answer = text.Trim().ToLower();
            if (answer.Any(char.IsDigit))
            {
                throw new ArgumentException($"Numbers are not allowed here: '{text.Trim()}'. Type a word.");
            }
            if (answer != "fruit" && answer != "citrus" && answer != "no")
            {
                throw new ArgumentException($"Unknown answer: '{text.Trim()}'. Type fruit, citrus or no.");
            }
            return answer;
        }

        public static void Main(string[] args)
        {
            try
            {
                List<Fruit> fruits = new List<Fruit>
                {
                    new Fruit("Banana", "Yellow"),
                    new Fruit("Apple", "Red"),
                    new Citrus("Lemon", "Yellow", 0.053),
                    new Citrus("Orange", "Orange", 0.059),
                    new Fruit("Pear", "Yellow"),
                    new Citrus("Lime", "Green", 0.029),
                    new Fruit("Kiwi", "Green")
                };

                string answer = AskChoice();
                if (answer == "fruit" || answer == "citrus")
                {
                    Fruit newFruit = answer == "fruit" ? new Fruit() : new Citrus();
                    newFruit.Input();
                    fruits.Add(newFruit);
                }

                Console.WriteLine();
                Console.WriteLine("All fruits:");
                foreach (Fruit fruit in fruits)
                {
                    fruit.Print();
                }

                Console.WriteLine();
                Console.WriteLine("Yellow fruits:");
                foreach (Fruit fruit in FruitStorage.GetYellowFruits(fruits))
                {
                    fruit.Print();
                }

                List<Fruit> sorted = FruitStorage.SortByName(fruits);
                FruitStorage.PrintToFile(sorted, "sorted_fruits.txt");
                Console.WriteLine();
                Console.WriteLine("Sorted list was written to sorted_fruits.txt");

                FruitStorage.SerializeToXml(fruits, "fruits.xml");
                List<Fruit> fromXml = FruitStorage.DeserializeFromXml("fruits.xml");
                Console.WriteLine();
                Console.WriteLine("Fruits loaded from fruits.xml:");
                foreach (Fruit fruit in fromXml)
                {
                    fruit.Print();
                }

                FruitStorage.SerializeToJson(fruits, "fruits.json");
                List<Fruit> fromJson = FruitStorage.DeserializeFromJson("fruits.json");
                Console.WriteLine();
                Console.WriteLine("Fruits loaded from fruits.json:");
                foreach (Fruit fruit in fromJson)
                {
                    fruit.Print();
                }


                Console.WriteLine();
                if (File.Exists("input.txt"))
                {
                    Console.WriteLine("Fruits loaded from input.txt:");
                    foreach (Fruit fruit in FruitStorage.ReadFromFile("input.txt"))
                    {
                        fruit.Print();
                    }
                }
                else
                {
                    Console.WriteLine("File input.txt was not found, skipping reading from file.");
                }
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Wrong number format: {ex.Message}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Wrong data: {ex.Message}");
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"File not found: {ex.Message}");
            }
            catch (InvalidDataException ex)
            {
                Console.WriteLine($"Wrong file content: {ex.Message}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"File error: {ex.Message}");
            }
            catch (InvalidOperationException ex)
            { 
                Console.WriteLine($"XML error: {ex.Message}");
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"JSON error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
            finally
            {
                Console.WriteLine();
                Console.WriteLine("Program finished.");
            }
        }
    }
}
