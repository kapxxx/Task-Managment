using DTOs.ResponseDTO;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IUserInterface
    {
        Task<GetUserByTaskStatus> GetUserByTaskStatus(int status);
    }
}
