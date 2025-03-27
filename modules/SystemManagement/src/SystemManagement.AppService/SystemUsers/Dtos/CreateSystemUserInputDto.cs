using FluentValidation;

namespace SystemManagement.AppService.SystemUsers.Dtos
{
   
    public record CreateSystemUserInputDto(string UserName,string AccountNumber,string PassWord, CreateUserRoleDto[] CreateUserRoles);

    public record CreateUserRoleDto(Guid RoleId,string RoleName);

    public class CreateSystemUserInputDtoValidator : AbstractValidator<CreateSystemUserInputDto>
    {
        public CreateSystemUserInputDtoValidator()
        {
            RuleFor(x => x.UserName).Must(x => !x.IsNullOrWhiteSpace());
            RuleFor(x => x.AccountNumber).Must(x => !x.IsNullOrWhiteSpace());
            RuleFor(x => x.PassWord).Must(x => !x.IsNullOrWhiteSpace());
        }
    }
}
