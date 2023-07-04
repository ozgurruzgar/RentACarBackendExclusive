using Core.DataAccess;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTO_s;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, RentACarContext>, ICarDal
    {
        public List<CarDetailDto> GetCarDetail()
        {
            using (var context = new RentACarContext())
            {
                var result = from car in context.Cars
                             join b in context.Brands
                             on car.BrandId equals b.BrandId
                             join c in context.Colors
                             on car.ColorId equals c.ColorId
                             select new CarDetailDto
                             {
                                 BrandId = b.BrandId,
                                 ColorId = c.ColorId,
                                 CarId = car.CarId,
                                 BrandName = b.BrandName,
                                 ColorName = c.ColorName,
                                 ModelName = car.Model,
                                 ModelYear = car.ModelYear,
                                 DailyPrice = car.DailyPrice,
                                 Description = car.Description,
                                 ImagePath = (from ci in context.CarImages where car.CarId == ci.CarId select ci.ImagePath).FirstOrDefault()
                             };

                return result.ToList();
            }
        }

        public List<CarDetailDto> GetCarDetailByBrandAndColorId(int brandId, int colorId)
        {
            using (var context = new RentACarContext())
            {
                var result = from car in context.Cars
                             join b in context.Brands
                             on car.BrandId equals b.BrandId
                             join c in context.Colors
                             on car.ColorId equals c.ColorId
                             where b.BrandId == brandId && c.ColorId == colorId
                             select new CarDetailDto
                             {
                                 BrandId = b.BrandId,
                                 CarId = car.CarId,
                                 ColorId = c.ColorId,
                                 ModelName = car.Model,
                                 BrandName = b.BrandName,
                                 ColorName = c.ColorName,
                                 Description = car.Description,
                                 DailyPrice = car.DailyPrice,
                                 ModelYear = car.ModelYear,
                                 ImagePath = (from ci in context.CarImages where car.CarId == ci.CarId select ci.ImagePath).FirstOrDefault()
                             };
                return result.ToList();
            }
        }

        public List<CarDetailDto> GetCarDetailByBrandId(int brandId)
        {
            using (var context = new RentACarContext())
            {
                var result = from car in context.Cars
                             join b in context.Brands
                             on car.BrandId equals b.BrandId
                             join c in context.Colors
                             on car.ColorId equals c.ColorId
                             where b.BrandId == brandId
                             select new CarDetailDto
                             {
                                 BrandId = b.BrandId,
                                 CarId = car.CarId,
                                 ColorId = c.ColorId,
                                 ModelName = car.Model,
                                 BrandName = b.BrandName,
                                 ColorName = c.ColorName,
                                 Description = car.Description,
                                 DailyPrice = car.DailyPrice,
                                 ModelYear = car.ModelYear,
                                 ImagePath = (from ci in context.CarImages where car.CarId == ci.CarId select ci.ImagePath).FirstOrDefault()
                             };
                return result.ToList();
            }
        }

        public List<CarDetailDto> GetCarDetailByCarId(int carId)
        {
            using (var context = new RentACarContext())
            {
                var result = from car in context.Cars
                             join b in context.Brands
                             on car.BrandId equals b.BrandId
                             join c in context.Colors
                             on car.ColorId equals c.ColorId
                             where car.CarId == carId
                             select new CarDetailDto
                             {
                                 BrandId = b.BrandId,
                                 CarId = car.CarId,
                                 ColorId = c.ColorId,
                                 ModelName = car.Model,
                                 BrandName = b.BrandName,
                                 ColorName = c.ColorName,
                                 Description = car.Description,
                                 DailyPrice = car.DailyPrice,
                                 ModelYear = car.ModelYear,
                                 ImagePath = (from ci in context.CarImages where car.CarId == ci.CarId select ci.ImagePath).FirstOrDefault()
                             };
                return result.ToList();
            }
        }

        public List<CarDetailDto> GetCarDetailByColorId(int colorId)
        {
            using (var context = new RentACarContext())
            {
                var result = from car in context.Cars
                             join b in context.Brands
                             on car.BrandId equals b.BrandId
                             join c in context.Colors
                             on car.ColorId equals c.ColorId
                             where c.ColorId == colorId
                             select new CarDetailDto
                             {
                                 BrandId = b.BrandId,
                                 CarId = car.CarId,
                                 ColorId = c.ColorId,
                                 ModelName = car.Model,
                                 BrandName = b.BrandName,
                                 ColorName = c.ColorName,
                                 Description = car.Description,
                                 DailyPrice = car.DailyPrice,
                                 ModelYear = car.ModelYear,
                                 ImagePath = (from ci in context.CarImages where car.CarId == ci.CarId select ci.ImagePath).FirstOrDefault()
                             };
                return result.ToList();
            }
        }
    }
}

