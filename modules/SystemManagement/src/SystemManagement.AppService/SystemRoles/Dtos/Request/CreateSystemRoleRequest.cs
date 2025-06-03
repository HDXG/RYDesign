using FluentValidation;

namespace SystemManagement.AppService.SystemRoles.Dtos.Request
{
    public record CreateSystemRoleRequest(string RoleName,string Describe,int OrderIndex,bool IsDefault,
        List<CreateSystemRoleMenuDto> childer);

    public class CreateSystemRoleRequestValidator : AbstractValidator<CreateSystemRoleRequest>
    {
        public CreateSystemRoleRequestValidator()
        {
            RuleFor(x => x.RoleName).Must(x => !x.IsNullOrWhiteSpace());
        }
    }

    public record CreateSystemRoleMenuDto(Guid value, string lable);
}
