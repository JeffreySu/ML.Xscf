using LibGit2Sharp;
using Senparc.CO2NET.Helpers;
using Senparc.CO2NET.HttpUtility;
using Senparc.Scf.XscfBase;
using Senparc.Scf.XscfBase.Functions;
using System;
using System.Collections.Generic;
using System.IO;

namespace ML.Xscf.Docs.Functions
{

    public class UpdateDocs : FunctionBase
    {
        public class UpdateDocs_Parameters : IFunctionParameter
        {

        }

        //注意：Name 必须在单个 Xscf 模块中唯一！
        public override string Name => "更新文档";

        public override string Description => "更新最新的官方文档";
        public override Type FunctionParameterType => typeof(UpdateDocs_Parameters);

        public UpdateDocs(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        /// <summary>
        /// 运行
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public override FunctionResult Run(IFunctionParameter param)
        {
            Console.WriteLine("start");
            /* 这里是处理文字选项（单选）的一个示例 */
            return FunctionHelper.RunFunction<UpdateDocs_Parameters>(param, (typeParam, sb, result) =>
               {
                   var wwwrootDir = Path.Combine(Senparc.CO2NET.Config.RootDictionaryPath, "wwwroot");
                   FileHelper.TryCreateDirectory(wwwrootDir);
                   var copyDir = Path.Combine(wwwrootDir, "scf_docs");
                   FileHelper.TryCreateDirectory(copyDir);

                   var gitUrl = "https://gitee.com/SenparcCoreFramework/ScfDocs.git";
                   Repository.Clone(gitUrl, copyDir);
                   sb.AppendLine($"仓库创建于 {copyDir}");
               });
        }
    }
}
