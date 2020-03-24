using ML.Xscf.Docs.Models.DatabaseModel.Dto;
using Senparc.Scf.Core.Enums;
using Senparc.Scf.Repository;
using Senparc.Scf.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Senparc.CO2NET.Trace;

namespace ML.Xscf.Docs.Services
{
    public class ArticleService : ServiceBase<Article>
    {
        public ArticleService(IRepositoryBase<Article> repo, IServiceProvider serviceProvider) : base(repo, serviceProvider)
        {
        }
    }
}
