using AutoMapper;
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
        private readonly IMapper _mapper;

        public TaskRepository(MyDbContext myDbContext,IMapper mapper)
        {
            _myDbContext = myDbContext;
            _mapper = mapper;
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
                //CreatedBy=input.CreatedBy
                UpdatedAt=DateTime.Now,
                UpdatedBy=input.UpdatedBy
                

            };
            var result=await _myDbContext.Tasks.AddAsync(task);
            await _myDbContext.SaveChangesAsync();
            return _mapper.Map<TaskDTO>(result);

        }

        public async Task DeleteAsync(Guid id)
        {
            var task = await _myDbContext.Tasks.FindAsync(id);
            if (task != null)
            {
                _myDbContext.Tasks.Remove(task);
                await _myDbContext.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("Task not found.");
            }
        }

        public async Task<TaskDTO> GetAsync(Guid id)
        {
            var task = await _myDbContext.Tasks.FindAsync(id);
            if (task == null)
            {
                throw new KeyNotFoundException("Task not found.");
            }

            return _mapper.Map<TaskDTO>(task);
        }

        public async Task<TaskDTO> UpdateAsync(Guid id,TaskUpdateDTO input)
        {
            var task = await _myDbContext.Tasks.FindAsync(id);
            if (task == null)
            {
                throw new KeyNotFoundException("Task not found.");
            }

            // Update task properties
            task.Name = input.Name;
            task.AssignedToUserId = input.AssignedToUserId;
            task.Description = input.Description;
            task.Priority = input.Priority;
            task.Status = input.Status; // Make sure TaskStatuses enum is handled properly
            task.DueDate = input.DueDate;
            task.UpdatedAt = DateTime.Now;
            task.UpdatedBy = input.UpdatedBy;

            _myDbContext.Tasks.Update(task);
            await _myDbContext.SaveChangesAsync();
            return _mapper.Map<TaskDTO>(task);
        }
    }
}
