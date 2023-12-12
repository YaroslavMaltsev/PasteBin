using AutoMapper;
using PasteBin.Model;
using PasteBinApi.Interface;
using PasteBinApi.ResourceModel;

namespace PasteBinApi.Helpers.AutoMapper.Paste.CreateMethods
{
    public class TimeCalculatorResolver : IValueResolver<CreatePastDto, Past, DateTime>
    {
        private readonly ITimeCalculationService _time;

        public TimeCalculatorResolver(ITimeCalculationService time)
        {
            _time = time;
        }

        public DateTime Resolve(CreatePastDto source, Past destination, DateTime destMember, ResolutionContext context)
        {
            var timeToDelete = _time.GetTimeToDelete(source.DateSave);

            return timeToDelete;
        }
    }
}
