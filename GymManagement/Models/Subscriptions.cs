namespace GymManagement.Models
{
    public class Subscriptions
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int NumOfMonths { get; set; }
         
        public WeekFrequency WeekFrequency { get; set; }
        public int TotalNumSessions { get; set; }
        public double TotalPrice { get; set; }
        public bool IsDeleted { get; set; }
    }


    public enum WeekFrequency
    {
        TwoDaysWeek = 2,
        ThreeDaysWeek = 3,
        FourDaysWeek = 4,
        FiveDaysWeek = 5

    }
}
