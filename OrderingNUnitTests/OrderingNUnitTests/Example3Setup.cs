using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OrderingNUnitTests.Example3
{
    // This structure was derived from the answers at http://stackoverflow.com/questions/1078658/nunit-test-run-order


    public class OrderedTestAttribute : Attribute
    {
        public int Order { get; set; }

        public OrderedTestAttribute(int order)
        {
            Order = order;
        }
    }

    public class TestStructure
    {
        public Action Test;
    }

    public class OrderedTestFixture
    {
        public IEnumerable<TestCaseData> TestSource
        {
            get
            {
                var assembly = Assembly.GetExecutingAssembly();
                Dictionary<int, List<MethodInfo>> methods = assembly
                    .GetType(this.GetType().FullName)
                    .GetMethods()
                    //.GetTypes()
                    //.SelectMany(x => x.GetMethods())
                    .Where(y => y.GetCustomAttributes().OfType<OrderedTestAttribute>().Any())
                    .GroupBy(z => z.GetCustomAttribute<OrderedTestAttribute>().Order)
                    .ToDictionary(gdc => gdc.Key, gdc => gdc.ToList());

                foreach (var order in methods.Keys.OrderBy(x => x))
                {
                    foreach (var methodInfo in methods[order])
                    {
                        MethodInfo info = methodInfo;
                        yield return new TestCaseData(
                            new TestStructure
                            {
                                Test = () =>
                                {
                                    object classInstance = Activator.CreateInstance(info.DeclaringType, null);
                                    info.Invoke(classInstance, null);
                                }
                            }).SetName(methodInfo.Name);
                    }
                }
            }
        }
    }
}
