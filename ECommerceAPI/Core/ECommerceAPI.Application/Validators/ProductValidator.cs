using ECommerceAPI.Application.Dtos.Products;
using FluentValidation;

namespace ECommerceAPI.Application.Validators
{
    public class ProductValidator : AbstractValidator<ProductDto>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .NotNull().WithMessage("Ürün ismi zorunludur.")
                .MinimumLength(3).WithMessage("Ürün ismi en az 3 karakter olmalıdır.")
                .MaximumLength(100).WithMessage("Ürün ismi en fazla 100 karakter olabilir.");

        }
    }
}
