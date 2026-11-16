using Practical_Lesson;

namespace Fruits.Tests
{
    [TestClass]
    public class ProgramTests
    {
        [TestMethod]
        public void ParseChoice_CorrectAnswers_AreAccepted()
        {
            Assert.AreEqual("fruit", Program.ParseChoice("fruit"));
            Assert.AreEqual("citrus", Program.ParseChoice("  Citrus "));
            Assert.AreEqual("no", Program.ParseChoice("NO"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParseChoice_Number_ThrowsArgumentException()
        {
            Program.ParseChoice("123");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParseChoice_Empty_ThrowsArgumentException()
        {
            Program.ParseChoice("   ");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParseChoice_UnknownWord_ThrowsArgumentException()
        {
            Program.ParseChoice("banana");
        }

        [TestMethod]
        public void AskChoice_WrongAnswers_ShowsWarningAndAsksAgain()
        {
            StringWriter output = new StringWriter();
            TextReader oldIn = Console.In;
            TextWriter oldOut = Console.Out;
            string result;
            try
            {
                Console.SetIn(new StringReader("123\nbanana\n\ncitrus\n"));
                Console.SetOut(output);
                result = Program.AskChoice();
            }
            finally
            {
                Console.SetIn(oldIn);
                Console.SetOut(oldOut);
            }

            Assert.AreEqual("citrus", result);
            StringAssert.Contains(output.ToString(), "Numbers are not allowed");
            StringAssert.Contains(output.ToString(), "Unknown answer");
        }
    }
}
