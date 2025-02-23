namespace GymManagement.Models
{
    public class Discounted_Member_Subscriptions
    {
        public int Id { get; set; }
        public int Members_SubscriptionsId { get; set; }
        public Member_Subscriptions? Member_Subscriptions { get; set; }
        public int DiscountsId { get; set; }
        public Discounts? Discounts { get; set; }
    }
}
