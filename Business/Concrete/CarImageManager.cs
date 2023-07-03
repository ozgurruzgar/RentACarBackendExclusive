using Business.Abstract;
using Core.Utilities.Helpers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        private readonly ICarImageDal _carImageDal;
        public IResult Add(IFormFile file, CarImage carImage)
        {
            carImage.ImagePath = FileHelper.Add(file);
            carImage.Date = DateTime.Now;
            _carImageDal.Add(carImage);
            return new SuccessResult();
        }

        public IResult Delete(CarImage carImage)
        {
           _carImageDal.Delete(carImage);
            return new SuccessResult();
        }

        public async Task<IDataResult<List<CarImage>>> GetAllAsync()
        {
            var carImages = await _carImageDal.GetAllAsync();
            return new SuccessDataResult<List<CarImage>>(carImages);
           
        }

        public async Task<IDataResult<CarImage>> GetAsync(int id)
        {
            var carImage = await _carImageDal.GetAsync(c=>c.Id == id);
            return new SuccessDataResult<CarImage>(carImage);
        }

        public async Task<IDataResult<List<CarImage>>> GetByCarIdAsync(int carId)
        {
            var carImagesByCarId = await _carImageDal.GetAllAsync(c => c.CarId == carId);
            return new SuccessDataResult<List<CarImage>>(carImagesByCarId);
        }

        public async Task<IResult> Update(IFormFile file, CarImage carImage)
        {
            var oldImageId = await GetAsync(carImage.Id);
            carImage.Date = DateTime.Now;
            string oldPath = oldImageId.Data.ImagePath;
            carImage.ImagePath = FileHelper.Update(oldPath, file);
            return new SuccessResult();
        }
    }
}
