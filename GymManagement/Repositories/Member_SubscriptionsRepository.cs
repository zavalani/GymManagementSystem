using GymManagement.Models;
using GymManagementx;
using System.Collections.Generic;
using GymManagement.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace GymManagement.Repositories
{
    public class Member_SubscriptionsRepository
    {

        private readonly GymDbContext _context;
        public Member_SubscriptionsRepository(GymDbContext context)
        {
            _context = context; 
        }

        public void CreateMember_Subscriptions(Member_Subscriptions memberSubscription)
        {
            //Error possible
            //Check if Member with that ID exists in Members table
            if (_context.Members.Any(x=>x.Id == memberSubscription.MembersId && x.IsDeleted == false))
            {
                //Check if Subsctiption with that ID exists in Subscriptions table
                if (_context.Subscriptions.Any(x => x.Id == memberSubscription.SubscriptionsId && x.IsDeleted == false))
                {
                    //Check if there already is a Subscription for the same Member


                    
                        if (_context.Discounts.Any(x => x.Value == memberSubscription.DiscountValue && x.isActive == true && memberSubscription.DiscountValue != null)){
                            //if there is a Discount
                            //Calculations
                            


                            var discount = _context.Discounts.Where(x => x.Value == memberSubscription.DiscountValue && x.isActive == true && memberSubscription.DiscountValue != null).FirstOrDefault();
                            
                            Discounted_Member_Subscriptions dMemSub = new Discounted_Member_Subscriptions();
                            _context.Member_Subscriptions.Add(memberSubscription);
                            _context.SaveChanges();

                            dMemSub.Members_SubscriptionsId = memberSubscription.Id;
                            dMemSub.DiscountsId = discount.Id;

                            Discounted_Member_SubscriptionsRepository repo = new Discounted_Member_SubscriptionsRepository(_context);
                            repo.CreateDisMemSub(dMemSub);


                           
                            _context.SaveChanges();

                        }

                        else
                        {
                            memberSubscription.PaidPrice = memberSubscription.OriginalPrice;
                            _context.Member_Subscriptions.Add(memberSubscription);
                            _context.SaveChanges();

                        }

                        
                    


                }

                else
                {
                    throw new Exception("Subcription doesn't exist");
                }

            }

            else
            {
                throw new Exception("Member doesn't exist");
            }

            
        }
        

        public void UpdateMember_Subscriptions(Member_Subscriptions updatedMembSub)
        {
            var existingMembSub = _context.Member_Subscriptions.FirstOrDefault(p => p.Id == updatedMembSub.Id);

            if (existingMembSub != null)
            {
                //We dont update MemberID or SubID
                existingMembSub.OriginalPrice = updatedMembSub.OriginalPrice;
                existingMembSub.DiscountValue = updatedMembSub.DiscountValue;
                existingMembSub.PaidPrice = updatedMembSub.PaidPrice;
                existingMembSub.StartDate = updatedMembSub.StartDate;
                existingMembSub.EndDate = updatedMembSub.EndDate;
                existingMembSub.RemainingSessions = updatedMembSub.RemainingSessions;
                //We dont update IsDeleted

            }
            _context.SaveChanges();
        }


        public void DeleteMember_Subscriptions(int id)
        {
            var existingSubscription = _context.Member_Subscriptions.Find(id);
            if (existingSubscription != null)
            {
                _context.Remove(existingSubscription);
                _context.SaveChanges();
            }
        }


        public List<Member_Subscriptions> GetMemberSubscriptions(int objectId)
        {
            //Dont show deleted

            return objectId == 0 ? _context.Member_Subscriptions.ToList() : _context.Member_Subscriptions.Where(x => x.SubscriptionsId == objectId || x.MembersId == objectId).ToList();


        }

        public Member_Subscriptions GetMemberSubscriptionById(int id)
        {
            return _context.Member_Subscriptions.Where(x => x.Id == id).FirstOrDefault();
        }

        public List<Members> GetAvailableMembers()
        {
            var membSubList = _context.Member_Subscriptions.Where(x => x.IsDeleted == false).Select(x => x.MembersId).ToList();
            var members = _context.Members.Where(x => !membSubList.Contains(x.Id) && x.IsDeleted == false).ToList();
            return members;

        }


    }
}
