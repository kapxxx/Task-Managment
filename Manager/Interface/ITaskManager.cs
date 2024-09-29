using DTOs.RequestDTO;
using DTOs.ResponseDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Interface
{
    public interface ITaskManager
    {
        Task<TaskDTO> CreateAsync(TaskCreateDTO input);
        Task<TaskDTO> UpdateAsync(TaskUpdateDTO input);
        Task<TaskDTO> GetAsync(Guid id);
        Task DeleteAsync(Guid id);
    }
}
