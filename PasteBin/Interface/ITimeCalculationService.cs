namespace PasteBinApi.Interface
{
    public interface ITimeCalculationService
    {
        DateTime GetTimeToDelete(double timeSave);
    }
}