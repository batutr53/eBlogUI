using eBlog.Application.DTOs;
using FluentValidation;

namespace eBlog.Application.Validators
{
    public class FollowCreateDtoValidator : AbstractValidator<FollowCreateDto>
    {
        public FollowCreateDtoValidator()
        {
            RuleFor(x => x.FollowingId).NotEmpty().WithMessage("Takip edilecek kullanıcı seçilmelidir.");
        }
    }
}
