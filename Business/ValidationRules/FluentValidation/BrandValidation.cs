using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class BrandValidation:AbstractValidator<Brand>
    {
        public BrandValidation()
        {
            RuleFor(b => b.BrandName).NotEmpty();
            RuleFor(b => b.BrandName).MinimumLength(3);
        }
    }     
}
