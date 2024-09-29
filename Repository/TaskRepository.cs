using DTOs.RequestDTO;
using DTOs.ResponseDTO;
using Repository.Data;
using Repository.Interface;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly MyDbContext _myDbContext;

        public TaskRepository(MyDbContext myDbContext)
        {
            _myDbContext = myDbContext;
        }

        public async Task<TaskDTO> CreateAsync(TaskCreateDTO input)
        {
            Tasks task = new Tasks {
                Id= Guid.NewGuid(),
                Name= input.Name,
                AssignedToUserId=input.AssignedToUserId,
                CreatedByUserId=input.CreatedByUserId,
                Description=input.Description,
                Priority=input.Priority,
                Status=DTOs.Enums.TaskStatuses.ToDo,
                DueDate=input.DueDate,
                CreatedAt=DateTime.Now,
                

            };
            var result=await _myDbContext.Tasks.AddAsync(task);
            await _myDbContext.SaveChangesAsync();
            return result;

        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<TaskDTO> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<TaskDTO> UpdateAsync(TaskUpdateDTO input)
        {
            throw new NotImplementedException();
        }
    }
}
