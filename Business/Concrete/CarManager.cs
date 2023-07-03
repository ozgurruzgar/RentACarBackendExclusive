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
            return new SuccessResult();
        }

        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new SuccessResult();
        }

        public async Task<IDataResult<List<Car>>> GetAllAsync()
        {
           var cars = await _carDal.GetAllAsync();
           var carList = new SuccessDataResult<List<Car>>(cars);
           return carList;
        }

        public async Task<IDataResult<Car>> GetAsync(int carId)
        {
            var car = await _carDal.GetAsync(c => c.CarId == carId);
            return new SuccessDataResult<Car>(car);
        }

        public IResult Update(Car car)
        {
           _carDal.Update(car);
            return new SuccessResult();
        }
    }
}
