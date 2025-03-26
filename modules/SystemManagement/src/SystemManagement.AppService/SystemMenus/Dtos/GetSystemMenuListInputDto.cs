using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RYDesign.Application.Contracts.Dtos;

namespace SystemManagement.AppService.SystemMenus.Dtos
{
    public class GetSystemMenuListInputDto:RYPagedRequestDto
    {
        public string MenuName { get; set; }

        public string MenuPath { get; set; }
    }
}
