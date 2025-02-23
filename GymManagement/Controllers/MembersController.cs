using GymManagement.Models;
using GymManagement.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;

namespace GymManagement.Controllers
{
    [ApiController]
    [Route("api/Members")]
    public class MembersController : Controller
    {
        private readonly MembersRepository _membersRepository;

        public MembersController(MembersRepository membersRepository)
        {
            _membersRepository = membersRepository;
        }


        [HttpPost]
        public IActionResult CreateMember(Members member)
        {
            try
            {
                _membersRepository.CreateMembers(member);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("{Id}")]
        public IActionResult UpdateMembers(int Id, Members updatedMember)
        {
            var newMember = new Members
            {
                Id = Id,
                FirstName = updatedMember.FirstName,
                LastName = updatedMember.LastName,
                Birthday = updatedMember.Birthday,
                IdCardNumber = updatedMember.IdCardNumber,
                Email = updatedMember.Email,
                RegistrationDate = updatedMember.RegistrationDate
            };

            try
            {
                _membersRepository.UpdateMember(newMember);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{Id}")]
        public IActionResult DeleteMember(int Id)
        {
            try
            {
                _membersRepository.DeleteMembers(Id);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("filter")]
        public IActionResult GetMembersByFilter(string? filter)
        {
            
            var membersList = _membersRepository.GetMembers(filter);
            return Ok(membersList);

            
        }

        [HttpGet("{Id}")]
        public IActionResult GetMembersById(int Id)
        {

            var member = _membersRepository.GetMembersById(Id);
            return Ok(member);


        }

    }
}
