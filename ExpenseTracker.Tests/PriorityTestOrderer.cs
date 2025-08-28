using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace ExpenseTracker.Tests
{
    public class PriorityTestOrderer : ITestCaseOrderer
    {
        public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases)
        where TTestCase : ITestCase
        {
            return testCases.OrderBy(testCase =>
                testCase.TestMethod.Method.Name switch
                {
                    "GetCategoryList_ReturnsAllCategories" => 0,
                    "DeleteCategory_RemovesCategory_WhenCategoryExists" => 2,                 
                    _ => 1                                      
                });
        }
    }
}
