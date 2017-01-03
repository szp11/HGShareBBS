using System;
using System.Linq;
using EsData.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nest;

namespace EsData.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            IElasticClient clent = EsHelper.CreateElasticClient();
            var result = clent.GetAlias(a => a.Name("vendorpromotionnews"));
            var indices = result.Indices.Select(index => index.Key).Select(dummy => (IndexName)dummy).ToArray();
            var s = result;
        }
    }
}
