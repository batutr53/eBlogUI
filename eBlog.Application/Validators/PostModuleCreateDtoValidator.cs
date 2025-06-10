using eBlog.Application.DTOs;
using FluentValidation;

namespace eBlog.Application.Validators
{
    public class PostModuleCreateDtoValidator : AbstractValidator<PostModuleCreateDto>
    {
        public PostModuleCreateDtoValidator()
        {
            RuleFor(x => x.Type).NotEmpty();
            RuleFor(x => x.Content).NotEmpty();
        }
    }
}
