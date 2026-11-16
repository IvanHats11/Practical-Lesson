using Practical_Lesson;

namespace Fruits.Tests
{
    [TestClass]
    public class FruitStorageTests
    {
        private List<Fruit> fruits;
        private string tempFile;

        [TestInitialize]
        public void SetUp()
        {
            fruits = new List<Fruit>
            {
                new Fruit("Banana", "Yellow"),
                new Fruit("Apple", "Red"),
                new Citrus("Lemon", "Yellow", 0.053),
                new Citrus("Orange", "Orange", 0.059),
                new Fruit("Kiwi", "Green")
            };
            tempFile = Path.GetTempFileName();
        }

        [TestCleanup]
        public void CleanUp()
        {
            if (File.Exists(tempFile))
            {
                File.Delete(tempFile);
            }
        }

        private static List<string> ToStrings(List<Fruit> list)
        {
            return list.Select(f => f.ToString()).ToList();
        }

        [TestMethod]
        public void GetYellowFruits_ReturnsOnlyYellow()
        {
            List<Fruit> yellow = FruitStorage.GetYellowFruits(fruits);

            Assert.AreEqual(2, yellow.Count);
            Assert.AreEqual("Banana", yellow[0].Name);
            Assert.AreEqual("Lemon", yellow[1].Name);
        }

        [TestMethod]
        public void GetYellowFruits_IgnoresCase()
        {
            List<Fruit> list = new List<Fruit> { new Fruit("Pear", "YELLOW") };

            Assert.AreEqual(1, FruitStorage.GetYellowFruits(list).Count);
        }

        [TestMethod]
        public void SortByName_SortsAlphabetically()
        {
            List<Fruit> sorted = FruitStorage.SortByName(fruits);

            CollectionAssert.AreEqual(
                new[] { "Apple", "Banana", "Kiwi", "Lemon", "Orange" },
                sorted.Select(f => f.Name).ToArray());
        }

        [TestMethod]
        public void PrintToFile_WritesOneLinePerFruit()
        {
            FruitStorage.PrintToFile(fruits, tempFile);

            string[] lines = File.ReadAllLines(tempFile);
            Assert.AreEqual(5, lines.Length);
            Assert.AreEqual("Fruit: Banana, color: Yellow", lines[0]);
            Assert.AreEqual("Citrus: Lemon, color: Yellow, vitamin C: 0.053 g", lines[2]);
        }

        [TestMethod]
        public void ReadFromFile_CreatesFruitAndCitrus()
        {
            File.WriteAllText(tempFile, "Fruit\nMango\nYellow\nCitrus\nLime\nGreen\n0.029\n");

            List<Fruit> result = FruitStorage.ReadFromFile(tempFile);

            Assert.AreEqual(2, result.Count);
            Assert.IsNotInstanceOfType(result[0], typeof(Citrus));
            Assert.IsInstanceOfType(result[1], typeof(Citrus));
            Assert.AreEqual(0.029, ((Citrus)result[1]).VitaminC, 0.0001);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void ReadFromFile_UnknownType_ThrowsInvalidDataException()
        {
            File.WriteAllText(tempFile, "Vegetable\nCarrot\nOrange\n");
            FruitStorage.ReadFromFile(tempFile);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void ReadFromFile_MissingFile_ThrowsFileNotFoundException()
        {
            FruitStorage.ReadFromFile("this_file_does_not_exist.txt");
        }

        [TestMethod]
        public void Xml_SerializeAndDeserialize_ReturnsSameList()
        {
            FruitStorage.SerializeToXml(fruits, tempFile);
            List<Fruit> result = FruitStorage.DeserializeFromXml(tempFile);

            CollectionAssert.AreEqual(ToStrings(fruits), ToStrings(result));
            Assert.IsInstanceOfType(result[2], typeof(Citrus));
        }

        [TestMethod]
        public void Json_SerializeAndDeserialize_ReturnsSameList()
        {
            FruitStorage.SerializeToJson(fruits, tempFile);
            List<Fruit> result = FruitStorage.DeserializeFromJson(tempFile);

            CollectionAssert.AreEqual(ToStrings(fruits), ToStrings(result));
            Assert.IsInstanceOfType(result[2], typeof(Citrus));
        }
    }
}
