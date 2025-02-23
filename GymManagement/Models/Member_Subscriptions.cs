namespace GymManagement.Models
{
    public class Member_Subscriptions
    {
        public int Id { get; set; }
        public int MembersId { get; set; }
        public Members? Members { get; set; }
        public int SubscriptionsId { get; set; }
        public Subscriptions? Subscriptions { get; set; }
        public double OriginalPrice { get; set; }
        public double? DiscountValue { get; set; }
        public double PaidPrice { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int RemainingSessions { get; set; }
        public bool IsDeleted { get; set; }

    }
}
