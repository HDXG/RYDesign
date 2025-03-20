using Microsoft.EntityFrameworkCore;
using RYDesign.EntityFrameworkCore.EntityFrameworkCore;
using SystemManagement.Domain;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace SystemManagement.Infrastructure.EntityFrameworkCore
{
    [ConnectionStringName(SystemManagemementConsts.ConnectionStringName)]
    public class SystemManagementDbContext(DbContextOptions<SystemManagementDbContext> dbContextOptions) : RYDesignEfCoreContext<SystemManagementDbContext>(dbContextOptions)
    {




        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ConfigureProjectName();
        }
    }
}
