using Microsoft.AspNetCore.Mvc;
using RYDesign.AspNetCore;
using SystemManagement.AppService.SystemMenus;
using SystemManagement.AppService.SystemMenus.Dtos;
using SystemManagement.Domain;

namespace SystemManagement.HttpApi.SystemMenus
{
    /// <summary>
    /// 菜单内容
    /// </summary>
    /// <param name="systemMenuAppService"></param>
    [ApiController]
    [Route("api/SystemManagement/[controller]/[action]")]
    [Area(SystemManagemementConsts.ApplicationName)]

    public class SystemMenuController(ISystemMenuAppService systemMenuAppService):RYDesignControllerBase
    {

        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<bool> CreateSystemMenuAsync(CreateSystemMenuInputDto input)
        {
            return systemMenuAppService.CreateSystemMenuAsync(input);
        }
    }
}
