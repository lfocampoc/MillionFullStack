using FluentValidation;
using RealEstateAPI.Core.DTOs;

namespace RealEstateAPI.Core.Validators
{
    public class PropertyFilterDtoValidator : AbstractValidator<PropertyFilterDto>
    {
        public PropertyFilterDtoValidator()
        {
            RuleFor(x => x.Name)
                .MaximumLength(100).When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage("El nombre del filtro no puede exceder los 100 caracteres.");

            RuleFor(x => x.Address)
                .MaximumLength(200).When(x => !string.IsNullOrEmpty(x.Address))
                .WithMessage("La dirección del filtro no puede exceder los 200 caracteres.");

            RuleFor(x => x.MinPrice)
                .GreaterThanOrEqualTo(0).When(x => x.MinPrice.HasValue)
                .WithMessage("El precio mínimo no puede ser negativo.");

            RuleFor(x => x.MaxPrice)
                .GreaterThanOrEqualTo(x => x.MinPrice).When(x => x.MinPrice.HasValue && x.MaxPrice.HasValue)
                .WithMessage("El precio máximo no puede ser menor que el precio mínimo.")
                .LessThan(1000000000).When(x => x.MaxPrice.HasValue)
                .WithMessage("El precio máximo no puede exceder 1,000,000,000.");

            RuleFor(x => x.Page)
                .GreaterThanOrEqualTo(1).WithMessage("La página debe ser al menos 1.");

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 100).WithMessage("El tamaño de página debe estar entre 1 y 100.");
        }
    }
}
