using Microsoft.AspNetCore.Mvc;
using ML.Xscf.Docs.Models.VD;
using Senparc.Scf.AreaBase.Admin;
using Senparc.Scf.AreaBase.Admin.Filters;
using Senparc.Scf.Core.Models.VD;
using Senparc.Scf.Core.WorkContext;
using Senparc.Scf.XscfBase;

namespace ML.Xscf.Docs
{

    public interface IBaseAdminPageModel : IBasePageModel
    {

    }

    //暂时取消权限验证
    //[ServiceFilter(typeof(AuthenticationAsyncPageFilterAttribute))]
    [AdminAuthorize("AdminOnly")]
    public class BaseAdminPageModel : AdminPageModelBase, IBaseAdminPageModel
    {
        public ML.Xscf.Docs.Register _xscfRegister;
        public ML.Xscf.Docs.Register XscfRegister
        {
            get
            {
                _xscfRegister = _xscfRegister ?? new Register();
                return _xscfRegister;
            }
        }

        public override IActionResult RenderError(string message)
        {
            return base.RenderError(message);
        }
    }
}
