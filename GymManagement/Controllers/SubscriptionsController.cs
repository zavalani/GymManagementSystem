using GymManagement.Models;
using GymManagement.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.Controllers
{
    [ApiController]
    [Route("api/Subscriptions")]
    public class SubscriptionsController : Controller
    {
        private readonly SubscriptionsRepository _subscriptionsRepository;

        public SubscriptionsController(SubscriptionsRepository subscriptionsRepository)
        {
            _subscriptionsRepository = subscriptionsRepository;
        }


        [HttpPost]
        public IActionResult CreateSubscription(Subscriptions subscription)
        {
            try
            {
                _subscriptionsRepository.CreateSubscriptions(subscription);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("{Id}")]
        public IActionResult UpdateSubscription(int Id, Subscriptions updatedSubscription)
        {
            var newSubscripton = new Subscriptions
            {
                Id = Id,
                Description = updatedSubscription.Description,
                NumOfMonths = updatedSubscription.NumOfMonths,
                WeekFrequency = updatedSubscription.WeekFrequency,
                TotalNumSessions = updatedSubscription.TotalNumSessions,
                TotalPrice = updatedSubscription.TotalPrice
            };

            try
            {
                _subscriptionsRepository.UpdateSubscription(newSubscripton);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{Id}")]
        public IActionResult DeleteSubscription(int Id)
        {
            try
            {
                _subscriptionsRepository.DeleteSubscriptions(Id);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("filter")]
        public IActionResult GetSubscriptionsByFilter(string? filter)
        {

            var subscriptionsList = _subscriptionsRepository.GetSubscriptions(filter);
            return Ok(subscriptionsList);
        }

        [HttpGet("{Id}")]
        public IActionResult GetSubscriptionsById(int Id)
        {

            var subscription = _subscriptionsRepository.GetSubscriptionsById(Id);
            return Ok(subscription);


        }


    }
}
