using Microsoft.VisualStudio.TestTools.UnitTesting;
using ML.Xscf.Docs.Functions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ML.Xscf.Tests.Functions
{
    [TestClass]
    public class UpdateDocsTests : BaseTest
    {
        public UpdateDocsTests()
        {
            //注册 SenparcHttpClient

        }

        [TestMethod]
        public void RunTest()
        {
            var function = new ML.Xscf.Docs.Functions.UpdateDocs(base.ServiceProvider);
            function.Run(new UpdateDocs.UpdateDocs_Parameters());

            var filePath = Path.Combine(Senparc.CO2NET.Config.RootDictionaryPath, "wwwroot", "ScfDocs", "README.md");
            //断言文件存在
            Assert.IsTrue(File.Exists(filePath));
        }
    }
}
