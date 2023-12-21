using AutoMapper;
using PasteBin.Model;
using PasteBinApi.DTOs;
using PasteBinApi.Interface;

namespace PasteBinApi.Helpers.AutoMapper.Paste.CreateMethods
{
    public class TimeCalculatorResolver : IValueResolver<CreatePasteDto, Past, DateTime>
    {
        private readonly ITimeCalculationService _time;

        public TimeCalculatorResolver(ITimeCalculationService time)
        {
            _time = time;
        }

        public DateTime Resolve(CreatePasteDto source, Past destination, DateTime destMember, ResolutionContext context)
        {
            var timeToDelete = _time.GetTimeToDelete(source.DateSave);

            return timeToDelete;
        }
    }
}