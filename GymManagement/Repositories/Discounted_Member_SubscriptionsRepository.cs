using GymManagement.Models;
using GymManagementx;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace GymManagement.Repositories
{
    public class Discounted_Member_SubscriptionsRepository
    {
        private readonly GymDbContext _context;


        public Discounted_Member_SubscriptionsRepository(GymDbContext context)
        {
            _context = context;
        }

        public void CreateDisMemSub(Discounted_Member_Subscriptions disMemSubObject)
        {
            if (disMemSubObject == null) { throw new Exception("Discount Member Subscription can't be empty"); }
            //Check if MemberSub Exists and Is Active
            else if (_context.Member_Subscriptions.Any(x => x.Id == disMemSubObject.Members_SubscriptionsId && x.IsDeleted == false))
            {
                if (_context.Discounts.Any(x => x.Id == disMemSubObject.DiscountsId && x.isActive == true))
                {

                    var memSubCompare = _context.Member_Subscriptions.Where(x => x.Id == disMemSubObject.Members_SubscriptionsId && x.IsDeleted == false).FirstOrDefault();

                    
                        var discCompare = _context.Discounts.Where(x => x.Id == disMemSubObject.DiscountsId && x.isActive == true).FirstOrDefault();

                        int result2 = DateTime.Compare(discCompare.EndDate, DateTime.Now);
                        if (result2 >= 0)
                        {
                            _context.Add(disMemSubObject);
                            _context.SaveChanges();
                        }
                        else
                        {
                            throw new Exception("The discount has expired.");

                        }

 

                    

                }
                else
                {
                    throw new Exception("Cannot apply discount to a Discount that doesn't exist.");
                }

            }

            else
            {
                throw new Exception("Cannot apply discount to a Subscription that doesn't exist.");
            }
        }

        //No Update

        //No Delete

        //No Get


    }
}
