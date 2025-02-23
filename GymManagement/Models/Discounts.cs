namespace GymManagement.Models
{
    public class Discounts
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int Value { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool isActive { get; set; }

    }
}
