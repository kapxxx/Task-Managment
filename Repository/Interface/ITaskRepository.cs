using DTOs.RequestDTO;
using DTOs.ResponseDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface ITaskRepository
    {
        Task<TaskDTO> CreateAsync(TaskCreateDTO input);
        Task<TaskDTO> UpdateAsync(Guid id,TaskUpdateDTO input);
        Task<TaskDTO> GetAsync(Guid id);
        Task DeleteAsync(Guid id);
        public Task<List<TaskDTO>> GetAllAsync();
        Task<PagedResult<TaskDTO>> GetAllByUserIdAsync(Guid? id, GetTasksByUserIdDto input);
    }
}
