namespace PasteBinApi.Services.Interface
{
    public interface ITimeCalculationService
    {
        DateTime GetTimeToDelete(double timeSave);
    }
}