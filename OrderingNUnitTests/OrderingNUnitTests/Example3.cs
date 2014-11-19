using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OrderingNUnitTests.Example3
{
    [TestFixture]
    public class Example3 : OrderedTestFixture
    {
        //private static readonly Int MyInt = new Int();
        private static int MyInt;

        [TestFixtureSetUp]
        public void SetUp()
        {
            //MyInt.I = 0;
            MyInt = 0;
        }

        [OrderedTest(0)]
        public void Test0()
        {
            Console.WriteLine("This is my test zero");
            //Assert.That(MyInt.I, Is.EqualTo(0));
            Assert.That(MyInt, Is.EqualTo(0));
        }

        [OrderedTest(2)]
        public void ATest0()
        {
            Console.WriteLine("This is my test two");
            //MyInt.I++; Assert.That(MyInt.I, Is.EqualTo(2));
            MyInt++;
            Assert.That(MyInt, Is.EqualTo(2));
        }


        [OrderedTest(1)]
        public void BTest0()
        {
            Console.WriteLine("This is my test one");
            //MyInt.I++; Assert.That(MyInt.I, Is.EqualTo(1));
            MyInt++;
            Assert.That(MyInt, Is.EqualTo(1));
        }

        [OrderedTest(3)]
        public void AAA()
        {
            Console.WriteLine("This is my test three");
            //MyInt.I++; Assert.That(MyInt.I, Is.EqualTo(3));
            MyInt++;
            Assert.That(MyInt, Is.EqualTo(3));
        }


        [TestCaseSource(sourceName: "TestSource")]
        public void MyTest(TestStructure test)
        {
            test.Test();
        }
    }
}