using Core.DataAccess;
using Entities.Concrete;
using Entities.DTO_s;

namespace DataAccess.Abstract
{
    public interface IRentalDal : IEntityRepository<Rental>
    {
        List<RentalDetailDto> GetRentalDetail();
    }
}
