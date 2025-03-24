using FluentValidation;

namespace SystemManagement.AppService.SystemUsers.Dtos
{
    public class CreateSystemUserInputDto
    {
        public string UserName { get; set; }

        public string AccountNumber { get; set; }

        public string PassWord { get; set; }

    }

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
