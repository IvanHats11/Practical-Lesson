using System.Globalization;

namespace Practical_Lesson
{
    public class Citrus : Fruit
    {
        private double vitaminC;

        public Citrus() : base()
        {
            vitaminC = 0;
        }

        public Citrus(string name, string color, double vitaminC) : base(name, color)
        {
            VitaminC = vitaminC;
        }

        public double VitaminC
        {
            get { return vitaminC; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(VitaminC), "Vitamin C can not be negative.");
                }
                vitaminC = value;
            }
        }

        public override void Input()
        {
            base.Input();

            while (true)
            {
                Console.Write("Enter vitamin C (grams, e.g. 0.05): ");
                try
                {
                    VitaminC = ParseVitaminC(ReadConsoleLine());
                    break; 
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"Error: {ex.Message} Try again.");
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine($"Error: {ex.Message} Try again.");
                }
            }
        }

        public override void Input(TextReader reader)
        {
            base.Input(reader);
            VitaminC = ParseVitaminC(ReadLineOrThrow(reader));
        }

        public override void Print()
        {
            Console.WriteLine(ToString());
        }

        public override void Print(TextWriter writer)
        {
            writer.WriteLine(ToString());
        }

        public override string ToString()
        {
            string vitamin = VitaminC.ToString(CultureInfo.InvariantCulture);
            return $"Citrus: {Name}, color: {Color}, vitamin C: {vitamin} g";
        }

        private static double ParseVitaminC(string text)
        {
            if (!double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out double result))
            {
                throw new FormatException($"'{text}' is not a valid number for vitamin C.");
            }
            return result;
        }
    }
}
