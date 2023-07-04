using Core.DataAccess;
using Entities.Concrete;
using Entities.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ICarDal:IEntityRepository<Car>
    {
        List<CarDetailDto> GetCarDetail();
        List<CarDetailDto> GetCarDetailByBrandId(int brandId);
        List<CarDetailDto> GetCarDetailByColorId(int colorId);
        List<CarDetailDto> GetCarDetailByCarId(int carId);
        List<CarDetailDto> GetCarDetailByBrandAndColorId(int brandId,int colorId);
    }
}
