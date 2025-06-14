using eBlog.Application.DTOs;
using FluentValidation;

namespace eBlog.Application.Validators
{
    public class FavoriteCreateDtoValidator : AbstractValidator<FavoriteCreateDto>
    {
        public FavoriteCreateDtoValidator()
        {
            RuleFor(x => x)
                .Must(x => x.PostId != null || x.ProductId != null || x.CommentId != null)
                .WithMessage("En az bir hedef (Post, Product veya Comment) seçilmelidir.");
        }
    }
}
