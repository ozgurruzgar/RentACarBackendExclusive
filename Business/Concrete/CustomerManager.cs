using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Business.Concrete
{
    public class CustomerManager : ICustomerService
    {
        private readonly ICustomerDal _customerDal;
        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }
        public IResult Add(Customer customer)
        {
            _customerDal.Add(customer);
            return new SuccessResult();
        }

        public IResult Delete(Customer customer)
        {
           _customerDal.Delete(customer);
            return new SuccessResult();
        }

        public async Task<IDataResult<List<Customer>>> GetAllAsync()
        {
            var customers = await _customerDal.GetAllAsync();
            return new SuccessDataResult<List<Customer>>(customers);
        }

        public async Task<IDataResult<Customer>> GetAsync(int customerId)
        {
            var customer = await _customerDal.GetAsync(c => c.CustomerId == customerId);
            return new SuccessDataResult<Customer>(customer);
        }

        public IResult Update(Customer customer)
        {
           _customerDal.Update(customer);
            return new SuccessResult();
        }
    }
}
