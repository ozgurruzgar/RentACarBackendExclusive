using Business.Abstract;
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
        public IResult Add(Brand brand)
        {
            _brandDal.Add(brand);
          return new SuccessResult();
        }

        public IResult Delete(Brand brand)
        {
            _brandDal.Delete(brand);
            return new SuccessResult();
        }

        public async Task<IDataResult<List<Brand>>> GetAllAsync()
        {
           var brands = await _brandDal.GetAllAsync();
            return new SuccessDataResult<List<Brand>>(brands);
        }

        public async Task<IDataResult<Brand>> GetAsync(int brandId)
        {
            var brand = await _brandDal.GetAsync(b => b.BrandId == brandId);
            return new SuccessDataResult<Brand>(brand);
        }

        public IResult Update(Brand brand)
        {
            _brandDal.Update(brand);
            return new SuccessResult();
        }
    }
}
