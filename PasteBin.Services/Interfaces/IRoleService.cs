using PasteBin.Domain.Interfaces;

namespace PasteBin.Services.Interfaces
{
    public interface IRoleService
    {
        Task<IBaseResponse<bool>> CreateRole();
    }
}