using Microsoft.AspNetCore.Mvc;
using RYDesign.AspNetCore;
using SystemManagement.AppService.SystemRoles;
using SystemManagement.AppService.SystemRoles.Dtos.Request;
using SystemManagement.AppService.SystemRoles.Dtos.Response;
using SystemManagement.Domain;

namespace SystemManagement.HttpApi.SystemRoles
{
    /// <summary>
    /// 系统角色
    /// </summary>
    [ApiController]
    [Route("api/SystemManagement/[controller]/[action]")]
    [Area(SystemManagemementConsts.ApplicationName)]
    public class SystemRoleController(ISystemRoleAppService systemRoleAppService): RYDesignControllerBase
    {
       

        /// <summary>
        /// 获取系统角色分页列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<GetSystemRolePagedListResponse> GetSystemRolePagedListAsync(GetSystemRolePagedListRequest input)
        {
            return systemRoleAppService.GetSystemRolePagedListAsync(input, HttpContext.RequestAborted);
        }

        /// <summary>
        /// 获取系统角色
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        public  Task<GetSystemRoleResponse> GetSystemRoleAsync([FromBody] Guid Id)
        {
            return systemRoleAppService.GetSystemRoleAsync(Id);
        }

        /// <summary>
        /// 创建系统角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<bool> CreateSysteRoleAsync(CreateSystemRoleRequest input)
        {
            return systemRoleAppService.CreateSysteRoleAsync(input, HttpContext.RequestAborted);
        }
    }
}
