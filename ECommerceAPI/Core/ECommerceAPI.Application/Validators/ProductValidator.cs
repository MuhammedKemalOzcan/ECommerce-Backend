using ECommerceAPI.Application.Features.Commands.Products.CreateProduct;
using FluentValidation;

namespace ECommerceAPI.Application.Validators
{
    public class ProductValidator : AbstractValidator<CreateProductCommandRequest>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .NotNull().WithMessage("Ürün ismi zorunludur.")
                .MinimumLength(3).WithMessage("Ürün ismi en az 3 karakter olmalıdır.")
                .MaximumLength(100).WithMessage("Ürün ismi en fazla 100 karakter olabilir.");

            RuleFor(p => p.Description)
                .MaximumLength(500).WithMessage("Ürün açıklaması en fazla 500 karakter olabilir.");

            RuleFor(p => p.Features)
                .MaximumLength(1000).WithMessage("Ürün özelliği en fazla 1000 karakter olabilir.");

            RuleFor(p => p.Category)
                .NotEmpty()
                .NotNull().WithMessage("Kategori seçimi zorunludur.");

            RuleFor(p => p.Price)
                .NotEmpty()
                .NotNull().WithMessage("Fiyat bilgisi zorunludur.")
                .GreaterThanOrEqualTo(1).WithMessage("Fiyat 0 olamaz");

            RuleFor(p => p.Stock)
                .NotEmpty()
                .NotNull().WithMessage("Stok bilgisi zorunludur")
                .GreaterThanOrEqualTo(0).WithMessage("Geçerli bir stok bilgisi giriniz.");

        }
    }
}
