﻿using Business.Abstract;
using Business.Contants;
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
        public IResult Add(Car car)
        {
            _carDal.Add(car);
            return new SuccessResult(Messages.CarAdded);
        }

        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new SuccessResult(Messages.CarDeleted);
        }

        public async Task<IDataResult<List<Car>>> GetAllAsync()
        {
           var cars = await _carDal.GetAllAsync();
           var carList = new SuccessDataResult<List<Car>>(cars,Messages.CarListed);
           return carList;
        }

        public async Task<IDataResult<Car>> GetAsync(int carId)
        {
            var car = await _carDal.GetAsync(c => c.CarId == carId);
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
