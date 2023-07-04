using Business.Abstract;
using Business.Contants;
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
        public IResult Add(Color color)
        {
            var result = BusinessRules.Run(CheckIfColorNameLengthExceeded(color.ColorName));
           _colorDal.Add(color);
            return new SuccessResult(Messages.ColorAdded);
        }

        public IResult Delete(Color color)
        {
            _colorDal.Delete(color);
            return new SuccessResult(Messages.ColorDeleted);
        }

        public async Task<IDataResult<List<Color>>> GetAllAsync()
        {
            var colors = await _colorDal.GetAllAsync();
            return new SuccessDataResult<List<Color>>(colors,Messages.ColorListed);
        }

        public async Task<IDataResult<Color>> GetAsync(int colorId)
        {
            var color = await _colorDal.GetAsync(c => c.ColorId == colorId);
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
