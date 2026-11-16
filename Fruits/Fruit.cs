using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Practical_Lesson
{
    [XmlInclude(typeof(Citrus))]
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
    [JsonDerivedType(typeof(Fruit), "fruit")]
    [JsonDerivedType(typeof(Citrus), "citrus")]
    public class Fruit
    {
        private string name;
        private string color;

        public Fruit()
        {
            name = "Unknown";
            color = "Unknown";
        }

        public Fruit(string name, string color)
        {
            Name = name;
            Color = color;
        }

        public string Name
        {
            get { return name; }
            set { name = CheckWord(value, "Name"); }
        }

        public string Color
        {
            get { return color; }
            set { color = CheckWord(value, "Color"); }
        }

        public virtual void Input()
        {
            while (true)
            {
                Console.Write("Enter name: ");
                try
                {
                    Name = ReadConsoleLine();
                    break; 
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Error: {ex.Message} Try again.");
                }
            }

            while (true)
            {
                Console.Write("Enter color: ");
                try
                {
                    Color = ReadConsoleLine();
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Error: {ex.Message} Try again.");
                }
            }
        }


        public virtual void Input(TextReader reader)
        {
            Name = ReadLineOrThrow(reader);
            Color = ReadLineOrThrow(reader);
        }

        public virtual void Print()
        {
            Console.WriteLine(ToString());
        }

        public virtual void Print(TextWriter writer)
        {
            writer.WriteLine(ToString());
        }

        public override string ToString()
        {
            return $"Fruit: {Name}, color: {Color}";
        }

        private static readonly Regex WordPattern = new Regex(@"^\p{L}+([ -]\p{L}+)*$");

        private static string CheckWord(string value, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException($"{fieldName} can not be empty.");
            }

            string text = value.Trim();
            if (!WordPattern.IsMatch(text))
            {
                throw new ArgumentException($"{fieldName} must contain only letters (no digits or symbols): '{text}'.");
            }
            return text;
        }

        protected static string ReadConsoleLine()
        {
            string line = Console.ReadLine();
            if (line == null)
            {
                throw new InvalidDataException("Console input has ended.");
            }
            return line;
        }

        protected static string ReadLineOrThrow(TextReader reader)
        {
            string line = reader.ReadLine();
            if (line == null)
            {
                throw new InvalidDataException("Unexpected end of file.");
            }
            return line;
        }
    }
}
