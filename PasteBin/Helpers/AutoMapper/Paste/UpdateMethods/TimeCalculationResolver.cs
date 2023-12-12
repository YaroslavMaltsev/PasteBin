using AutoMapper;
using PasteBin.Model;
using PasteBinApi.Dto;
using PasteBinApi.Interface;
using PasteBinApi.ResourceModel;

namespace PasteBinApi.Helpers.AutoMapper.Paste.UpdateMethods
{
    public class TimeCalculationResolver : IValueResolver<UpdatePastDto, Past, DateTime>
    {
        private readonly ITimeCalculationService _time;

        public TimeCalculationResolver(ITimeCalculationService time)
        {
            _time = time;
        }
        public DateTime Resolve(UpdatePastDto source, Past destination, DateTime destMember, ResolutionContext context)
        {
            var timeToDelete = _time.GetTimeToDelete(source.DateSave);

            return timeToDelete;
        }
    }
}
