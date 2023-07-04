using Core.DataAccess;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTO_s;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental, RentACarContext>, IRentalDal
    {
        public List<RentalDetailDto> GetRentalDetail()
        {
            using (var context = new RentACarContext())
            {
                var result = from r in context.Rentals
                             join car in context.Cars
                             on r.CarId equals car.CarId
                             join c in context.Customers
                             on r.CustomerId equals c.CustomerId
                             join b in context.Brands
                             on car.BrandId equals b.BrandId
                             join color in context.Colors
                             on car.ColorId equals color.ColorId
                             join u in context.Users
                             on c.CustomerId equals u.Id
                             select new RentalDetailDto
                             {
                                 CarBrandName = b.BrandName,
                                 CarColorName = color.ColorName,
                                 CarName = car.Model,
                                 CustomerFullName = u.FirstName + " " + u.LastName,
                                 ModelYear = car.ModelYear,
                                 Description=car.Description,
                                 RentDate=r.RentDate,
                                 ReturnDate=r.ReturnDate
                             };
                return result.ToList();
            }
        }
    }
}
