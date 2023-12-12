using PasteBinApi.Interface;

namespace PasteBinApi.Services
{
    public class TimeCalculationService : ITimeCalculationService
    {
        public DateTime GetTimeToDelete(double timeSave)
        {

            return DateTime.UtcNow.AddDays(timeSave);
        }
    }
}
