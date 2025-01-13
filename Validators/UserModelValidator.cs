using FluentValidation;
using lab1.Models;
public class UserModelValidator : AbstractValidator<UserModel>
{
    public UserModelValidator()
    {
        RuleFor(user => user.Name)
            .NotEmpty().WithMessage("Name is required.")
            .Length(3, 50).WithMessage("Name must be between 3 and 50 characters.");
        RuleFor(user => user.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");
        RuleFor(user => user.Age)
            .InclusiveBetween(18, 100).WithMessage("Age must be between 18 and 100.");
    }
}