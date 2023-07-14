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
    public class ColorsControllerTest
    {
        private readonly Mock<IColorService> _mockRepo;
        private readonly ColorsController _colorController;
        public IDataResult<List<Color>> _colors;
        public ColorsControllerTest()
        {
            _mockRepo = new Mock<IColorService>();
            _colorController = new ColorsController(_mockRepo.Object);
            _colors = new SuccessDataResult<List<Color>>();
            _colors.Data = new List<Color>()
            {
                new Color(){ ColorId=1,ColorName="Mavi"},
                new Color(){ ColorId=2,ColorName="Yeşil"}
            };
        }

        [Fact]
        public  void GetAllAsync_ActionExecutes_ReturnGetAllColors()
        {
            var colorList = _mockRepo.Setup(b => b.GetAllAsync()).Returns(_colors);
            var result =  _colorController.GetAllAsync();
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnColor = Assert.IsAssignableFrom<SuccessDataResult<List<Color>>>(okResult.Value);
            Assert.Equal<int>(2, returnColor.Data.Count);
        }

        [Fact]
        public  void GetAllAsync_IsInErrorIDataResult_ReturnBadRequest()
        {
            ErrorDataResult<List<Color>> fakeColor = new ErrorDataResult<List<Color>> { Data = null };
            var colorList = _mockRepo.Setup(b => b.GetAllAsync()).Returns(fakeColor);
            var result =  _colorController.GetAllAsync();
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var returnColor = Assert.IsAssignableFrom<ErrorDataResult<List<Color>>>(badRequestResult.Value);
        }

        [Theory]
        [InlineData(1)]
        public  void GetById_ActionExecutes_ReturnColorByColorId(int colorId)
        {
            var color = new SuccessDataResult<Color>(_colors.Data.First(c =>c.ColorId == colorId));
            var expectedColor = _mockRepo.Setup(b => b.GetAsync(colorId)).Returns(color);
            var result =  _colorController.GetByIdAsync(colorId);
            var successColor = Assert.IsType<OkObjectResult>(result);
            Assert.Equal<int>(200, successColor.StatusCode.Value);
        }

        [Theory]
        [InlineData(3)]
        public async void GetById_IsInValidColorId_ReturnBadRequest(int colorId)
        {
            var color = new ErrorDataResult<Color>();
            var expectedColor = _mockRepo.Setup(b => b.GetAsync(colorId)).Returns(color);
            var result = _colorController.GetByIdAsync(colorId);
            var successColor = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal<int>(400, successColor.StatusCode.Value);
        }

        [Fact]
        public void Add_ActionExecutes_AddColorAndReturnOk()
        {
            SuccessResult successResult = new SuccessResult();
            var setup = _mockRepo.Setup(b => b.Add(_colors.Data.First())).Returns(successResult);
            var result = _colorController.Add(_colors.Data.First());
            var actionResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, actionResult.StatusCode.Value);
        }

        [Fact]
        public void Add_IsInValidColor_NotAddAndReturnBadRequest()
        {
            ErrorResult errorResult = new ErrorResult();
            Color color = new Color { ColorId = 1, ColorName = "" };
            var setup = _mockRepo.Setup(b => b.Add(color)).Returns(errorResult);
            var result = _colorController.Add(color);
            var actionResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal<int>(400, actionResult.StatusCode.Value);
        }

        [Fact]
        public void Delete_ActionExecutes_DeleteColorAndReturnOk()
        {
            SuccessResult successResult = new SuccessResult();
            var setup = _mockRepo.Setup(b => b.Delete(_colors.Data.First())).Returns(successResult);
            var result = _colorController.Delete(_colors.Data.First());
            var actionResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal<int>(200, actionResult.StatusCode.Value);
        }

        [Fact]
        public void Delete_IsInValidColor_ReturnBadRequest()
        {
            ErrorResult errorResult = new ErrorResult();
            var setup = _mockRepo.Setup(b => b.Delete(_colors.Data.First())).Returns(errorResult);
            var result = _colorController.Delete(_colors.Data.First());
            var actionResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal<int>(400, actionResult.StatusCode.Value);
        }

        [Fact]
        public void Update_ActionExecutes_UpdateColorAndReturnOk()
        {
            SuccessResult successResult = new SuccessResult();
            var setup = _mockRepo.Setup(b => b.Update(_colors.Data.First())).Returns(successResult);
            var result = _colorController.Update(_colors.Data.First());
            var actionResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal<int>(200, actionResult.StatusCode.Value);
        }
        [Fact]
        public void Update_IsInValidColor_ReturnBadRequest()
        {
            ErrorResult errorResult = new ErrorResult();
            var setup = _mockRepo.Setup(b => b.Update(_colors.Data.First())).Returns(errorResult);
            var result = _colorController.Update(_colors.Data.First());
            var actionResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal<int>(400, actionResult.StatusCode.Value);

        }
    }
}
