using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ML.Xscf.Docs.Models.VD;
using Senparc.Scf.Core.Cache;
using Senparc.Scf.Core.Models;

namespace ML.Xscf.Docs
{
    public class IndexModel : BasePageModel
    {
        public async Task<IActionResult> OnGetAsync()
        {
            return Page();
        }
    }
}