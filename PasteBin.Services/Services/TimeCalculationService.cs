using PasteBinApi.Services.Interface;

namespace PasteBinApi.Services.Services
{
    public class TimeCalculationService : ITimeCalculationService
    {
        public DateTime GetTimeToDelete(double timeSave)
        {

            return DateTime.UtcNow.AddDays(timeSave);
        }
    }
}
