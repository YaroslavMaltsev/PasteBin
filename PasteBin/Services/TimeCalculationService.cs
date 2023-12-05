using Microsoft.AspNetCore.Http.HttpResults;

namespace PasteBinApi.Services
{
    public static class TimeCalculationService
    {
        public static DateTime GetTimeToDelete(double timeSave,DateTime dateCreate)
        {
      
            
                return dateCreate.AddDays(timeSave);
           
        }
    }
}
