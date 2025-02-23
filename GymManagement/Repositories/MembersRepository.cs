using GymManagement.Models;
using GymManagementx;
using Microsoft.IdentityModel.Tokens;

namespace GymManagement.Repositories
{
    public class MembersRepository
    {

        private readonly GymDbContext _context;
        public MembersRepository(GymDbContext context)
        {
            _context = context;
        }


        public void CreateMembers(Members member)
        {
            if (member == null) throw new Exception("Member can't be empty");
            if (_context.Members.Any(x => x.IdCardNumber == member.IdCardNumber))
            {
                throw new Exception("Member with this ID Card already exists");
            }
            member.IsDeleted = false;

            _context.Members.Add(member);
            _context.SaveChanges();
        }

        public void UpdateMember(Members updatedMember)
        {
            var existingMember = _context.Members.FirstOrDefault(p => p.Id == updatedMember.Id);

            if (existingMember != null)
            {
                existingMember.FirstName = updatedMember.FirstName;
                existingMember.LastName = updatedMember.LastName;
                existingMember.Birthday = updatedMember.Birthday;
                //TELL FRONT THAT YOU CANT UPDATE IDCARDNUMBER
                existingMember.Email = updatedMember.Email;
                existingMember.RegistrationDate = updatedMember.RegistrationDate;
                //We don't update IsDeleted

            }
            _context.SaveChanges();
        }
        public List<Members> GetMembers(string filter)
        {
            //Dont show deleted
            var nonDeletedMemberList = _context.Members.Where(x => x.IsDeleted == false).ToList();

            return  filter.IsNullOrEmpty() ? nonDeletedMemberList : nonDeletedMemberList.Where(x => x.FirstName == filter || x.LastName == filter || x.IdCardNumber == filter || x.Email == filter).ToList();


   
            
        }

        public Members GetMembersById(int id)
        {
            return _context.Members.Where(x=>x.Id == id && x.IsDeleted == false).FirstOrDefault();
        }


        public void DeleteMembers(int id)
        {
            var existingMember = _context.Members.Find(id);

            if (existingMember != null)
            {
                existingMember.IsDeleted = true;
                existingMember.IdCardNumber = "old_" + existingMember.IdCardNumber;

                Member_Subscriptions memberMemberSubscription = _context.Member_Subscriptions.FirstOrDefault(p => p.MembersId == id);
                if (memberMemberSubscription != null)
                {
                    memberMemberSubscription.IsDeleted = true;
                }

                _context.SaveChanges();
            }
        }

    }
}
