using Practical_Lesson;

namespace Fruits.Tests
{
    [TestClass]
    public class FruitTests
    {
        [TestMethod]
        public void Constructor_SetsNameAndColor()
        {
            Fruit fruit = new Fruit("Apple", "Red");

            Assert.AreEqual("Apple", fruit.Name);
            Assert.AreEqual("Red", fruit.Color);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_EmptyName_ThrowsArgumentException()
        {
            Fruit fruit = new Fruit("", "Red");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Color_SetEmpty_ThrowsArgumentException()
        {
            Fruit fruit = new Fruit("Apple", "Red");
            fruit.Color = "   ";
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Name_SetNumber_ThrowsArgumentException()
        {
            Fruit fruit = new Fruit("123", "Red");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Name_SetLettersWithDigits_ThrowsArgumentException()
        {
            Fruit fruit = new Fruit("Apple1", "Red");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Color_SetNumber_ThrowsArgumentException()
        {
            Fruit fruit = new Fruit("Apple", "42");
        }

        [TestMethod]
        public void Name_TwoWordsAndHyphen_AreAllowed()
        {
            Fruit fruit = new Fruit("Blood orange", "Yellow-green");

            Assert.AreEqual("Blood orange", fruit.Name);
            Assert.AreEqual("Yellow-green", fruit.Color);
        }

        [TestMethod]
        public void Input_FromConsole_WrongValue_AsksAgain()
        {
            Fruit fruit = new Fruit();
            StringWriter output = new StringWriter();
            TextReader oldIn = Console.In;
            TextWriter oldOut = Console.Out;
            try
            {
                Console.SetIn(new StringReader("123\nBanana\n777\nYellow\n"));
                Console.SetOut(output);
                fruit.Input();
            }
            finally
            {
                Console.SetIn(oldIn);
                Console.SetOut(oldOut);
            }

            Assert.AreEqual("Banana", fruit.Name);
            Assert.AreEqual("Yellow", fruit.Color);
            StringAssert.Contains(output.ToString(), "Try again");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void Input_FromConsole_InputEnded_ThrowsInvalidDataException()
        {
            Fruit fruit = new Fruit();
            TextReader oldIn = Console.In;
            TextWriter oldOut = Console.Out;
            try
            {
                Console.SetIn(new StringReader("123\n")); 
                Console.SetOut(new StringWriter());
                fruit.Input();
            }
            finally
            {
                Console.SetIn(oldIn);
                Console.SetOut(oldOut);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Input_FromReader_NumberAsName_ThrowsArgumentException()
        {
            Fruit fruit = new Fruit();
            fruit.Input(new StringReader("123\nRed\n"));
        }

        [TestMethod]
        public void ToString_ReturnsCorrectText()
        {
            Fruit fruit = new Fruit("Apple", "Red");

            Assert.AreEqual("Fruit: Apple, color: Red", fruit.ToString());
        }

        [TestMethod]
        public void Input_FromConsole_ReadsNameAndColor()
        {
            Fruit fruit = new Fruit();
            TextReader oldIn = Console.In;
            TextWriter oldOut = Console.Out;
            try
            {
                Console.SetIn(new StringReader("Banana\nYellow\n"));
                Console.SetOut(new StringWriter()); 
                fruit.Input();
            }
            finally
            {
                Console.SetIn(oldIn);
                Console.SetOut(oldOut);
            }

            Assert.AreEqual("Banana", fruit.Name);
            Assert.AreEqual("Yellow", fruit.Color);
        }

        [TestMethod]
        public void Input_FromReader_ReadsNameAndColor()
        {
            Fruit fruit = new Fruit();
            StringReader reader = new StringReader("Pear\nGreen\n");

            fruit.Input(reader);

            Assert.AreEqual("Pear", fruit.Name);
            Assert.AreEqual("Green", fruit.Color);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void Input_FromReader_NotEnoughLines_ThrowsInvalidDataException()
        {
            Fruit fruit = new Fruit();
            fruit.Input(new StringReader("OnlyName\n"));
        }

        [TestMethod]
        public void Print_ToConsole_WritesToString()
        {
            Fruit fruit = new Fruit("Apple", "Red");
            StringWriter output = new StringWriter();
            TextWriter oldOut = Console.Out;
            try
            {
                Console.SetOut(output);
                fruit.Print();
            }
            finally
            {
                Console.SetOut(oldOut);
            }

            Assert.AreEqual("Fruit: Apple, color: Red", output.ToString().Trim());
        }

        [TestMethod]
        public void Print_ToWriter_WritesToString()
        {
            Fruit fruit = new Fruit("Apple", "Red");
            StringWriter writer = new StringWriter();

            fruit.Print(writer);

            Assert.AreEqual("Fruit: Apple, color: Red", writer.ToString().Trim());
        }
    }
}
