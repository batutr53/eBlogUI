using eBlog.Application.DTOs.Auth;
using FluentValidation;

namespace eBlog.Application.Validators
{
 
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
        }
    }

}
