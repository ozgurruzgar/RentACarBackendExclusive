using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class RentalValidation:AbstractValidator<Rental>
    {
        public RentalValidation()
        {
            RuleFor(r => r.CarId).NotEmpty();
            RuleFor(r => r.CarId).NotEmpty();
            RuleFor(r => r.CustomerId).NotEmpty();
            RuleFor(r => r.CustomerId).NotEmpty();
            RuleFor(r => r.RentDate).Empty().When(r=>r.ReturnDate==null);
        }
    }
}
