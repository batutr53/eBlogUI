using eBlog.Application.DTOs;
using FluentValidation;

namespace eBlog.Application.Validators
{
    public class PostModuleUpdateDtoValidator : AbstractValidator<PostModuleUpdateDto>
    {
        public PostModuleUpdateDtoValidator()
        {
            RuleFor(x => x.Type).NotEmpty();
            RuleFor(x => x.Content).NotEmpty();
        }
    }
}
