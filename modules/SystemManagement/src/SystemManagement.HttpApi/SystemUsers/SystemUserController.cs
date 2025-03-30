using Microsoft.AspNetCore.Mvc;
using RYDesign.AspNetCore;
using SystemManagement.AppService.SystemUsers;
using SystemManagement.AppService.SystemUsers.Dtos;
using SystemManagement.Domain;

namespace SystemManagement.HttpApi.SystemUsers
{
    /// <summary>
    /// 用户内容
    /// </summary>
    [ApiController]
    [Route("api/SystemManagement/[controller]/[action]")]
    [Area(SystemManagemementConsts.ApplicationName)]
    public class SystemUserController(ISystemUserAppServicce systemUserAppServicce)
        : RYDesignControllerBase
    {

        /// <summary>
        /// 获取用户分页列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public  Task<GetSystemUserPagedListResponse> GetSystemUserPagedListAsync([FromBody]   GetSystemUserPagedListInputDto input)
        {
            return systemUserAppServicce.GetSystemUserPagedListAsync(input,HttpContext.RequestAborted);
        }

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<GetSystemUserResponse> GetSystemUserAsync([FromBody]Guid id)
        {
            return systemUserAppServicce.GetSystemUserAsync(id, HttpContext.RequestAborted);
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<bool> CreateSystemUserAsync(CreateSystemUserInputDto input)
        {
            return systemUserAppServicce.CreateSystemUserAsync(input, HttpContext.RequestAborted);
        }
    }
}
