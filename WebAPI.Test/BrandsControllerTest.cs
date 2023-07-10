using Business.Abstract;
using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
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
    public class BrandControllerTest
    {
        private readonly Mock<IBrandService> _mockRepo;
        private readonly BrandsController _brandController;
        public IDataResult<List<Brand>> _brands;
        public BrandControllerTest()
        {
            _mockRepo = new Mock<IBrandService>();
            _brandController= new BrandsController(_mockRepo.Object);
            _brands = new SuccessDataResult<List<Brand>>();
            _brands.Data = new List<Brand>() 
            {
                new Brand(){ BrandId=1,BrandName="Mazda"},
                new Brand(){ BrandId=2,BrandName="Volvo"}
            };
        }

        [Fact]
        public async void GetAllAsync_ActionExecutes_ReturnGetAllBrands()
        {
            var brandList= _mockRepo.Setup(b => b.GetAllAsync()).ReturnsAsync(_brands);
            var result = await _brandController.GetAllAsync();
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnBrand = Assert.IsAssignableFrom<SuccessDataResult<List<Brand>>>(okResult.Value);
            Assert.Equal<int>(2, returnBrand.Data.Count);
        }

        [Fact]
        public async void GetAllAsync_IsInErrorIDataResult_ReturnBadRequest() 
        {
            ErrorDataResult<List<Brand>> fakeBrand = new ErrorDataResult<List<Brand>> { Data=null};
            var brandList = _mockRepo.Setup(b => b.GetAllAsync()).ReturnsAsync(fakeBrand);
            var result = await _brandController.GetAllAsync();
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var returnBrand = Assert.IsAssignableFrom<ErrorDataResult<List<Brand>>>(badRequestResult.Value);
        }

        [Theory]
        [InlineData(1)]
        public async void GetById_ActionExecutes_ReturnBrandByBrandId(int brandId)
        {
            var brand = new SuccessDataResult<Brand>(_brands.Data.First(b=>b.BrandId==brandId));
            var expectedBrand = _mockRepo.Setup(b => b.GetAsync(brandId)).ReturnsAsync(brand);
            var result = await _brandController.GetByIdAsync(brandId);
            var successBrand = Assert.IsType<OkObjectResult>(result);
            Assert.Equal<int>(200, successBrand.StatusCode.Value);
        }

        [Theory]
        [InlineData(3)]
        public async void GetById_IsInValidBrandId_ReturnBadRequest(int brandId)
        {
            var brand = new ErrorDataResult<Brand>();
            var expectedBrand = _mockRepo.Setup(b => b.GetAsync(brandId)).ReturnsAsync(brand);
            var result = await _brandController.GetByIdAsync(brandId);
            var successBrand = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal<int>(400, successBrand.StatusCode.Value);
        }

        [Fact]
        public void Add_ActionExecutes_AddBrandAndReturnOk()
        {
            SuccessResult successResult = new SuccessResult();
            var setup = _mockRepo.Setup(b => b.Add(_brands.Data.First())).Returns(successResult);
            var result = _brandController.Add(_brands.Data.First());
            var actionResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, actionResult.StatusCode.Value);
        }

        [Fact]
        public void Add_IsInValidBrand_NotAddAndReturnBadRequest()
        {
            ErrorResult errorResult = new ErrorResult();
            Brand brand = new Brand { BrandId=1,BrandName=""};
            var setup = _mockRepo.Setup(b=>b.Add(brand)).Returns(errorResult);
            var result = _brandController.Add(brand);
            var actionResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal<int>(400, actionResult.StatusCode.Value);
        }

        [Fact]
        public void Delete_ActionExecutes_DeleteBrandAndReturnOk()
        {
            SuccessResult successResult = new SuccessResult();
            var setup = _mockRepo.Setup(b => b.Delete(_brands.Data.First())).Returns(successResult);
            var result = _brandController.Delete(_brands.Data.First());
            var actionResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal<int>(200, actionResult.StatusCode.Value);
        }

        [Fact]
        public void Delete_IsInValidBrand_ReturnBadRequest()
        {
            ErrorResult errorResult = new ErrorResult();
            var setup = _mockRepo.Setup(b=>b.Delete(_brands.Data.First())).Returns(errorResult);
            var result = _brandController.Delete(_brands.Data.First());
            var actionResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal<int>(400, actionResult.StatusCode.Value);
        }

        [Fact]
        public void Update_ActionExecutes_UpdateBrandAndReturnOk()
        {
            SuccessResult successResult = new SuccessResult();
            var setup = _mockRepo.Setup(b => b.Update(_brands.Data.First())).Returns(successResult);
            var result = _brandController.Update(_brands.Data.First());
            var actionResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal<int>(200, actionResult.StatusCode.Value);
        }
        [Fact]
        public void Update_IsInValidBrand_ReturnBadRequest()
        {
            ErrorResult errorResult = new ErrorResult();
            var setup = _mockRepo.Setup(b => b.Update(_brands.Data.First())).Returns(errorResult);
            var result = _brandController.Update(_brands.Data.First());
            var actionResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal<int>(400, actionResult.StatusCode.Value);
        }
    }
}
