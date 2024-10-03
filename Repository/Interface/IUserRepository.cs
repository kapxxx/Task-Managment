using DTOs.RequestDTO;
using DTOs.ResponseDTO;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IUserRepository
    {
        Task<GetUserByTaskStatus> GetUserByTaskStatus(int status);
        Task<PagedResult<UserTaskDTO>> GetAllUserTasks(UserTaskFilterDTO input);
    }
}
