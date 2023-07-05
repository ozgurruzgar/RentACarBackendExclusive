using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class CustomerValidation:AbstractValidator<Customer>
    {
        public CustomerValidation()
        {
            RuleFor(c => c.CompanyName).NotEmpty();
            RuleFor(c => c.CompanyName).MaximumLength(3);
        }
    }    
}
