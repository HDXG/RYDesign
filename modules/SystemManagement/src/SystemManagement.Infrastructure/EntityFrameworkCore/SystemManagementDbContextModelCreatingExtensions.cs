using Microsoft.EntityFrameworkCore;
using SystemManagement.Domain;
using SystemManagement.Domain.SystemMenus;
using SystemManagement.Domain.SystemRoles;
using SystemManagement.Domain.SystemUsers;
using Volo.Abp;

namespace SystemManagement.Infrastructure.EntityFrameworkCore
{
    public static class SystemManagementDbContextModelCreatingExtensions
    {
        public static void ConfigureProjectName(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            builder.Entity<System_User>(a => {
                a.ToTable("SystemUser", SystemManagemementConsts.DbSchemaName);
                a.HasKey(b => b.Id);

                a.HasMany(e => e.system_UserRoles)
                 .WithOne()
                 .HasPrincipalKey(e => e.Id)
                 .HasForeignKey(a => a.UserId)
                 .IsRequired()
                 .OnDelete(DeleteBehavior.Cascade); ;

            });

            builder.Entity<System_UserRole>(b =>
            {
                b.ToTable("SystemUserRole", SystemManagemementConsts.DbSchemaName);
                b.HasKey(c => new { c.UserId, c.RoleId });
            });

            builder.Entity<System_Role>(a =>
            {
                a.ToTable("SystemRole", SystemManagemementConsts.DbSchemaName);
                a.HasKey(c => c.Id);
            });

            builder.Entity<System_Menu>(a =>
            {
                a.ToTable("SystemMenu", SystemManagemementConsts.DbSchemaName);
                a.HasKey(b => b.Id);
                a.HasMany(c => c.SubMenus).WithOne().HasForeignKey("ParentId").OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
