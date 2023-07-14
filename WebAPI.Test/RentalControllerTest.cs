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
    public class RentalControllerTest
    {
        private readonly Mock<IRentalService> _mockRepo;
        private readonly RentalsController _rentalController;
        public IDataResult<List<Rental>> _rentals;
        public RentalControllerTest()
        {
            _mockRepo = new Mock<IRentalService>();
            _rentalController = new RentalsController(_mockRepo.Object);
            _rentals = new SuccessDataResult<List<Rental>>();
            _rentals.Data = new List<Rental>()
            {
                new Rental() {CarId=1,CustomerId=2,RentalId=1},
                new Rental() {CarId=2,CustomerId=1,RentalId=2},
            };
        }
        [Fact]
        public  void GetAllAsync_ActionExecutes_ReturnOkWithRentals()
        {
            var setup = _mockRepo.Setup(r => r.GetAllAsync()).Returns(_rentals);
            var result = _rentalController.GetAllAsync();
            var actionResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal<int>(200, actionResult.StatusCode.Value);
            Assert.IsAssignableFrom<SuccessDataResult<List<Rental>>>(actionResult.Value);
        }
        [Fact]
        public  void GetAllAsync_IsErrorDataResult_ReturnBadRequst()
        {
            var errorDataResult = new ErrorDataResult<List<Rental>>() { Data = null };
            var setup = _mockRepo.Setup(r => r.GetAllAsync()).Returns(errorDataResult);
            var result = _rentalController.GetAllAsync();
            var actionResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal<int>(400, actionResult.StatusCode.Value);
            Assert.IsAssignableFrom<ErrorDataResult<List<Rental>>>(actionResult.Value);
        }
        [Theory]
        [InlineData(1)]
        public  void GetById_IsValidRentalId_ReturnOkWithExpectedRental(int rentalId)
        {
            var expectedRental = new SuccessDataResult<Rental>() { Data = _rentals.Data.First(r => r.RentalId == rentalId) };
            var setup = _mockRepo.Setup(r => r.GetAsync(rentalId)).Returns(expectedRental);
            var result = _rentalController.GetByIdAsync(rentalId);
            var actionResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal<int>(200, actionResult.StatusCode.Value);
            Assert.IsAssignableFrom<SuccessDataResult<Rental>>(actionResult.Value);
        }        
        [Theory]
        [InlineData(3)]
        public  void GetById_IsInValidRentalId_ReturnBadRequest(int rentalId)
        {
            var isInValidRental = new ErrorDataResult<Rental>() { Data =null };
            var setup = _mockRepo.Setup(r => r.GetAsync(rentalId)).Returns(isInValidRental);
            var result = _rentalController.GetByIdAsync(rentalId);
            var actionResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal<int>(400, actionResult.StatusCode.Value);
            Assert.IsAssignableFrom<ErrorDataResult<Rental>>(actionResult.Value);
        }
        [Fact]
        public void Add_IsValidRental_AddRentalWithOk()
        {
            var setup = _mockRepo.Setup(r => r.Add(_rentals.Data.First())).Returns(new SuccessResult());
            var result = _rentalController.Add(_rentals.Data.First());
            var actionResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal<int>(200, actionResult.StatusCode.Value);
        }        
        [Fact]
        public void Add_IsErrorDataResult_ReturnBadRequest()
        {
            var setup = _mockRepo.Setup(r => r.Add(_rentals.Data.First())).Returns(new ErrorResult());
            var result = _rentalController.Add(_rentals.Data.First());
            var actionResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal<int>(400, actionResult.StatusCode.Value);
        }        
        [Fact]
        public void Delete_IsValidRental_DeleteRentalWithOk()
        {
            var setup = _mockRepo.Setup(r => r.Delete(_rentals.Data.First())).Returns(new SuccessResult());
            var result = _rentalController.Delete(_rentals.Data.First());
            var actionResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal<int>(200, actionResult.StatusCode.Value);
        }        
        [Fact]
        public void Delete_IsErrorDataResult_ReturnBadRequest()
        {
            var setup = _mockRepo.Setup(r => r.Delete(_rentals.Data.First())).Returns(new ErrorResult());
            var result = _rentalController.Delete(_rentals.Data.First());
            var actionResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal<int>(400, actionResult.StatusCode.Value);
        }        
        [Fact]
        public void Update_IsValidRental_UpdateRentalWithOk()
        {
            var setup = _mockRepo.Setup(r => r.Update(_rentals.Data.First())).Returns(new SuccessResult());
            var result = _rentalController.Update(_rentals.Data.First());
            var actionResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal<int>(200, actionResult.StatusCode.Value);
        }        
        [Fact]
        public void Update_IsErrorDataResult_ReturnBadRequest()
        {
            var setup = _mockRepo.Setup(r => r.Update(_rentals.Data.First())).Returns(new ErrorResult());
            var result = _rentalController.Update(_rentals.Data.First());
            var actionResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal<int>(400, actionResult.StatusCode.Value);
        }

    } 
}
