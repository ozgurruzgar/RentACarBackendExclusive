using Business.Abstract;
using Business.Contants;
using Business.ValidationRules.FluentValidation;
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
    public class BrandManager : IBrandService
    {
        private readonly IBrandDal _brandDal;
        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }
        [ValidationAspect(typeof(BrandValidation))]
        public IResult Add(Brand brand)
        {
            var result = BusinessRules.Run(CheckIfBrandNameLengthExceeded(brand.BrandName));
            if (result != null)
            {
                return result;
            }
            _brandDal.Add(brand);
          return new SuccessResult(Messages.CarAdded);
        }

        public IResult Delete(Brand brand)
        {
            _brandDal.Delete(brand);
            return new SuccessResult(Messages.BrandDeleted);
        }

        public  IDataResult<List<Brand>> GetAllAsync()
        {
           var brands =  _brandDal.GetAllAsync().Result;
            return new SuccessDataResult<List<Brand>>(brands,Messages.BrandListed);
        }

        public  IDataResult<Brand> GetAsync(int brandId)
        {
            var brand =  _brandDal.GetAsync(b => b.BrandId == brandId).Result;
            return new SuccessDataResult<Brand>(brand,Messages.BroughtExpectedBrand);
        }

        public IResult Update(Brand brand)
        {
            _brandDal.Update(brand);
            return new SuccessResult(Messages.BrandUpdated);
        }
        private IResult CheckIfBrandNameLengthExceeded(string brandName)
        {
            if (brandName.Length > 30)
            {
                return new ErrorResult(Messages.BrandNameCharacterLimitExceeded);
            }
            return new SuccessResult();
        }
    }
}
