using Core.DataAccess;
using Entities.Concrete;
using Entities.DTO_s;

namespace DataAccess.Abstract
{
    public interface ICustomerDal : IEntityRepository<Customer>
    {
        List<CustomerDetailDto> GetCustomerDetail();
    }
}
