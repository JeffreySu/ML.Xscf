﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Senparc.CO2NET.Extensions;
using Senparc.CO2NET.Trace;
using ML.Xscf.Docs.Functions;
using ML.Xscf.Docs.Models;
using ML.Xscf.Docs.Models.DatabaseModel;
using ML.Xscf.Docs.Models.DatabaseModel.Dto;
using ML.Xscf.Docs.Services;
using Senparc.Scf.Core.Areas;
using Senparc.Scf.Core.Enums;
using Senparc.Scf.Core.Models;
using Senparc.Scf.XscfBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Senparc.Scf.Core.Config;

namespace ML.Xscf.Docs
{
    public class Register : XscfRegisterBase,
        IXscfRegister, //注册 XSCF 基础模块接口（必须）
        IAreaRegister, //注册 XSCF 页面接口（按需选用）
        IXscfDatabase,  //注册 XSCF 模块数据库（按需选用）
        IXscfRazorRuntimeCompilation  //需要使用 RazorRuntimeCompilation，在开发环境下实时更新 Razor Page
    {
        public Register()
        { }


        #region IXscfRegister 接口

        public override string Name => "ML.Xscf.Docs";
        public override string Uid => "519E8526-A738-465A-9DB8-2762E8441762";//必须确保全局唯一，生成后必须固定
        public override string Version => "0.0.1.17";//必须填写版本号

        public override string MenuName => "开发者文档";
        public override string Icon => "fa fa-dot-circle-o";//参考如：https://colorlib.com/polygon/gentelella/icons.html
        public override string Description => "这是一个开发者文档项目，用于阐述SCF的架构,便于开发者快速上手并掌握SCF的使用规范及开发方法";

        /// <summary>
        /// 注册当前模块需要支持的功能模块
        /// </summary>
        public override IList<Type> Functions => new Type[] {
            typeof(DownloadSourceCode)
        };


        /// <summary>
        /// 安装或更新过程需要执行的业务
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="installOrUpdate"></param>
        /// <returns></returns>
        public override async Task InstallOrUpdateAsync(IServiceProvider serviceProvider, InstallOrUpdate installOrUpdate)
        {
            //更新数据库
            await base.MigrateDatabaseAsync<MLEntities>(serviceProvider);

            switch (installOrUpdate)
            {
                case InstallOrUpdate.Install:
                    //新安装,建目录
                    var catalogService = serviceProvider.GetService<CatalogService>();
                    var catalogRows = catalogService.GetCount(z => true);
                    if(catalogRows <= 0)
                    {
                        await catalogService.InitCatalog();
                    }
                    var articleService = serviceProvider.GetService<ArticleService>();
                    var articleRows = articleService.GetCount(w => true);
                    if(articleRows <= 0)
                    {
                        await articleService.InitArticle();
                    }
                    break;
                case InstallOrUpdate.Update:
                    //更新
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        /// <summary>
        /// 删除模块时需要执行的业务
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="unsinstallFunc"></param>
        /// <returns></returns>
        public override async Task UninstallAsync(IServiceProvider serviceProvider, Func<Task> unsinstallFunc)
        {
            MLEntities mlEntities = serviceProvider.GetService<MLEntities>();
            
            //指定需要删除的数据实体

            //注意：这里作为演示，删除了所有的表，实际操作过程中，请谨慎操作，并且按照删除顺序对实体进行排序！
            var dropTableKeys = EntitySetKeys.GetEntitySetInfo(this.XscfDatabaseDbContextType).Keys.ToArray();
            await base.DropTablesAsync(serviceProvider, mlEntities, dropTableKeys);

            await unsinstallFunc().ConfigureAwait(false);
        }

        #endregion

        #region IAreaRegister 接口

        public string HomeUrl => "/Admin/Document/Index";

        public List<AreaPageMenuItem> AareaPageMenuItems => new List<AreaPageMenuItem>() {
             new AreaPageMenuItem(GetAreaHomeUrl(),"目录管理","fa fa-laptop"),
             new AreaPageMenuItem(GetAreaUrl("/Admin/DocsArticle/Index"),"内容管理","fa fa-bookmark-o"),
             new AreaPageMenuItem(GetAreaUrl("/Admin/MyApp/Index"),"随机目录生成","fa fa-bookmark-o"),
        };

        public IMvcBuilder AuthorizeConfig(IMvcBuilder builder, IWebHostEnvironment env)
        {
            builder.AddRazorPagesOptions(options =>
            {
                //此处可配置页面权限
            });

            SenparcTrace.SendCustomLog("系统启动", "完成 Area:MyApp 注册");

            return builder;
        }

        public override IServiceCollection AddXscfModule(IServiceCollection services, IConfiguration configuration)
        {
            //任何需要注册的对象
            return base.AddXscfModule(services,configuration);
        }

        #endregion

        #region IXscfDatabase 接口

        /// <summary>
        /// 数据库前缀
        /// </summary>
        public const string DATABASE_PREFIX = "Docs_";

        /// <summary>
        /// 数据库前缀
        /// </summary>
        public string DatabaseUniquePrefix => DATABASE_PREFIX;
        /// <summary>
        /// 设置 XscfSenparcEntities 类型
        /// </summary>
        public Type XscfDatabaseDbContextType => typeof(MLEntities);


        public void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new Docs_CatalogConfigurationMapping());
            modelBuilder.ApplyConfiguration(new Docs_ArticleConfigurationMapping());
        }

        public void AddXscfDatabaseModule(IServiceCollection services)
        {
            //add catalog
            services.AddScoped(typeof(Catalog));
            services.AddScoped(typeof(CatalogDto));
            services.AddScoped(typeof(CatalogService));
            //add article
            services.AddScoped(typeof(Article));
            services.AddScoped(typeof(ArticleDto));
            services.AddScoped(typeof(ArticleService));
        }

        #endregion

        #region IXscfRazorRuntimeCompilation 接口
        public string LibraryPath => Path.GetFullPath(Path.Combine(SiteConfig.WebRootPath, "..", "ML.Xscf.Docs"));
        #endregion
    }
}
