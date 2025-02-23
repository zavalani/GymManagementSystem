using GymManagement.Models;
using GymManagementx;
using Microsoft.IdentityModel.Tokens;

namespace GymManagement.Repositories
{
    public class DiscountsRepository
    {
        private readonly GymDbContext _context;

        public DiscountsRepository(GymDbContext context)
        {
            _context = context;
        }


        public void CreateDiscount(Discounts discount)
        {
            if (discount == null) throw new Exception("Discount can't be empty");

            if (_context.Discounts.Any(x => x.Code == discount.Code))
            {
                throw new Exception("Discount with this Code already exists");
            }
            discount.isActive = true;

            _context.Discounts.Add(discount);
            _context.SaveChanges();
        }

        public void UpdateDiscount(Discounts updatedDiscount)
        {
            var existingDiscount = _context.Discounts.FirstOrDefault(p => p.Id == updatedDiscount.Id);

            if (existingDiscount != null)
            {
                //We don't update the Code
                existingDiscount.Value = updatedDiscount.Value;
                existingDiscount.StartDate = updatedDiscount.StartDate;
                existingDiscount.EndDate = updatedDiscount.EndDate;

                //We don't update isActive

            }
            _context.SaveChanges();
        }

        /*
        public Discounts GetDiscountByCode(string code)
        {
            //NUK ESHTE LISTE ESHTE GET 1 ELEMENT
            //Dont show deleted
            return _context.Discounts.Where(x => x.isActive == true && x.Code == code).FirstOrDefault();

                       
        }
        */


        public void DeleteDiscount(int id)
        {
            var existingDiscount = _context.Discounts.Find(id);

            if (existingDiscount != null)
            {
                existingDiscount.isActive = false;
                existingDiscount.Code = "old_" + existingDiscount.Code;

                _context.SaveChanges();
            }
        }

        public List<Discounts> GetDiscountsByFilter(string filter)
        {
            //Dont show deleted
            var nonDeletedDiscountList = _context.Discounts.Where(x => x.isActive == true).ToList();

            return filter.IsNullOrEmpty() ? nonDeletedDiscountList : nonDeletedDiscountList.Where(x => x.Code == filter).ToList();




        }

        public Discounts GetDiscountsById(int id)
        {
            return _context.Discounts.Where(x => x.isActive == true && x.Id == id).FirstOrDefault();
        }

    }
}
