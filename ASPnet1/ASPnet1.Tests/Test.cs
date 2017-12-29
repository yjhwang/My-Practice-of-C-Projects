using NUnit.Framework;
using System;

namespace ASPnet1.Tests
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void TestCase()
        {
            Random r = new Random();
            int a = r.Next(0, 100);

        }
    }
}
