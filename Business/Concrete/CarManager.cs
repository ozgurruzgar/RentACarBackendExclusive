using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Contants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Entities;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTO_s;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        private readonly ICarDal _carDal;
        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }
        [ValidationAspect(typeof(CarValidation))]
        public IResult Add(Car car)
        {
            _carDal.Add(car);
            return new SuccessResult(Messages.CarAdded);
        }
        //[TransactionScopeAspect]
        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new SuccessResult(Messages.CarDeleted);
        }
        //[SecuredOperation("admin")]
        [CacheAspect<List<Car>>] 
        //[CacheRemoveAspect("Business.Abstract.ICarService.GetAllAsync()")]
        public  IDataResult<List<Car>> GetAllAsync()
        {
           var cars =  _carDal.GetAllAsync().Result;
           var carList = new SuccessDataResult<List<Car>>(cars,Messages.CarListed);
           return carList;
        }
        [CacheAspect<Car>]
        public  IDataResult<Car> GetAsync(int carId)
        {
            var car = _carDal.GetAsync(c => c.CarId == carId).Result;
            return new SuccessDataResult<Car>(car,Messages.BroughtExpectedCar);
        }

        public IDataResult<List<CarDetailDto>> GetCarDetail()
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetail(),Messages.ListedCarDetail);
        }

        public IDataResult<List<CarDetailDto>> GetCarDetailByBrandAndColorId(int brandId, int colorId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetailByBrandAndColorId(brandId,colorId),Messages.ListedCarDetailByBrandAndColor);
        }

        public IDataResult<List<CarDetailDto>> GetCarDetailByBrandId(int brandId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetailByBrandId(brandId),Messages.ListedCarDetailByBrandId);
        }

        public IDataResult<List<CarDetailDto>> GetCarDetailByCarId(int carId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetailByCarId(carId),Messages.ListedCarDetailByCarId);
        }

        public IDataResult<List<CarDetailDto>> GetCarDetailByColorId(int colorId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetailByColorId(colorId),Messages.ListedCarDetailByColorId);
        }

        public IResult Update(Car car)
        {
           _carDal.Update(car);
            return new SuccessResult(Messages.CarUpdated);
        }
    }
}
