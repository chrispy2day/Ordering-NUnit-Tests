using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OrderingNUnitTests.Example2
{
    [TestFixture]
    public class Example2b
    {
        [OrderedTest(0)]
        public void Test0()
        {
            Console.WriteLine("This is another test 0.");
            Assert.That(0, Is.EqualTo(0));
        }
    }
}
