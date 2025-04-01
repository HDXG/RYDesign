using FluentValidation;

namespace SystemManagement.AppService.SystemMenus.Dtos
{
    public class CreateSystemMenuInputDto
    {
        public string MenuName { get; set; }

        public string MenuPath { get; set; }

        public int MenuType { get; set; }

        public string Icon { get; set; }

        public string PermissionKey { get; set; }

        public Guid? ParentId { get; set; }

        public string RouteName { get; set; }

        public string ComponentPath { get; set; }

        public string Remark { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        public int OrderIndex { get; set; }

        /// <summary>
        /// 子菜单
        /// </summary>
        public List<CreateSystemMenuInputDto> Childrens { get; set; }
    }

    public class CreateSystemMenuInputDtoValidator : AbstractValidator<CreateSystemMenuInputDto>
    {
        public CreateSystemMenuInputDtoValidator()
        {
            RuleFor(x => x.MenuName).Must(x => !x.IsNullOrWhiteSpace());
            RuleFor(x => x.MenuPath).Must(x => !x.IsNullOrWhiteSpace());
            RuleFor(x => x.Icon).Must(x => !x.IsNullOrWhiteSpace());
            RuleFor(x => x.ComponentPath).Must(x => !x.IsNullOrWhiteSpace());
        }
    }
}
