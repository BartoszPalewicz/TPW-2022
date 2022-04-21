using Microsoft.VisualStudio.TestTools.UnitTesting;
using TPW;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Assert.AreEqual(Class1.GetString(0), "Witaj Swiecie!");
            Assert.AreEqual(Class1.GetString(1), "Hello World!");
        }
    }
}