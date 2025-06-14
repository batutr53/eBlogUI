using eBlog.Application.DTOs;
using FluentValidation;

namespace eBlog.Application.Validators
{
    public class CommentCreateDtoValidator : AbstractValidator<CommentCreateDto>
    {
        public CommentCreateDtoValidator()
        {
            RuleFor(x => x.Content).NotEmpty().MaximumLength(1000);
        }
    }
}
