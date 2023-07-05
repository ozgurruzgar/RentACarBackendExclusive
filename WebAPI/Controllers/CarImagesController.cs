using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarImagesController : ControllerBase
    {
        private readonly ICarImageService _carImageService;

        public CarImagesController(ICarImageService carImageService)
        {
            _carImageService = carImageService;
        }
        [HttpGet("getall")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _carImageService.GetAllAsync();
            if(result.Data == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet("getbyid")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _carImageService.GetAsync(id);
            if (result.Data == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet("getbycarid")]
        public async Task<IActionResult> GetByCarIdAsync(int carId)
        {
            var result = await _carImageService.GetByCarIdAsync(carId);
            if (result.Data == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPost("add")]
        public IActionResult Add([FromForm(Name = ("Image"))] IFormFile file,CarImage carImage)
        {
            var result = _carImageService.Add(file, carImage);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPost("delete")]
        public IActionResult Delete([FromForm(Name = ("Id"))] CarImage carImage)
        {
            var result = _carImageService.Delete(carImage);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPost("update")]
        public async Task<IActionResult> Update([FromForm(Name = ("Image"))] IFormFile file, CarImage carImage)
        {
            var result = await _carImageService.Update(file, carImage);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
