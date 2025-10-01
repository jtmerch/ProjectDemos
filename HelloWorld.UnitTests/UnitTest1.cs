using HelloWorld.IntermediateTopics.ExtClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HelloWorld.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string registrationNumber = "123456";
            var car = new ConstructorInheritanceCar(registrationNumber);
        }
    }
}
