using Business.Abstract;
using Core.Entities.Concrete;
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
    public class UsersControllerTest
    {
        private readonly Mock<IUserService> _mockRepo;
        private readonly UsersController _userController;
        public IDataResult<List<User>> _users;
        public UsersControllerTest()
        {
            _mockRepo = new Mock<IUserService>();
            _userController = new UsersController(_mockRepo.Object);
            _users = new SuccessDataResult<List<User>>();
            _users.Data = new List<User>()
            {
                new User(){ Id=1,FirstName="Özgür",LastName="Rüzgar"},
                new User(){ Id=2,FirstName="Sedat",LastName="Rüzgar"}
            };
        }
        [Fact]
        public async void GetAllAsync_ActionExecutes_ReturnOkWithRentals()
        {
            var setup = _mockRepo.Setup(u => u.GetAllAsync()).ReturnsAsync(_users);
            var result = await _userController.GetAllAsync();
            var actionResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal<int>(200, actionResult.StatusCode.Value);
            Assert.IsAssignableFrom<SuccessDataResult<List<User>>>(actionResult.Value);
        }
        [Fact]
        public async void GetAllAsync_IsErrorDataResult_ReturnBadRequst()
        {
            var errorDataResult = new ErrorDataResult<List<User>>() { Data = null };
            var setup = _mockRepo.Setup(u => u.GetAllAsync()).ReturnsAsync(errorDataResult);
            var result = await _userController.GetAllAsync();
            var actionResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal<int>(400, actionResult.StatusCode.Value);
            Assert.IsAssignableFrom<ErrorDataResult<List<User>>>(actionResult.Value);
        }
        [Theory]
        [InlineData(1)]
        public async void GetById_IsValidRentalId_ReturnOkWithExpectedRental(int userId)
        {
            var expectedRental = new SuccessDataResult<User>() { Data = _users.Data.First(u => u.Id == userId) };
            var setup = _mockRepo.Setup(u => u.GetbyIdAsync(userId)).ReturnsAsync(expectedRental);
            var result = await _userController.GetByIdAsync(userId);
            var actionResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal<int>(200, actionResult.StatusCode.Value);
            Assert.IsAssignableFrom<SuccessDataResult<User>>(actionResult.Value);
        }
        [Theory]
        [InlineData(3)]
        public async void GetById_IsInValidRentalId_ReturnBadRequest(int userId)
        {
            var isInValidRental = new ErrorDataResult<User>() { Data = null };
            var setup = _mockRepo.Setup(u => u.GetbyIdAsync(userId)).ReturnsAsync(isInValidRental);
            var result = await _userController.GetByIdAsync(userId);
            var actionResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal<int>(400, actionResult.StatusCode.Value);
            Assert.IsAssignableFrom<ErrorDataResult<User>>(actionResult.Value);
        }
    }
}
