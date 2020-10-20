using System.Threading.Tasks;
using User.Core.Dto;

namespace User.Services.Interface
{
    public interface IAdminstrationservice
    {
        Task<object> CreateRole(CreateRoleDto createRoleDto);
        Task<object> Delete(string id);
        object Get();
        Task<object> Get(string id);
        Task<object> Put(string id, CreateRoleDto createRoleDto);
    }
}