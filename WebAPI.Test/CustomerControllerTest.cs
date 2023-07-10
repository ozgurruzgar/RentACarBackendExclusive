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
    public class CustomerControllerTest
    {
        private readonly Mock<ICustomerService> _mockRepo;
        private readonly CustomersController _customersController;
        public IDataResult<List<Customer>> _customers;
        public CustomerControllerTest()
        {
            _mockRepo = new Mock<ICustomerService>();
            _customersController = new CustomersController(_mockRepo.Object);
            _customers = new SuccessDataResult<List<Customer>>();
            _customers.Data = new List<Customer>()
            {
                new Customer(){CustomerId=1,CompanyName="Aras Kargo" },
                new Customer(){CustomerId=2,CompanyName="Mng Kargo" }
            };
        }
        [Fact]
        public async void GetAllAsync_ActionExecutes_ReturnCustomerList()
        {
            var setup = _mockRepo.Setup(c => c.GetAllAsync()).ReturnsAsync(_customers);
            var result = await _customersController.GetAllAsync();
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var returnCustomer = Assert.IsAssignableFrom<SuccessDataResult<List<Customer>>>(actionResult.Value);
            Assert.Equal<int>(200, actionResult.StatusCode.Value);
        }
        [Fact]
        public async void GetAllAsync_IsErrorDataResult_ReturnBadRequest()
        {
            var errorDataResult = new ErrorDataResult<List<Customer>>() { Data=null};
            var setup = _mockRepo.Setup(c => c.GetAllAsync()).ReturnsAsync(errorDataResult);
            var result = await _customersController.GetAllAsync();
            var actionResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal<int>(400, actionResult.StatusCode.Value);
            Assert.IsAssignableFrom<ErrorDataResult<List<Customer>>>(actionResult.Value);
        }
        [Theory]
        [InlineData(1)]
        public async void GetById_IsValidCustomerId_ReturnOkWithCustomer(int customerId)
        {
            var expectedCustomer = new SuccessDataResult<Customer> { Data = _customers.Data.First(c => c.CustomerId == customerId) };
            var setup = _mockRepo.Setup(c => c.GetAsync(customerId)).ReturnsAsync(expectedCustomer);
            var result = await _customersController.GetByIdAsync(customerId);
            var actionResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal<int>(200, actionResult.StatusCode.Value);
            Assert.IsAssignableFrom<SuccessDataResult<Customer>>(actionResult.Value);
        }
        [Theory]
        [InlineData(3)]
        public async void GetById_IsInValidCustomerId_ReturnOkWithCustomer(int customerId)
        {
            var errorCustomer = new ErrorDataResult<Customer> { Data =null };
            var setup = _mockRepo.Setup(c => c.GetAsync(customerId)).ReturnsAsync(errorCustomer);
            var result = await _customersController.GetByIdAsync(customerId);
            var actionResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal<int>(400, actionResult.StatusCode.Value);
            Assert.IsAssignableFrom<ErrorDataResult<Customer>>(actionResult.Value);
        }
        [Fact]
        public void Add_IsValidCustomer_AddCustomerWithReturnOk()
        {
            var successResult = new SuccessResult();
            var setup = _mockRepo.Setup(c => c.Add(_customers.Data.First())).Returns(successResult);
            var result = _customersController.Add(_customers.Data.First());
            var actionResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal<int>(200, actionResult.StatusCode.Value);
        }
        [Fact]
        public void Add_InInValidCustomer_NotAddWithReturnBadRequest()
        {
            var addedCustomer = new Customer() { CustomerId = 0, CompanyName = "" };
            var errorResult = new ErrorResult();
            var setup = _mockRepo.Setup(c=>c.Add(addedCustomer)).Returns(errorResult);
            var result = _customersController.Add(addedCustomer);
            var actionResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal<int>(400, actionResult.StatusCode.Value);
        }        
        [Fact]
        public void Delete_IsValidCustomer_DeleteCustomerWithReturnOk()
        {
            var successResult = new SuccessResult();
            var setup = _mockRepo.Setup(c => c.Delete(_customers.Data.First())).Returns(successResult);
            var result = _customersController.Delete(_customers.Data.First());
            var actionResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal<int>(200, actionResult.StatusCode.Value);
        }
        [Fact]
        public void Delete_InInValidCustomer_NotDeleteWithReturnBadRequest()
        {
            var addedCustomer = new Customer() { CustomerId = 0, CompanyName = "" };
            var errorResult = new ErrorResult();
            var setup = _mockRepo.Setup(c=>c.Delete(addedCustomer)).Returns(errorResult);
            var result = _customersController.Delete(addedCustomer);
            var actionResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal<int>(400, actionResult.StatusCode.Value);
        }        
        [Fact]
        public void Update_IsValidCustomer_UpdateCustomerWithReturnOk()
        {
            var successResult = new SuccessResult();
            var setup = _mockRepo.Setup(c => c.Update(_customers.Data.First())).Returns(successResult);
            var result = _customersController.Update(_customers.Data.First());
            var actionResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal<int>(200, actionResult.StatusCode.Value);
        }
        [Fact]
        public void Delete_InInValidCustomer_NotUpdateWithReturnBadRequest()
        {
            var addedCustomer = new Customer() { CustomerId = 0, CompanyName = "" };
            var errorResult = new ErrorResult();
            var setup = _mockRepo.Setup(c=>c.Update(addedCustomer)).Returns(errorResult);
            var result = _customersController.Update(addedCustomer);
            var actionResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal<int>(400, actionResult.StatusCode.Value);
        }
    }
}
