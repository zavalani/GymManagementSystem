using GymManagement.Models;
using GymManagement.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.Controllers
{
    [ApiController]
    [Route("api/Discounts")]
    public class DiscountsController : Controller
    {
        private readonly DiscountsRepository _discountsRepository;

        public DiscountsController(DiscountsRepository discountsRepository)
        {
            _discountsRepository = discountsRepository;
        }


        [HttpPost]
        public IActionResult CreateDiscount(Discounts discounts)
        {
            try
            {
                _discountsRepository.CreateDiscount(discounts);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("{Id}")]
        public IActionResult UpdateDiscount(int Id, Discounts updatedDiscount)
        {
            var newDiscount = new Discounts
            {
                Id = Id,
                Value = updatedDiscount.Value,
                StartDate = updatedDiscount.StartDate,
                EndDate = updatedDiscount.EndDate
            };

            try
            {
                _discountsRepository.UpdateDiscount(newDiscount);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{Id}")]
        public IActionResult DeleteDiscount(int Id)
        {
            try
            {
                _discountsRepository.DeleteDiscount(Id);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /*

        [HttpGet("code")]
        public IActionResult GetDiscountsByCode(string code)
        {

            var discountList = _discountsRepository.GetDiscountByCode(code);
            return Ok(discountList);
        }*/

        [HttpGet("filter")]
        public IActionResult GetDiscountsByFilter(string? filter)
        {

            var discountsList = _discountsRepository.GetDiscountsByFilter(filter);
            return Ok(discountsList);


        }

        [HttpGet("{Id}")]
        public IActionResult GetDiscountsById(int Id)
        {
            var discount = _discountsRepository.GetDiscountsById(Id);
            return Ok(discount);
        }

    }
}
