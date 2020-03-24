using Senparc.Scf.Core.Config;
using System;
using System.IO;

namespace ML.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            string RootDictionaryPath = Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\"/*项目根目录*/, "..\\..\\..\\_GitHub.Senparc\\SCF\\src\\Senparc.Web"/*找到 Web目录，以获取统一的数据库连接字符串配置*/);
            string SitePath = Path.GetFullPath(Path.Combine(SiteConfig.WebRootPath, "..", "ML.Xscf.DocsManage"));
            Console.WriteLine(RootDictionaryPath);
            Console.WriteLine(SitePath);
            Console.ReadLine();
        }
    }
}
