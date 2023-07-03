using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class CustomerValidation:AbstractValidator<Customer>
    {
        public CustomerValidation()
        {
            RuleFor(c => c.CustomerId).NotEmpty();
            RuleFor(c => c.CustomerId).GreaterThan(0);
            RuleFor(c => c.CompanyName).NotEmpty();
            RuleFor(c => c.CompanyName).MaximumLength(3);
            RuleFor(c=>c.Email).NotEmpty();
            RuleFor(c=>c.Email).MinimumLength(20);
        }
    }    
}
