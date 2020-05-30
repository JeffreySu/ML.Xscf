using Senparc.CO2NET.HttpUtility;
using Senparc.Scf.XscfBase;
using Senparc.Scf.XscfBase.Functions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;

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
            /* 这里是处理文字选项（单选）的一个示例 */
            return FunctionHelper.RunFunction<UpdateDocs_Parameters>(param, async (typeParam, sb, result) =>
            {
                var url = "https://gitee.com/SenparcCoreFramework/ScfDocs/repository/archive/master.zip";
                Dictionary<string, string> headerAddition = new Dictionary<string, string>();
                headerAddition["User-Agent"] = "wget";
                var httpResponse = await RequestUtility.HttpResponseGetAsync(base.ServiceProvider, url, headerAddition: headerAddition).ConfigureAwait(false);
                using (var stream = await httpResponse.Content.ReadAsStreamAsync().ConfigureAwait(false))
                {
                    var len = stream.Length;


                }

                //if (Enum.TryParse<DownloadSourceCode_Parameters.Parameters_Site>(typeParam.Site.SelectedValues.FirstOrDefault()/*单选可以这样做，如果是多选需要遍历*/, out var siteType))
                //{
                //    switch (siteType)
                //    {
                //        case DownloadSourceCode_Parameters.Parameters_Site.GitHub:
                //            result.Message = "https://github.com/SenparcCoreFramework/ScfDocs/archive/master.zip";
                //            break;
                //        case DownloadSourceCode_Parameters.Parameters_Site.Gitee:
                //            result.Message = "https://gitee.com/SenparcCoreFramework/ScfDocs/archive/master.zip";
                //            break;
                //        default:
                //            result.Message = "未知的下载地址";
                //            result.Success = false;
                //            break;
                //    }
                //}
                //else
                //{
                //    result.Message = "未知的下载参数";
                //    result.Success = false;
                //}
            });
        }
    }
}
