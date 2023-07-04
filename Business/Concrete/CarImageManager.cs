using Business.Abstract;
using Business.Contants;
using Core.Utilities.Business;
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
            Task<IResult> result = BusinessRules.RunTasks(CheckIfCarImageLimitExceeded(carImage.CarId));
            if (result.Result !=null)
            {
                return result.Result;   
            }
            carImage.ImagePath = FileHelper.Add(file);
            carImage.Date = DateTime.Now;
            _carImageDal.Add(carImage);
            return new SuccessResult(Messages.CarImageAdded);
        }

        public IResult Delete(CarImage carImage)
        {
           IResult result = BusinessRules.Run(CarImageDelete(carImage));
            if (result != null)
            {
                return result;
            }
            _carImageDal.Delete(carImage);
            return new SuccessResult(Messages.CarImageDeleted);
        }

        public async Task<IDataResult<List<CarImage>>> GetAllAsync()
        {
            var carImages = await _carImageDal.GetAllAsync();
            return new SuccessDataResult<List<CarImage>>(carImages,Messages.CarImageListed);
           
        }

        public async Task<IDataResult<CarImage>> GetAsync(int id)
        {
            var carImage = await _carImageDal.GetAsync(c=>c.Id == id);
            return new SuccessDataResult<CarImage>(carImage,Messages.BroughtExpectedCarImage);
        }

        public async Task<IDataResult<List<CarImage>>> GetByCarIdAsync(int carId)
        {
            var carImagesByCarId = await _carImageDal.GetAllAsync(c => c.CarId == carId);
            return new SuccessDataResult<List<CarImage>>(carImagesByCarId,Messages.BroughtExpectedCarIamgeByCarId);
        }

        public async Task<IResult> Update(IFormFile file, CarImage carImage)
        {
            Task<IResult> result = BusinessRules.RunTasks(CheckIfCarImageLimitExceeded(carImage.CarId));
            if (result.Result != null)
            {
                return result.Result;
            }
            var oldImageId = await GetAsync(carImage.Id);
            carImage.Date = DateTime.Now;
            string oldPath = oldImageId.Data.ImagePath;
            carImage.ImagePath = FileHelper.Update(oldPath, file);
            return new SuccessResult(Messages.CarImageUpdated);
        }

        private IResult CarImageDelete(CarImage carImage)
        {
            try
            {
                _carImageDal.Delete(carImage);
            }
            catch (Exception exception)
            {
                return new ErrorResult(exception.Message);
            }
            return new SuccessResult();
        }

        private async Task<List<CarImage>> CheckIfCarImageNull(int carId)
        {
            string path = @"DefaultIamge.jpg";
            var result = await _carImageDal.GetAllAsync(c => c.CarId == carId);
            if (result.Any() == false)
            {
                return new List<CarImage> { new CarImage { CarId = carId, ImagePath = path, Date = DateTime.Now } };
            }
            var carImageList = await _carImageDal.GetAllAsync(c => c.CarId == carId);
            return carImageList;
        }

        private async Task<IResult> CheckIfCarImageLimitExceeded(int carId)
        {
            var carImageList = await _carImageDal.GetAllAsync(c=>c.CarId == carId);
            var list = carImageList.Count; 
            if(list > 15)
            {
                return new ErrorResult(Messages.CarImageLimitExceeded);
            }
            return new SuccessResult();
        }
    }
}
