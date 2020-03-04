using Microsoft.VisualStudio.TestTools.UnitTesting;
using FaustVX.Temp;
using FaustVX.Process;

namespace FaustVX.Process.Test
{
    [TestClass]
    public class UnitTest1
    {
        private static void Prepare(System.Action action)
        {
            using (TemporaryDirectory.CreateTemporaryDirectory(setCurrentDirectory: true))
                action();
        }

        [TestMethod]
        public void Join()
        {
            var actual1 = new[] { "clone", "-n", "-b test", "http://github.com" }.Join();
            System.Console.WriteLine(actual1);
            Assert.AreEqual("clone -n -b test http://github.com", actual1);
            var actual2 = new[] { null, "-b test" }.Join();
            System.Console.WriteLine(actual2);
            Assert.AreEqual("-b test", actual2);
            var actual3 = new string?[] { null, null }.Join();
            System.Console.WriteLine(actual3);
            Assert.AreEqual("", actual3);
            
            var git = Process.CreateProcess("git");
            var clone = git("clone", "plop", "dfgh").StartAndWaitForExit();
            Assert.IsFalse(clone);
        }
    }
}
