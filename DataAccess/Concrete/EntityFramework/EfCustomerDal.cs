using Core.DataAccess;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTO_s;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCustomerDal : EfEntityRepositoryBase<Customer, RentACarContext>, ICustomerDal
    {
        public List<CustomerDetailDto> GetCustomerDetail()
        {
            using (RentACarContext context = new RentACarContext())
            {
                var result = from c in context.Customers
                             join u in context.Users
                             on c.CustomerId equals u.Id
                             select new CustomerDetailDto { CustomerFirstName = u.FirstName, CustomerLastName = u.LastName, CustomerEmail = u.Email, CompanyName = c.CompanyName };
                return result.ToList();
            }
        }
    }
}
