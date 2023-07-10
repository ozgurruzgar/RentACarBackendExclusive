using Business.Abstract;
using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Controllers;

namespace WebAPI.Test
{
    public class CarsControllerTest
    {
        private readonly Mock<ICarService> _mockRepo;
        private readonly CarsController _carsController;
        public IDataResult<List<Car>> _cars;
        public CarsControllerTest()
        {
            _mockRepo = new Mock<ICarService>();
            _carsController = new CarsController(_mockRepo.Object);
            _cars = new SuccessDataResult<List<Car>>();
            _cars.Data = new List<Car>()
            {
                new Car {CarId=1,BrandId=1,ColorId=1,Model="320İ",ModelYear=2012,DailyPrice=2500,Description="BMW 320İ"},
                new Car {CarId=2,BrandId=2,ColorId=2,Model="520İ",ModelYear=2012,DailyPrice=3500,Description="BMW 520İ"}
            };
        }
        [Fact]
        public async void GetAllAsync_ActionExecutes_ReturnOkWithCarList()
        {
            var setup = _mockRepo.Setup(c => c.GetAllAsync()).ReturnsAsync(_cars);
            var result = await _carsController.GetAllAsync();
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var returnCarList = Assert.IsAssignableFrom<SuccessDataResult<List<Car>>>(actionResult.Value);
            Assert.Equal<int>(200, actionResult.StatusCode.Value);
        }
        [Fact]
        public async void GetAllAsync_IsInErrorDataResult_ReturnBadRequest()
        {
            ErrorDataResult<List<Car>> errorDataResult = new ErrorDataResult<List<Car>>();
            var setup = _mockRepo.Setup(c => c.GetAllAsync()).ReturnsAsync(errorDataResult);
            var result = await _carsController.GetAllAsync();
            var actionResult = Assert.IsType<BadRequestObjectResult>(result);
            var returnErrorDataResult = Assert.IsAssignableFrom<ErrorDataResult<List<Car>>>(actionResult.Value);
            Assert.Equal<int>(400, actionResult.StatusCode.Value);
        }
        [Theory]
        [InlineData(1)]
        public async void GetByIdAsync_IsValidCarId_ReturnOkWithCar(int carId)
        {
            var successDataResult = new SuccessDataResult<Car>(_cars.Data.First(c => c.CarId == carId));
            var setup = _mockRepo.Setup(c => c.GetAsync(carId)).ReturnsAsync(successDataResult);
            var result = await _carsController.GetByIdAsync(carId);
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var returnExcpectedCar = Assert.IsAssignableFrom<SuccessDataResult<Car>>(actionResult.Value);
        }
        [Theory]
        [InlineData(3)]
        public async void GetByIdAsync_IsInValidCarId_ReturnBadRequest(int carId)
        {
            var errorDataResult = new ErrorDataResult<Car>();
            var setup = _mockRepo.Setup(c => c.GetAsync(carId)).ReturnsAsync(errorDataResult);
            var result = await _carsController.GetByIdAsync(carId);
            var actionResult = Assert.IsType<BadRequestObjectResult>(result);
            var returnErrorDataResult = Assert.IsAssignableFrom<ErrorDataResult<Car>>(actionResult.Value);
        }
        [Fact]
        public void Add_IsValidCar_AddCarAndReturnOk()
        {
            SuccessResult successResult = new SuccessResult();
            var setup = _mockRepo.Setup(b => b.Add(_cars.Data.First())).Returns(successResult);
            var result = _carsController.Add(_cars.Data.First());
            var actionResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, actionResult.StatusCode.Value);
        }
        [Fact]
        public void Add_IsInValidCar_ReturnBadRequest()
        {
            var errorResult = new ErrorResult();
            var AddedCar = new Car() {CarId=1,BrandId=1,ColorId=1,DailyPrice=100,Description=".d.d",Model="bmw",ModelYear=1995 };
            var setup = _mockRepo.Setup(c => c.Add(AddedCar)).Returns(errorResult);
            var result = _carsController.Add(AddedCar);
            var actionResult = Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void Delete_ActionExecutes_DeleteBrandAndReturnOk()
        {
            SuccessResult successResult = new SuccessResult();
            var setup = _mockRepo.Setup(b => b.Delete(_cars.Data.First())).Returns(successResult);
            var result = _carsController.Delete(_cars.Data.First());
            var actionResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal<int>(200, actionResult.StatusCode.Value);
        }

        [Fact]
        public void Delete_IsInValidBrand_ReturnBadRequest()
        {
            ErrorResult errorResult = new ErrorResult();
            var setup = _mockRepo.Setup(b => b.Delete(_cars.Data.First())).Returns(errorResult);
            var result = _carsController.Delete(_cars.Data.First());
            var actionResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal<int>(400, actionResult.StatusCode.Value);
        }

        [Fact]
        public void Update_ActionExecutes_UpdateBrandAndReturnOk()
        {
            SuccessResult successResult = new SuccessResult();
            var setup = _mockRepo.Setup(b => b.Update(_cars.Data.First())).Returns(successResult);
            var result = _carsController.Update(_cars.Data.First());
            var actionResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal<int>(200, actionResult.StatusCode.Value);
        }
        [Fact]
        public void Update_IsInValidBrand_ReturnBadRequest()
        {
            ErrorResult errorResult = new ErrorResult();
            var setup = _mockRepo.Setup(b => b.Update(_cars.Data.First())).Returns(errorResult);
            var result = _carsController.Update(_cars.Data.First());
            var actionResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal<int>(400, actionResult.StatusCode.Value);

        }
    }
}
