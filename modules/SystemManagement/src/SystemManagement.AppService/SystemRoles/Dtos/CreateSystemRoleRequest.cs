using FluentValidation;

namespace SystemManagement.AppService.SystemRoles.Dtos
{
    public record CreateSystemRoleRequest(string RoleName,string Describe,int OrderIndex,bool IsDefault);

    public class CreateSystemRoleRequestValidator : AbstractValidator<CreateSystemRoleRequest>
    {
        public CreateSystemRoleRequestValidator()
        {
            RuleFor(x => x.RoleName).Must(x => !x.IsNullOrWhiteSpace());
        }
    }
}
