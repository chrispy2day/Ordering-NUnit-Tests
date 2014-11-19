using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;


// Note: This excellent code was taken from:
// http://stackoverflow.com/questions/1078658/nunit-test-run-order
// Only the namespace was changed to work with the other modifications shown here.
namespace OrderingNUnitTests.Example1
{
    public class TestStructure
    {
        public Action Test;
    }

    class Int
    {
        public int I;
    }

    [TestFixture]
    public class ControllingTestOrder
    {
        private static readonly Int MyInt = new Int();

        [TestFixtureSetUp]
        public void SetUp()
        {
            MyInt.I = 0;
        }

        [TestCaseSource(sourceName: "TestSource")]
        public void MyTest(TestStructure test)
        {
            test.Test();
        }

        public IEnumerable<TestCaseData> TestSource
        {
            get
            {
                yield return new TestCaseData(
                    new TestStructure
                    {
                        Test = () =>
                        {
                            Console.WriteLine("This is test one");
                            MyInt.I++; Assert.That(MyInt.I, Is.EqualTo(1));
                        }
                    }).SetName(@"Test One");
                yield return new TestCaseData(
                    new TestStructure
                    {
                        Test = () =>
                        {
                            Console.WriteLine("This is test two");
                            MyInt.I++; Assert.That(MyInt.I, Is.EqualTo(2));
                        }
                    }).SetName(@"Test Two");
                yield return new TestCaseData(
                    new TestStructure
                    {
                        Test = () =>
                        {
                            Console.WriteLine("This is test three");
                            MyInt.I++; Assert.That(MyInt.I, Is.EqualTo(3));
                        }
                    }).SetName(@"Test Three");
            }
        }
    }
}
