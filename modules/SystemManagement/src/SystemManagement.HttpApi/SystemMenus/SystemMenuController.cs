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
        /// 返回菜单列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<GetSystemMenuListResponse> GetSystemMenuListAsync(GetSystemMenuListInputDto input)
        {
            return systemMenuAppService.GetSystemMenuListAsync(input,HttpContext.RequestAborted);
        }

        /// <summary>
        /// 查询菜单信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<SystemMenuDto> GetSystemMenuAsync(Guid id)
        {
            return systemMenuAppService.GetSystemMenuAsync(id, HttpContext.RequestAborted);
        }

        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<bool> CreateSystemMenuAsync(CreateSystemMenuInputDto input)
        {
            return systemMenuAppService.CreateSystemMenuAsync(input, HttpContext.RequestAborted);
        }

        /// <summary>
        /// 删除菜单内容
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<bool> DeleteSystemMenuAsync(Guid id)
        {
            return systemMenuAppService.DeleteSystemMenuAsync(id, HttpContext.RequestAborted);
        }
    }
}
