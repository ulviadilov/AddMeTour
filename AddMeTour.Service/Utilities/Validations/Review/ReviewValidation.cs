using AddMeTour.Entity.ViewModels.Review;
using FluentValidation;

namespace AddMeTour.Service.Utilities.Validations.Review;

public class ReviewValidation: AbstractValidator<ReviewVM>
{
    public ReviewValidation()
    {
        RuleFor(c => c.Name).NotEmpty().WithMessage("User name is empty!").NotNull().MinimumLength(2);
        RuleFor(c => c.Email).NotEmpty().NotNull().WithMessage("Email is empty!");
        RuleFor(c => c.Message).NotEmpty().NotNull().MinimumLength(2).WithMessage("Minimum 2 letter!");
    }
}
