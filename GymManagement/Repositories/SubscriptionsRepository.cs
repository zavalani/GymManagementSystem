using GymManagement.Models;
using GymManagementx;

namespace GymManagement.Repositories
{
    public class SubscriptionsRepository
    {

        private readonly GymDbContext _context;
        public SubscriptionsRepository(GymDbContext context)
        {
            _context = context;
        }

        public void CreateSubscriptions(Subscriptions subscription)
        {
            if (subscription == null) throw new Exception("Subscription can't be empty");
            if (_context.Subscriptions.Any(x => x.Code == subscription.Code))
            {
                throw new Exception("Subscription with this Code already exists");
            }
            subscription.IsDeleted = false;

            _context.Subscriptions.Add(subscription);
            _context.SaveChanges();
        }

        public void UpdateSubscription(Subscriptions updatedSubscription)
        {
            var existingSubscription = _context.Subscriptions.FirstOrDefault(p => p.Id == updatedSubscription.Id);

            if (existingSubscription != null)
            {
                //Make sure Front end cannot put in a different Code TELL FRONT
                existingSubscription.Description = updatedSubscription.Description;
                existingSubscription.NumOfMonths = updatedSubscription.NumOfMonths;
                existingSubscription.WeekFrequency = updatedSubscription.WeekFrequency;
                existingSubscription.TotalNumSessions = updatedSubscription.TotalNumSessions;
                existingSubscription.TotalPrice = updatedSubscription.TotalPrice;

            }
            _context.SaveChanges();
        }
        public List<Subscriptions> GetSubscriptions(string filter)
        {

            //List of non-Deleted subscriptions
            var nonDeletedSubList = _context.Subscriptions.Where(x => x.IsDeleted == false).ToList();
            //We only iterate through the non-deleted Subscriptions
            return filter == null ? nonDeletedSubList : nonDeletedSubList.Where(x => x.NumOfMonths.ToString() == filter
                                        || ((int)x.WeekFrequency).ToString() == filter
                                        || x.Code == filter
                                        || x.Description == filter).ToList();


        }



        public void DeleteSubscriptions(int id)
        {
            var existingSubscription = _context.Subscriptions.Find(id);
            if (existingSubscription != null)
            {
                existingSubscription.IsDeleted = true;
                existingSubscription.Code = "old_" + existingSubscription.Code;

                Member_Subscriptions memberMemberSubscription = _context.Member_Subscriptions.FirstOrDefault(p => p.SubscriptionsId == id);
                if (memberMemberSubscription != null)
                {
                    memberMemberSubscription.IsDeleted = true;

                }

                _context.SaveChanges();
            }
        }

        public Subscriptions GetSubscriptionsById(int id)
        {
            return _context.Subscriptions.Where(x => x.Id == id && x.IsDeleted == false).FirstOrDefault();
        }



    }
}
