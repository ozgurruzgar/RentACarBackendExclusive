using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICarService
    {
        Task<IDataResult<List<Car>>> GetAllAsync();
        Task<IDataResult<Car>> GetAsync(int carId);
        IDataResult<List<CarDetailDto>> GetCarDetail();
        IDataResult<List<CarDetailDto>> GetCarDetailByBrandId(int brandId);
        IDataResult<List<CarDetailDto>> GetCarDetailByColorId(int colorId);
        IDataResult<List<CarDetailDto>> GetCarDetailByCarId(int carId);
        IDataResult<List<CarDetailDto>> GetCarDetailByBrandAndColorId(int brandId, int colorId);
        IResult Add(Car car);
        IResult Delete(Car car);
        IResult Update(Car car);
    }
}
