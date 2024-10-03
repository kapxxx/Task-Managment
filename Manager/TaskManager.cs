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
    public class TaskManager : ITaskManager
    {
        private readonly ITaskRepository _taskRepository;

        public TaskManager(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<TaskDTO> CreateAsync(TaskCreateDTO input)
        {
          return await _taskRepository.CreateAsync(input);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _taskRepository.DeleteAsync(id);  
        }

        public async Task<PagedResult<TaskDTO>> GetAllByUserIdAsync(Guid? id, GetTasksByUserIdDto input)
        {
            return await _taskRepository.GetAllByUserIdAsync(id, input);
        }

        public async Task<TaskDTO> GetAsync(Guid id)
        {
            return await _taskRepository.GetAsync(id);
        }

        public async Task<TaskDTO> UpdateAsync(Guid id, TaskUpdateDTO input)
        {
            return await _taskRepository.UpdateAsync(id,input);
        }
    }
}
