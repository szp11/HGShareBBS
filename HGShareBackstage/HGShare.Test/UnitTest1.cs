using System;
using HGShare.Site.Config;
using HGShare.Site.Token;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HGShare.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Token tk=new PwdToken(TokenConfig.PwdTokenKey,"000000");
            string pwd=tk.GetToken();
        }
    }
}
