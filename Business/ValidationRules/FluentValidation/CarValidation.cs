using Entities.Concrete;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class CarValidation:AbstractValidator<Car>
    {
        public CarValidation() 
        {
            RuleFor(c=>c.BrandId).NotNull();
            RuleFor(c=>c.ColorId).NotNull();            
            RuleFor(c=>c.BrandId).NotEqual(0);
            RuleFor(c=>c.ColorId).NotEqual(0);
            RuleFor(c => c.ModelYear).GreaterThan((short)2010);
            RuleFor(c => c.ModelYear).LessThanOrEqualTo((short)DateTime.Now.Year);
            RuleFor(c => c.Model).NotEmpty();
            RuleFor(c => c.Model).MinimumLength(3);
            RuleFor(c => c.DailyPrice).NotNull();
            RuleFor(c => c.DailyPrice).GreaterThan((decimal)100);

            
        }
    }
}
