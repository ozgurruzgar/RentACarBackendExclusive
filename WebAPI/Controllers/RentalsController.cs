﻿using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private readonly IRentalService _rentalService;
        public RentalsController(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }
        [HttpPost("add")]
        public IActionResult Add(Rental rental)
        {
            var result = _rentalService.Add(rental);
            if(!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPost("delete")]
        public IActionResult Delete(Rental rental)
        {
            var result = _rentalService.Delete(rental);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPost("update")]
        public IActionResult Update(Rental rental)
        {
            var result = _rentalService.Update(rental);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet("getall")]
        public IActionResult GetAllAsync()
        {
            var result =  _rentalService.GetAllAsync();
            if(result.Data == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }        
        [HttpGet("getbyid")]
        public  IActionResult GetByIdAsync(int rentalId)
        {
            var result =  _rentalService.GetAsync(rentalId);
            if(result.Data == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
