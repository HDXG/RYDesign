using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace RYDesign.AspNetCore
{
    public abstract class RYDesignControllerBase: AbpController
    {
        protected FileContentResult FileByExcel(byte[] bytes, string fileName)
        {
            return File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
    }
}
