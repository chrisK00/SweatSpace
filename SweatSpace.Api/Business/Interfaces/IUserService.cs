using System.Threading.Tasks;
using SweatSpace.Api.Business.Dtos;

namespace SweatSpace.Api.Business.Interfaces
{
    public interface IUserService
    {
        public Task Register(UserRegisterDto userRegisterDto);
    }
}
