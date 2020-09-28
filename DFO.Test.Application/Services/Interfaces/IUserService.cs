using System.Threading.Tasks;
using DFO.Test.Application.Contracts.User;

namespace DFO.Test.Application.Services.Interfaces
{
    public interface IUserService
    {

        Task<BaseResponse<UserResponse>> GetAll();

        Task<BaseResponse<UserResponse>> Add(UserRequest userRequest);

        Task<BaseResponse<UserResponse>> GetByHashId(string hashId);

        Task<BaseResponse<UserResponse>> UpdateByHashId(string hashId, UserRequest userRequest);

    }
}
