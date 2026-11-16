using Practical_Lesson;

namespace Fruits.Tests
{
    [TestClass]
    public class CitrusTests
    {
        [TestMethod]
        public void Constructor_SetsAllProperties()
        {
            Citrus lemon = new Citrus("Lemon", "Yellow", 0.053);

            Assert.AreEqual("Lemon", lemon.Name);
            Assert.AreEqual("Yellow", lemon.Color);
            Assert.AreEqual(0.053, lemon.VitaminC, 0.0001);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_NegativeVitaminC_ThrowsException()
        {
            Citrus lemon = new Citrus("Lemon", "Yellow", -1);
        }

        [TestMethod]
        public void Citrus_IsAFruit()
        {
            Fruit fruit = new Citrus("Lime", "Green", 0.029);

            Assert.IsInstanceOfType(fruit, typeof(Fruit));
        }

        [TestMethod]
        public void ToString_ContainsVitaminC()
        {
            Citrus lemon = new Citrus("Lemon", "Yellow", 0.053);

            Assert.AreEqual("Citrus: Lemon, color: Yellow, vitamin C: 0.053 g", lemon.ToString());
        }

        [TestMethod]
        public void Input_FromConsole_ReadsAllValues()
        {
            Citrus orange = new Citrus();
            TextReader oldIn = Console.In;
            TextWriter oldOut = Console.Out;
            try
            {
                Console.SetIn(new StringReader("Orange\nOrange\n0.059\n"));
                Console.SetOut(new StringWriter());
                orange.Input();
            }
            finally
            {
                Console.SetIn(oldIn);
                Console.SetOut(oldOut);
            }

            Assert.AreEqual("Orange", orange.Name);
            Assert.AreEqual("Orange", orange.Color);
            Assert.AreEqual(0.059, orange.VitaminC, 0.0001);
        }

        [TestMethod]
        public void Input_FromConsole_WrongVitaminC_AsksAgain()
        {
            Citrus lemon = new Citrus();
            TextReader oldIn = Console.In;
            TextWriter oldOut = Console.Out;
            try
            {
                Console.SetIn(new StringReader("Lemon\nYellow\nabc\n-1\n0.053\n"));
                Console.SetOut(new StringWriter());
                lemon.Input();
            }
            finally
            {
                Console.SetIn(oldIn);
                Console.SetOut(oldOut);
            }

            Assert.AreEqual(0.053, lemon.VitaminC, 0.0001);
        }

        [TestMethod]
        public void Input_FromReader_ReadsAllValues()
        {
            Citrus lime = new Citrus();

            lime.Input(new StringReader("Lime\nGreen\n0.029\n"));

            Assert.AreEqual("Lime", lime.Name);
            Assert.AreEqual("Green", lime.Color);
            Assert.AreEqual(0.029, lime.VitaminC, 0.0001);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void Input_FromReader_WrongNumber_ThrowsFormatException()
        {
            Citrus lime = new Citrus();
            lime.Input(new StringReader("Lime\nGreen\nabc\n"));
        }

        [TestMethod]
        public void Print_ThroughFruitVariable_CallsCitrusVersion()
        {
            Fruit fruit = new Citrus("Lemon", "Yellow", 0.053);
            StringWriter writer = new StringWriter();

            fruit.Print(writer);

            Assert.AreEqual("Citrus: Lemon, color: Yellow, vitamin C: 0.053 g", writer.ToString().Trim());
        }
    }
}
