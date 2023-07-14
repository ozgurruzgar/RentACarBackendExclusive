using Business.Abstract;
using Entities.Concrete;

using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandService _brandService;
        public BrandsController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpPost("add")]
        public IActionResult Add(Brand brand)
        {
            var result = _brandService.Add(brand);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }        
        [HttpPost("delete")]
        public IActionResult Delete(Brand brand)
        {
            var result = _brandService.Delete(brand);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }        
        [HttpPost("update")]
        public IActionResult Update(Brand brand)
        {
            var result = _brandService.Update(brand);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet("getall")]
        public  IActionResult GetAllAsync()
        {
            var result =  _brandService.GetAllAsync();
            if(result.Data == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }        
        [HttpGet("getbyId")]
        public  IActionResult GetByIdAsync(int brandId)
        {
            var result =  _brandService.GetAsync(brandId);
            if(result.Data == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
