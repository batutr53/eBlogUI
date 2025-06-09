using eBlog.Application.DTOs;
using FluentValidation;

namespace eBlog.Application.Validators
{
    public class ProductOrderCreateDtoValidator : AbstractValidator<ProductOrderCreateDto>
    {
        public ProductOrderCreateDtoValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty();
            RuleFor(x => x.Quantity).GreaterThan(0);
        }
    }
}
