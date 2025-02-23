using GymManagement.Models;
using GymManagement.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.Controllers
{
    [ApiController]
    [Route("api/Member_Subscriptions")]
    public class Member_SubscriptionsController : Controller
    {
        private readonly Member_SubscriptionsRepository _membSubRepository;

        public Member_SubscriptionsController(Member_SubscriptionsRepository membSubRepository)
        {
            _membSubRepository = membSubRepository;
        }


        [HttpPost]
        public IActionResult CreateMemberSubscription(Member_Subscriptions membSub)
        {
            try
            {
                _membSubRepository.CreateMember_Subscriptions(membSub);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("{Id}")]
        public IActionResult UpdateMemberSubscription(int Id, Member_Subscriptions membSub)
        {
            var newMembSub = new Member_Subscriptions
            {
                Id = Id,
                OriginalPrice = membSub.OriginalPrice,
                DiscountValue = membSub.DiscountValue,
                PaidPrice = membSub.PaidPrice,
                StartDate = membSub.StartDate,
                EndDate = membSub.EndDate,
                RemainingSessions = membSub.RemainingSessions
            };

            try
            {
                _membSubRepository.UpdateMember_Subscriptions(newMembSub);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{Id}")]
        public IActionResult DeleteMemberSubscription(int Id)
        {
            try
            {
                _membSubRepository.DeleteMember_Subscriptions(Id);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /*
        [HttpGet("GetBymemberID")]
        public IActionResult GetMemberSubscriptionByMemberId(int id)
        {

            var memberSub = _membSubRepository.GetMember_SubscriptionsByMemberId(id);
            return Ok(memberSub);
        }

        [HttpGet("{id}")]
        public IActionResult GetMemberSubscriptionsBySubscriptionId(int id)
        {

            var memberSubList = _membSubRepository.GetMember_SubscriptionsBySubId(id);
            return Ok(memberSubList);
        }
        */

        [HttpGet("IdFilter")]
        public IActionResult GetMemberSubscriptionsByIdFilter(int IdFilter)
        {
            if (IdFilter == 0)
            {
                var memberSubList1 = _membSubRepository.GetMemberSubscriptions(0);
                return Ok(memberSubList1);


            }
           
            var memberSubList2 = _membSubRepository.GetMemberSubscriptions(IdFilter);
            return Ok(memberSubList2);
            

        }

        [HttpGet("{Id}")]
        public IActionResult GetMemberSubscriptionById(int Id)
        {

            var memSub = _membSubRepository.GetMemberSubscriptionById(Id);
            return Ok(memSub);


        }
        [HttpGet("GetAvailableMembers")]
        public IActionResult GetAvailableMembers()
        {

            var availableMembers = _membSubRepository.GetAvailableMembers();
            return Ok(availableMembers);


        }

    }
}
