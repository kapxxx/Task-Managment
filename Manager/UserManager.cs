using DTOs.RequestDTO;
using DTOs.ResponseDTO;
using Manager.Interface;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository _userRepository;

        public UserManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<PagedResult<UserTaskDTO>> GetAllUserTasks(UserTaskFilterDTO input)
        {
            return await _userRepository.GetAllUserTasks(input);
        }
    }
}
