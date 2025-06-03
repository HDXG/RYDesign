using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace SystemManagement.Domain.SystemRoles
{
    public class System_RoleMenu:Entity<Guid>
    {
        public System_RoleMenu()
        {

        }

        public System_RoleMenu(Guid Id,Guid roleId, Guid menuId, string menuName):base(Id)
        {
            RoleId = roleId;
            MenuId = menuId;
            ChangeMenuName(menuName);
        }

        public Guid RoleId { get; set; }
        
        public Guid MenuId { get; set; }

        public String MenuName { get; set; }

        public void ChangeMenuName(string menuName)
        {
            MenuName = Check.NotNullOrWhiteSpace(menuName, "MenuName");
        }

    }
}
