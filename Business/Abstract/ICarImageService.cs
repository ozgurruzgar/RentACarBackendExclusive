﻿using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICarImageService
    {
        Task<IDataResult<List<CarImage>>> GetAllAsync();
        Task<IDataResult<List<CarImage>>> GetByCarIdAsync(int carId);
        Task<IDataResult<CarImage>> GetAsync(int id);
        IResult Add(IFormFile file, CarImage carImage);
        IResult Delete(CarImage carImage);
        Task<IResult> Update(IFormFile file, CarImage carImage);
    }
}
