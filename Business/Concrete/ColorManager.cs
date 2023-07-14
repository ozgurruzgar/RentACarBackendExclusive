using Business.Abstract;
using Business.Contants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ColorManager : IColorService
    {
        private readonly IColorDal _colorDal;
        public ColorManager(IColorDal colorDal)
        {
            _colorDal = colorDal; 
        }
        [ValidationAspect(typeof(ColorValidation))]
        public IResult Add(Color color)
        {
            var result = BusinessRules.Run(CheckIfColorNameLengthExceeded(color.ColorName));
            if(result != null)
            {
                return result;
            }
           _colorDal.Add(color);
            return new SuccessResult(Messages.ColorAdded);
        }
        public IResult Delete(Color color)
        {
            _colorDal.Delete(color);
            return new SuccessResult(Messages.ColorDeleted);
        }
        [CacheAspect<List<Color>>]
        public IDataResult<List<Color>> GetAllAsync()
        {
            var colors =  _colorDal.GetAllAsync().Result;
            return new SuccessDataResult<List<Color>>(colors,Messages.ColorListed);
        }
        [CacheAspect<Color>]
        public  IDataResult<Color> GetAsync(int colorId)
        {
            var color =  _colorDal.GetAsync(c => c.ColorId == colorId).Result;
            return new SuccessDataResult<Color>(color,Messages.BroughtExpectedColor);
        }

        public IResult Update(Color color)
        {
           _colorDal.Update(color);
            return new SuccessResult(Messages.ColorUpdated);
        }

        private IResult CheckIfColorNameLengthExceeded(string colorName)
        {
            if (colorName.Length >= 30)
            {
                return new ErrorResult(Messages.ColorNameCharacterLimitExceeded);
            }
            return new SuccessResult();
        }
    }
}
