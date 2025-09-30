using FluentValidation;
using RealEstateAPI.Core.DTOs;

namespace RealEstateAPI.Core.Validators
{
    public class PropertyDtoValidator : AbstractValidator<PropertyDto>
    {
        public PropertyDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre de la propiedad es requerido.")
                .MaximumLength(100).WithMessage("El nombre no puede exceder los 100 caracteres.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("La dirección de la propiedad es requerida.")
                .MaximumLength(200).WithMessage("La dirección no puede exceder los 200 caracteres.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("El precio debe ser mayor que cero.")
                .LessThan(1000000000).WithMessage("El precio no puede exceder 1,000,000,000.");

            RuleFor(x => x.CodeInternal)
                .NotEmpty().WithMessage("El código interno es requerido.")
                .MaximumLength(50).WithMessage("El código interno no puede exceder los 50 caracteres.")
                .Matches("^[A-Z0-9-_]+$").WithMessage("El código interno solo puede contener letras mayúsculas, números, guiones y guiones bajos.");

            RuleFor(x => x.Year)
                .GreaterThan(1900).WithMessage("El año debe ser posterior a 1900.")
                .LessThanOrEqualTo(DateTime.Now.Year + 1).WithMessage($"El año no puede ser posterior al año {DateTime.Now.Year + 1}.");

            RuleFor(x => x.IdOwner)
                .NotEmpty().WithMessage("El ID del propietario es requerido.")
                .Matches("^[a-fA-F0-9]{24}$").WithMessage("El ID del propietario debe ser un ObjectId válido de MongoDB.");
        }
    }
}
