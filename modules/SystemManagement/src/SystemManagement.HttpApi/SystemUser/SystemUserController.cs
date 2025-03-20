using Microsoft.AspNetCore.Mvc;
using RYDesign.AspNetCore;
using SystemManagement.Domain;

namespace SystemManagement.HttpApi.SystemUser
{
    /// <summary>
    /// 用户内容
    /// </summary>
    [ApiController]
    [Route("api/SystemManagement/[controller]/[action]")]
    [Area(SystemManagemementConsts.ApplicationName)]
    public class SystemUserController : RYDesignControllerBase
    {
        /// <summary>
        /// 测试内容
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string GetDemo() => "123456";
    }
}
